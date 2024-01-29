using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;

    //�p�j
    private float chargeTime = 7.0f;
    private float timeCount;

    //����
    public AudioClip stepSound;
    public AudioClip gameoverSound;
    private AudioSource audioSource;//����
    private Transform playerTransform;
    public float tempoMultiplier = 0.89f;

    //���E
    public float sightRange = 30f;//������̒���
    public float fieldOfView = 60f;//�͈͊p�x
    public LayerMask targetLayer;//�Ώۂ̃��C���[
    public LayerMask obstacleLayer; // ��Q���̃��C���[
    public float chaseSpeed = 3f; // �ǂ������鑬��

    //�ǔ�
    private NavMeshAgent navMeshAgent;
    private Transform target; // �ǂ�������Ώ�

    private bool chaseFlag=false;
    private bool pDeath;

    // ���g��Transform
    [SerializeField] private Transform myT;
    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform targetT;

    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g

    void Start()
    {
        pDeath = false;//�v���C���[�̎��S

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        //�v���C���[�������Ă�����
        if (pDeath ==false&& !targetObject.activeSelf)
        {
            timeCount += Time.deltaTime;
            // �����O�i
            transform.position += transform.forward * Time.deltaTime;

            // �w�莞�Ԃ̌o�߁i�����j
            if (timeCount > chargeTime)
            {
                // �i�H�������_���ɕύX����
                Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
                transform.localRotation = Quaternion.Euler(course);

                // �^�C���J�E���g���O�ɖ߂�
                timeCount = 0;
            }

            DetectTarget();

            //��������
            if (target != null)
            {
                ChaseTarget();
                animator.SetBool("EnemyWalking", false);
                animator.SetBool("EnemyRunning", true);
                //������炷
                PlayFootstepSound(stepSound);
            }
            else                    //�������ĂȂ�or�����؂�
            {
                animator.SetBool("EnemyWalking", true);
                animator.SetBool("EnemyRunning", false);
                //������炷
                PlayFootstepSound(stepSound);
            }
        }

        //�v���C���[�ɐG�ꂽ��
        if (pDeath != false)
        {
            animator.SetBool("EnemyUppercut", true);

            // �^�[�Q�b�g�̕����Ɏ��g����]������
            myT.LookAt(targetT);
        }
    }

    void DetectTarget()
    {
        Vector3 enemyPosition = transform.position;
        Ray ray = new Ray(enemyPosition, transform.forward);
        RaycastHit hit;

        //���E�͈͓̔����ǂ���
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

        // �ː�����Q���ɓ��������ꍇ�͖���
        //if (Physics.Raycast(ray, out hit, sightRange, obstacleLayer))
        //{
        //    target = null;
        //}
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        // �ǂ������鑬����ݒ�
        navMeshAgent.speed = chaseSpeed;
    }

    //������炷
    void PlayFootstepSound(AudioClip sound)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sound;

            // �v���C���[�Ƒ����\�[�X�̋����ɉ����ĉ��ʂ�ݒ�
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            float volume = Mathf.Clamp01(0.7f - distance / 35.0f);

            audioSource.volume = volume;
            audioSource.Play();
            
            //�����̃e���|��ς���
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
        // �e���|�̕ύX
        tempoMultiplier = multiplier;
        audioSource.pitch = tempoMultiplier;

        // �T�E���h�̍Đ��i��~���̏ꍇ�͍Đ��j
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    //�G�ƃv���C���[�̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("��������");
            pDeath = true;//�v���C���[�̎��S
        }
    }
}