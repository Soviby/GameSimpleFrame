using System.Collections;
using UnityEngine;

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager : Singleton<EventManager>, IUpdateable
{
    public void Update()
    {
        ExEventCrossThreadHelper.MainThreadUpdate();
    }


    #region 所有事件
    // input System
    public class Input_BtnA: ExEvent<Input_BtnA>
    {
        public int deviceId;
        public float btnValue;
    }

    public class Input_BtnB : ExEvent<Input_BtnB>
    {
        public int deviceId;
        public float btnValue;
    }

    public class Input_BtnBack : ExEvent<Input_BtnBack>
    {
        public int deviceId;
        public float btnValue;
    }
    // Battle

    public class Battle_GameStart : ExEvent<Battle_GameStart>
    {
    }

    // Player
    public class Player_PlayerChange : ExEvent<Player_PlayerChange>
    {
    }

    #endregion
}

