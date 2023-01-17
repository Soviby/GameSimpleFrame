
using System.Collections.Generic;
public class MyUIItemData
{
    public const string DEFAULT_ITEM_GROUP = "default";
    private Dictionary<string, List<MyUIItem>> _uiItemMap = new Dictionary<string, List<MyUIItem>>();

    public List<MyUIItem> GetUIItemListByGroupName(string groupName = DEFAULT_ITEM_GROUP)
    {
        if (!_uiItemMap.ContainsKey(groupName)) return null;
        return _uiItemMap[groupName];
    }

    public MyUIItem GetUIItem(int index,string groupName = DEFAULT_ITEM_GROUP)
    {
        var itemList = GetUIItemListByGroupName(groupName);
        if (itemList == null)
            return null;
        else
        {
            return itemList.Count <= index ? null : itemList[index];
        }
    }

    public void AddUIItem(MyUIItem item, string groupName = DEFAULT_ITEM_GROUP)
    {
        var itemList = GetUIItemListByGroupName(groupName);
        if (itemList == null)
        {
            _uiItemMap.Add(groupName, new List<MyUIItem>());
            _uiItemMap[groupName].Add(item);
        }
        else
        {
            itemList.Add(item);
        }
    }

    public void RemoveUIItem(MyUIItem item, string groupName = DEFAULT_ITEM_GROUP)
    {
        var itemList = GetUIItemListByGroupName(groupName);
        if (itemList == null)
            return ;
        else
        {
            itemList.Remove(item);
        }
    }

    public void CloseAllUIItem()
    {
        foreach (var group in _uiItemMap.Values)
        {
            foreach (var item in group)
            {
                item.Close();
            }
            group.Clear();
        }

        _uiItemMap.Clear();
    }



}
