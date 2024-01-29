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

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    private bool pDeath;

    // 自身のTransform
    [SerializeField] private Transform myT;

    // ターゲットのTransform
    [SerializeField] private Transform targetT;

    public GameObject targetObject; // 存在を確認したいオブジェクト
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
            //ポーズ中
            if (!targetObject2.activeSelf)
            {
                float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
                float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

                cameraRot *= Quaternion.Euler(-yRot, 0, 0);
                characterRot *= Quaternion.Euler(0, xRot, 0);

                //Updateの中で作成した関数を呼ぶ
                cameraRot = ClampRotation(cameraRot);

                cam.transform.localRotation = cameraRot;
                transform.localRotation = characterRot;
            }
        }
        else
        {
            // ターゲットの方向に自身を回転させる
            myT.LookAt(targetT);
        }
        
        //プレイヤーが死んだら
        if(targetObject == null)
        {
            //カーソルを出す
            cursorLock = false;
        }

        UpdateCursorLock();
    }

    //マウスカーソルの有無
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
            //カーソルを消す
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            //カーソルを出す
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

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