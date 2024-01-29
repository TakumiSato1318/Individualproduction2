using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;

    //徘徊
    private float chargeTime = 7.0f;
    private float timeCount;

    //足音
    public AudioClip stepSound;
    public AudioClip gameoverSound;
    private AudioSource audioSource;//音源
    private Transform playerTransform;
    public float tempoMultiplier = 0.89f;

    //視界
    public float sightRange = 30f;//直線状の長さ
    public float fieldOfView = 60f;//範囲角度
    public LayerMask targetLayer;//対象のレイヤー
    public LayerMask obstacleLayer; // 障害物のレイヤー
    public float chaseSpeed = 3f; // 追いかける速さ

    //追尾
    private NavMeshAgent navMeshAgent;
    private Transform target; // 追いかける対象

    private bool chaseFlag=false;
    private bool pDeath;

    // 自身のTransform
    [SerializeField] private Transform myT;
    // ターゲットのTransform
    [SerializeField] private Transform targetT;

    public GameObject targetObject; // 存在を確認したいオブジェクト

    void Start()
    {
        pDeath = false;//プレイヤーの死亡

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        //プレイヤーが生きていたら
        if (pDeath ==false&& !targetObject.activeSelf)
        {
            timeCount += Time.deltaTime;
            // 自動前進
            transform.position += transform.forward * Time.deltaTime;

            // 指定時間の経過（条件）
            if (timeCount > chargeTime)
            {
                // 進路をランダムに変更する
                Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
                transform.localRotation = Quaternion.Euler(course);

                // タイムカウントを０に戻す
                timeCount = 0;
            }

            DetectTarget();

            //見つけたら
            if (target != null)
            {
                ChaseTarget();
                animator.SetBool("EnemyWalking", false);
                animator.SetBool("EnemyRunning", true);
                //足音を鳴らす
                PlayFootstepSound(stepSound);
            }
            else                    //見つかってないor逃げ切り
            {
                animator.SetBool("EnemyWalking", true);
                animator.SetBool("EnemyRunning", false);
                //足音を鳴らす
                PlayFootstepSound(stepSound);
            }
        }

        //プレイヤーに触れたら
        if (pDeath != false)
        {
            animator.SetBool("EnemyUppercut", true);

            // ターゲットの方向に自身を回転させる
            myT.LookAt(targetT);
        }
    }

    void DetectTarget()
    {
        Vector3 enemyPosition = transform.position;
        Ray ray = new Ray(enemyPosition, transform.forward);
        RaycastHit hit;

        //視界の範囲内かどうか
        if (Physics.Raycast(ray, out hit, sightRange, targetLayer))
        {
            Vector3 directionToTarget = (hit.point - enemyPosition).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < fieldOfView * 0.5f)
            {
                chaseFlag = true;
                target = hit.collider.transform;
            }
        }
        else
        {
            chaseFlag = false;
            target = null;
        }

        // 射線が障害物に当たった場合は無視
        //if (Physics.Raycast(ray, out hit, sightRange, obstacleLayer))
        //{
        //    target = null;
        //}
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        // 追いかける速さを設定
        navMeshAgent.speed = chaseSpeed;
    }

    //足音を鳴らす
    void PlayFootstepSound(AudioClip sound)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sound;

            // プレイヤーと足音ソースの距離に応じて音量を設定
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            float volume = Mathf.Clamp01(0.7f - distance / 35.0f);

            audioSource.volume = volume;
            audioSource.Play();
            
            //足音のテンポを変える
            if (chaseFlag==true)
            {
                ChangeTempo(1.05f);
            }
            else
            {
                ChangeTempo(0.89f);
            }
        }
    }

    void ChangeTempo(float multiplier)
    {
        // テンポの変更
        tempoMultiplier = multiplier;
        audioSource.pitch = tempoMultiplier;

        // サウンドの再生（停止中の場合は再生）
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    //敵とプレイヤーの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("当たった");
            pDeath = true;//プレイヤーの死亡
        }
    }
}