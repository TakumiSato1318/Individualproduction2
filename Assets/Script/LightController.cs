using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light roomLight;
    public GameObject targetObject; // 存在を確認したいオブジェクト

    void Start()
    {
        // ゲーム開始時にライトをオフにする
        if (roomLight != null)
        {
            roomLight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが死んだらオンにする
        if (targetObject == null)
        {
            roomLight.enabled = true;
        }
    }
}
