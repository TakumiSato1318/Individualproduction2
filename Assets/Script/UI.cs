using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject targetObject; // 存在を確認したいオブジェクト
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 表示
        SetVisibility(true);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)&&targetObject!=null)
        {
            // 表示と非表示を切り替える
            ToggleVisibility();
        }
    }

    void SetVisibility(bool isVisible)
    {
        // プロパティを使用して表示と非表示を切り替える
        spriteRenderer.enabled = isVisible;
    }

    void ToggleVisibility()
    {
        // 現在の表示状態に応じて切り替える
        SetVisibility(!spriteRenderer.enabled);
    }
}
