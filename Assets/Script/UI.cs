using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �\��
        SetVisibility(true);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)&&targetObject!=null)
        {
            // �\���Ɣ�\����؂�ւ���
            ToggleVisibility();
        }
    }

    void SetVisibility(bool isVisible)
    {
        // �v���p�e�B���g�p���ĕ\���Ɣ�\����؂�ւ���
        spriteRenderer.enabled = isVisible;
    }

    void ToggleVisibility()
    {
        // ���݂̕\����Ԃɉ����Đ؂�ւ���
        SetVisibility(!spriteRenderer.enabled);
    }
}
