using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light roomLight;
    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g

    void Start()
    {
        // �Q�[���J�n���Ƀ��C�g���I�t�ɂ���
        if (roomLight != null)
        {
            roomLight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�����񂾂�I���ɂ���
        if (targetObject == null)
        {
            roomLight.enabled = true;
        }
    }
}
