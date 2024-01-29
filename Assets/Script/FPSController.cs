using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FPSController : MonoBehaviour
{
    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    private bool cursorLock = true;

    private int lockTime = 0;
    private bool countFlag=false;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    private bool pDeath;

    // ���g��Transform
    [SerializeField] private Transform myT;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform targetT;

    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g
    public GameObject targetObject2;

    // Start is called before the first frame update
    void Start()
    {
        pDeath = false;
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (pDeath==false)
        {
            //�|�[�Y��
            if (!targetObject2.activeSelf)
            {
                float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
                float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

                cameraRot *= Quaternion.Euler(-yRot, 0, 0);
                characterRot *= Quaternion.Euler(0, xRot, 0);

                //Update�̒��ō쐬�����֐����Ă�
                cameraRot = ClampRotation(cameraRot);

                cam.transform.localRotation = cameraRot;
                transform.localRotation = characterRot;
            }
        }
        else
        {
            // �^�[�Q�b�g�̕����Ɏ��g����]������
            myT.LookAt(targetT);
        }
        
        //�v���C���[�����񂾂�
        if(targetObject == null)
        {
            //�J�[�\�����o��
            cursorLock = false;
        }

        UpdateCursorLock();
    }

    //�}�E�X�J�[�\���̗L��
    void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        if (Input.GetMouseButton(0)&&targetObject2==true)
        {
            countFlag = true;
        }

        if(countFlag==true)
        {
            lockTime++;
        }

        if (lockTime >= 10)
        {
            cursorLock = true;
            countFlag = false;
            lockTime = 0;
        }


        if (cursorLock)
        {
            //�J�[�\��������
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            //�J�[�\�����o��
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //�p�x�����֐��̍쐬
    Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            pDeath = true;
        }
    }
}