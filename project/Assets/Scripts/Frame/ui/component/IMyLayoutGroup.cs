using System.Collections;
using UnityEngine;

public interface IMyLayoutGroup
{
    public void InitChildren(int count, System.Action<int, RectTransform> initItemCallback = null);

}