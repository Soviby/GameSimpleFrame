
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class PanelBase 
{
    protected GameObject _gameObject;

    protected MyUIItemData itemData = new MyUIItemData();

    private Action clearCallback;

    public bool IsVisible
    {
        get
        {
            return _gameObject && _gameObject.activeSelf;
        }
    }

    public GameObject gameObject { get => _gameObject; }
    public MyTaskRunner myTaskRunner = new MyTaskRunner(5);


    protected void initRC()
    {
        var rc = this._gameObject.GetComponent<ReferenceCollector>();
        if (rc)
        {
            foreach (var data in rc.data)
            {
                var type = data.gameObject.GetType();
                if (type == typeof(UIItemConfig))
                {
                    var config = (UIItemConfig)data.gameObject;
                    var itemType = Type.GetType(config.uiItemClassName);
                    var uiItem = Activator.CreateInstance(itemType) as MyUIItem;
                    itemData.AddUIItem(uiItem);

                    var key = data.key;
                    var fieldInfo = this.GetType().GetField(key);
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(this, uiItem);
                    }

                    uiItem.Init(config.gameObject);
                }
            }
        }
    }

    public T AddUIItem<T>(Transform root = null, GameObject target=null, string groupName = MyUIItemData.DEFAULT_ITEM_GROUP) where T : MyUIItem, new()
    {
        var config = MyGUIManager.Instance.GetItemConfigByClassType<T>();
        if (config == null)
        {
            return null;
        }
        if (!target)
        {
            target = GameObject.Instantiate(Resources.Load<GameObject>(@"UI\Prefabs\UIItem\" + config.resName));
            if (root)
            {
                target.transform.SetParent(root, false);
            }
        }

        var uiItem = new T();
        itemData.AddUIItem(uiItem);
        uiItem.Init(target);
        return uiItem;
    }

    public void RemoveUIItem(MyUIItem item, string groupName = MyUIItemData.DEFAULT_ITEM_GROUP)
    {
        itemData.RemoveUIItem(item);
    }

    public T GetUIItem<T>(int index, string groupName = MyUIItemData.DEFAULT_ITEM_GROUP) where T : MyUIItem, new()
    {
        var uiItem = itemData.GetUIItem(index);
        return uiItem==null ? null: uiItem as T; 
    }

    public List<T> GetUIItemListByGroupName<T>(string groupName = MyUIItemData.DEFAULT_ITEM_GROUP) where T : MyUIItem, new()
    {
        var uiItemList = itemData.GetUIItemListByGroupName(groupName);
        if (uiItemList == null)
            return new List<T>();
        else
        {
            var list = new List<T>();
            uiItemList.ForEach(i=> {
                list.Add(i as T);
            });
            return list;
        }
    }

    public void Close()
    {
        clearCallback?.Invoke();
        clearCallback = null;
        myTaskRunner.Stop();

        OnClose();

        itemData.CloseAllUIItem();
        GameObject.Destroy(_gameObject);
    }

    protected virtual void OnClose()
    {

    }

    protected virtual void OnInit()
    {

    }
    protected virtual void OnShow()
    {

    }

    protected virtual void OnHide()
    {

    }

    public virtual void Update()
    {

    }

    protected MyTask RunUITask(IEnumerator e)
    {
        return myTaskRunner.Run(e);
    }

    protected void AddListener<T>(ExEventHandler<T> eventHandler) where T : ExEvent<T>, IEvent
    {
        ExEvent<T>.Add(eventHandler);
        clearCallback += () => {
            ExEvent<T>.Remove(eventHandler);
        };
    }



}