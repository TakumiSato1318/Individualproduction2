using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    public float normalSpeed = 8.0f;     // 通常の速度
    public float collisionSpeed = 0.01f;  // 衝突時の速度
    public float collisionDuration = 1000f; // 衝突中の時間

    private float currentSpeed;         // 現在の速度
    private float collisionTimer = 0f;  // 衝突タイマー

    private bool death;//死亡

    public GameObject soul;
    public GameObject targetObject; // 存在を確認したいオブジェクト

    //アニメーション
    [SerializeField] Animator animator;

    void Start()
    {
        death = false;
        soul.SetActive(true);

        animator =GetComponent<Animator>();

        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (death == false&&!targetObject.activeSelf)
        {
            // キー入力を取得
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 移動ベクトル
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

            // ワールド座標系での移動
            transform.Translate(movement * currentSpeed * Time.deltaTime);

            // 速度を制限
            float currentMagnitude = GetComponent<Rigidbody>().velocity.magnitude;
            if (currentMagnitude > normalSpeed)
            {
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * normalSpeed;
            }

            // 衝突中の速度制御
            if (collisionTimer > 0f)
            {
                currentSpeed = collisionSpeed;
                collisionTimer -= Time.deltaTime;
            }
            else
            {
                currentSpeed = normalSpeed;
            }

            // Wキー（前方移動）
            if (Input.GetKey(KeyCode.W))
            {
                //transform.position += speed * transform.forward * Time.deltaTime;
                animator.SetBool("Slow Run", true);
            }

            // Sキー（後方移動）
            if (Input.GetKey(KeyCode.S))
            {
                //transform.position -= 15.0f/speed * transform.forward * Time.deltaTime;
                animator.SetBool("Unarmed Run Back", true);
            }

            // Dキー（右移動）
            if (Input.GetKey(KeyCode.D))
            {
                //transform.position += speed * transform.right * Time.deltaTime;
                animator.SetBool("Right Strafe", true);
            }

            // Aキー（左移動）
            if (Input.GetKey(KeyCode.A))
            {
                //transform.position -= speed * transform.right * Time.deltaTime;
                animator.SetBool("Left Strafe", true);
            }

            //キーを離したら
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("Slow Run", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("Unarmed Run Back", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("Right Strafe", false);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("Left Strafe", false);
            }
        }
    }

    // 衝突時の処理
    void OnCollisionEnter(Collision collision)
    {
        //壁と出口に衝突している間は
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Goal"))
        {
            // 速度を減少させ、一定時間制限する
            currentSpeed = collisionSpeed;
            collisionTimer = collisionDuration;
        }

        //敵に触れたら
        if (collision.gameObject.CompareTag("Enemy"))
        {
            death = true;
            Destroy(soul);
        }
    }

    void OnCollisionExit(Collision collision)
    {
            collisionTimer = 0;
    }
}
