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
        //�S�[���ɕt������I�u�W�F�N�g�������Ȃ�����
        if (targetObject == null)
        {
            //���U���g��ʂ�\������
           result.gameObject.SetActive(true);
        }
    }
}
