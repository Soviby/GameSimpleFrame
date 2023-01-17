using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 5f;
    private Vector3 distance;
    public Transform target;

    private void Start()
    {
        distance = target.position - transform.position;
    }


    private void LateUpdate()
    {

        Vector3 newTrans = target.position - distance;
        transform.position = Vector3.Lerp(transform.position, newTrans, cameraSpeed);
        //transform.rotation = target.rotation;//û��ֻ���þ�ͷ����ת����������һֱ�Ϳ����ˣ��������λ���Լ�����
    }
}