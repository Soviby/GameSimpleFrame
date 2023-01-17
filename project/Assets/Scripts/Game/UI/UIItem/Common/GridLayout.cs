using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridLayout: MyUIItem
{
    private IMyLayoutGroup layoutGroup;
    protected override void OnInit()
    {
        layoutGroup = this.gameObject.GetComponent<LayoutGroup>() as IMyLayoutGroup;
    }

    public void InitChildren<T>(int count, System.Action<int, T> initItemCallback = null) where T : MyUIItem, new()
    {
        var groupList = this.GetUIItemListByGroupName<T>();
        if (count < groupList.Count)
        {
            var residueList = groupList.GetRange(count, groupList.Count- count);
            foreach (var item in residueList)
            {
                this.RemoveUIItem(item);
                item.Close();
            }
        }
        
        layoutGroup.InitChildren(count, (index, rt)=> {
            T uiItem = this.GetUIItem<T>(index);
            if(uiItem==null)
                uiItem = this.AddUIItem<T>(target: rt.gameObject);
            initItemCallback(index, uiItem);
        });
    }

}