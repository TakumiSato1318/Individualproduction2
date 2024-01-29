using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button button;
    public Button button2;

    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g

    void Start()
    {
        // �{�^�������߂͔�\���ɂ���
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)&&targetObject!=null)
        {
            //�{�^���̕\���؂�ւ�
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
        // �{�^���̕\����؂�ւ���
        button.gameObject.SetActive(!button.gameObject.activeSelf);
        button2.gameObject.SetActive(!button2.gameObject.activeSelf);
    }
}
