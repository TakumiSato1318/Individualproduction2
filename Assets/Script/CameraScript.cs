using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject mainCamera; //メインカメラ格納用
    private GameObject playerObject;//回転の中心となるプレイヤー格納用
    public float rotateSpeed = 2.0f;    //回転の速さ
    public float deathCount = 5.0f;//

    [SerializeField] private CameraShake cameraShake_;

    private bool pDeath;//true=死  false=生

    //呼び出し時に実行される関数
    void Start()
    {
        pDeath = false;

        mainCamera = Camera.main.gameObject;
        playerObject = GameObject.Find("Player");

        cameraShake_.SetUp(mainCamera,1.0f);
    }


    //単位時間ごとに実行される関数
    void Update()
    {
        //プレイヤーが生存している間
        if (pDeath == false)
        {
            //キー入力に応じてカメラを揺らす
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Vector3 handBob = cameraShake_.DoHeadBob(0.8f);
                mainCamera.transform.localPosition = handBob;
            }//後退するとき
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 handBob = cameraShake_.DoHeadBob(0.6f);
                mainCamera.transform.localPosition = handBob;
            }
            else//停止中もゆっくり揺らす
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
            Debug.Log("カメラ衝突");
            pDeath = true;
        }
    }

    //カメラを回転させる関数
    private void rotateCamera()
    {
        //Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed*0.01f, Input.GetAxis("Mouse Y") * rotateSpeed * 0.01f, 0);
    }
}