using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject Panelfade;   //フェードパネルの取得
    public GameObject targetObject; // 存在を確認したいオブジェクト
    Image fadealpha;               //フェードパネルのイメージ取得変数
    private float alpha;           //パネルのalpha値取得変数
    private bool fadeout;          //フェードアウトのフラグ変数

    // Colliderコンポーネントへの参照
    private Collider myCollider;

    void Start()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //パネルのイメージ取得
        alpha = fadealpha.color.a;                 //パネルのalpha値を取得

        // Colliderコンポーネントを取得
        myCollider = GetComponent<Collider>();

        //初期状態ではパネルを無効
        Panelfade.gameObject.SetActive(false);

        // 初期状態ではIsTriggerを無効にする
        myCollider.isTrigger = false;
    }

    void Update()
    {
        // 鍵がマップ上に存在するか確認
        if (targetObject == null)
        {
            // IsTriggerを切り替える
            myCollider.isTrigger = true;
        }

        if (fadeout == true)
        {
            FadeOut();
        }
    }

    //フェードアウト
    void FadeOut()
    {
        alpha += 0.01f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        //画面が暗くなったら
        if (alpha >= 1)
        {
            fadeout = false;
            //リザルト画面に移行
            SceneManager.LoadScene("Result");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("クリア");
            Panelfade.gameObject.SetActive (true);
            fadeout = true;
        }
    }
}
