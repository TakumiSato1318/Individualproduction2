using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject mainCamera; //���C���J�����i�[�p
    private GameObject playerObject;//��]�̒��S�ƂȂ�v���C���[�i�[�p
    public float rotateSpeed = 2.0f;    //��]�̑���
    public float deathCount = 5.0f;//

    [SerializeField] private CameraShake cameraShake_;

    private bool pDeath;//true=��  false=��

    //�Ăяo�����Ɏ��s�����֐�
    void Start()
    {
        pDeath = false;

        mainCamera = Camera.main.gameObject;
        playerObject = GameObject.Find("Player");

        cameraShake_.SetUp(mainCamera,1.0f);
    }


    //�P�ʎ��Ԃ��ƂɎ��s�����֐�
    void Update()
    {
        //�v���C���[���������Ă����
        if (pDeath == false)
        {
            //�L�[���͂ɉ����ăJ������h�炷
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Vector3 handBob = cameraShake_.DoHeadBob(0.8f);
                mainCamera.transform.localPosition = handBob;
            }//��ނ���Ƃ�
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 handBob = cameraShake_.DoHeadBob(0.6f);
                mainCamera.transform.localPosition = handBob;
            }
            else//��~�����������h�炷
            {
                Vector3 handBob = cameraShake_.DoHeadBob(0.09f);
                mainCamera.transform.localPosition = handBob;
            }
        }
        else
        {
            deathCount -= 0.05f;
            Vector3 handBob = cameraShake_.DoHeadBob(deathCount);
            mainCamera.transform.localPosition = handBob;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            Debug.Log("�J�����Փ�");
            pDeath = true;
        }
    }

    //�J��������]������֐�
    private void rotateCamera()
    {
        //Vector3��X,Y�����̉�]�̓x�������`
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed*0.01f, Input.GetAxis("Mouse Y") * rotateSpeed * 0.01f, 0);
    }
}