using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject result;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        result.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゴールに付随するオブジェクトが無くなったら
        if (targetObject == null)
        {
            //リザルト画面を表示する
           result.gameObject.SetActive(true);
        }
    }
}
