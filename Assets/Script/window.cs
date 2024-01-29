using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class window : MonoBehaviour
{
    public GameObject targetObject; // 存在を確認したいオブジェクト
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 表示
        SetVisibility(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 鍵がマップ上に存在するか確認
        if (targetObject == null)
        {
            // 非表示
            SetVisibility(false);
        }
    }

    void SetVisibility(bool isVisible)
    {
        // プロパティを使用して表示と非表示を切り替える
        spriteRenderer.enabled = isVisible;
    }
}
