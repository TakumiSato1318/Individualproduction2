using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal : MonoBehaviour
{
    public GameObject medal;
    public GameObject medal2;
    public GameObject medal3;

    public GameObject targetObject; // 存在を確認したいオブジェクト
    public GameObject targetObject2;
    public GameObject targetObject3;

    void Start()
    {
        medal.gameObject.SetActive(false);
        medal2.gameObject.SetActive(false);
        medal3.gameObject.SetActive(false);
    }

    void Update()
    {
        if(targetObject == null)
        {
            medal.gameObject.SetActive(true);
        }
        if(targetObject2 == null)
        {
            medal2.gameObject.SetActive(true);
        }
        if (targetObject3 == null)
        {
            medal3.gameObject.SetActive(true);
        }
    }
}
