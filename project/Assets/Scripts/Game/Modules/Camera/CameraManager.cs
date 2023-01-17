using UnityEngine;
using UnityEditor;



public class CameraManager : Singleton<CameraManager>
{
    private GameObject _root;

    public Camera mainCamera
    {
        get
        {
            return Camera.main;
        }
    }
}
