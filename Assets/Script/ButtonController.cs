using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button button;
    public Button button2;

    public GameObject targetObject; // 存在を確認したいオブジェクト

    void Start()
    {
        // ボタンを初めは非表示にする
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)&&targetObject!=null)
        {
            //ボタンの表示切り替え
            ToggleButtonVisibility();
        }

         if(targetObject == null)
        {
            button.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
        }
    }

    void ToggleButtonVisibility()
    {
        // ボタンの表示を切り替える
        button.gameObject.SetActive(!button.gameObject.activeSelf);
        button2.gameObject.SetActive(!button2.gameObject.activeSelf);
    }
}
