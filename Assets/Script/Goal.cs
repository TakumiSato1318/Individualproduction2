using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject Panelfade;   //�t�F�[�h�p�l���̎擾
    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g
    Image fadealpha;               //�t�F�[�h�p�l���̃C���[�W�擾�ϐ�
    private float alpha;           //�p�l����alpha�l�擾�ϐ�
    private bool fadeout;          //�t�F�[�h�A�E�g�̃t���O�ϐ�

    // Collider�R���|�[�l���g�ւ̎Q��
    private Collider myCollider;

    void Start()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //�p�l���̃C���[�W�擾
        alpha = fadealpha.color.a;                 //�p�l����alpha�l���擾

        // Collider�R���|�[�l���g���擾
        myCollider = GetComponent<Collider>();

        //������Ԃł̓p�l���𖳌�
        Panelfade.gameObject.SetActive(false);

        // ������Ԃł�IsTrigger�𖳌��ɂ���
        myCollider.isTrigger = false;
    }

    void Update()
    {
        // �����}�b�v��ɑ��݂��邩�m�F
        if (targetObject == null)
        {
            // IsTrigger��؂�ւ���
            myCollider.isTrigger = true;
        }

        if (fadeout == true)
        {
            FadeOut();
        }
    }

    //�t�F�[�h�A�E�g
    void FadeOut()
    {
        alpha += 0.01f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        //��ʂ��Â��Ȃ�����
        if (alpha >= 1)
        {
            fadeout = false;
            //���U���g��ʂɈڍs
            SceneManager.LoadScene("Result");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("�N���A");
            Panelfade.gameObject.SetActive (true);
            fadeout = true;
        }
    }
}
