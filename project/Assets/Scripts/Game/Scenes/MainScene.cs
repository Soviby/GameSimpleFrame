using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : SceneBehavior
{
    public MainScene(Scene _scene) : base(_scene)
    {

    }

    public override async UniTask OnEnterScene(LoadItem item, Action UpdateLoading)
    {
        this.AddListener<EventManager.Input_BtnA>(OnBtnA);
        this.AddListener<EventManager.Input_BtnBack>(OnBtnBack);
        MyGUIManager.Instance.Show<MainPanel>();
    }

    private void OnBtnA(EventManager.Input_BtnA input_BtnA)
    {
        if (input_BtnA.btnValue == 1)
        {
            // GameSceneManager.Instance.EnterScene<PrepareScene>();
        }
    }

    private void OnBtnBack(EventManager.Input_BtnBack input_BtnBack)
    {
        if (input_BtnBack.btnValue == 1)
        {
            Application.Quit();
        }
    }

    public override async UniTask OnLeaveScene(LoadItem item, Action UpdateLoading)
    {
        MyGUIManager.Instance.Close<MainPanel>();
    }

}

