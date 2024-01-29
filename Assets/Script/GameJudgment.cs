using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJudgment : MonoBehaviour
{
    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g
    public GameObject targetObject2; // ���݂��m�F�������I�u�W�F�N�g
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��\��
        SetVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �}�b�v��ɑ��݂��邩�m�F
        if (targetObject == null)
        {
            // �\��
            SetVisibility(true);
            Debug.Log("����������");
        }
    }

    void SetVisibility(bool isVisible)
    {
        // �v���p�e�B���g�p���ĕ\���Ɣ�\����؂�ւ���
        spriteRenderer.enabled = isVisible;
    }
}
