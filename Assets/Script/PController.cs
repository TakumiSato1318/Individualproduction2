using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    public float normalSpeed = 8.0f;     // �ʏ�̑��x
    public float collisionSpeed = 0.01f;  // �Փˎ��̑��x
    public float collisionDuration = 1000f; // �Փ˒��̎���

    private float currentSpeed;         // ���݂̑��x
    private float collisionTimer = 0f;  // �Փ˃^�C�}�[

    private bool death;//���S

    public GameObject soul;
    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g

    //�A�j���[�V����
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
            // �L�[���͂��擾
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // �ړ��x�N�g��
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

            // ���[���h���W�n�ł̈ړ�
            transform.Translate(movement * currentSpeed * Time.deltaTime);

            // ���x�𐧌�
            float currentMagnitude = GetComponent<Rigidbody>().velocity.magnitude;
            if (currentMagnitude > normalSpeed)
            {
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * normalSpeed;
            }

            // �Փ˒��̑��x����
            if (collisionTimer > 0f)
            {
                currentSpeed = collisionSpeed;
                collisionTimer -= Time.deltaTime;
            }
            else
            {
                currentSpeed = normalSpeed;
            }

            // W�L�[�i�O���ړ��j
            if (Input.GetKey(KeyCode.W))
            {
                //transform.position += speed * transform.forward * Time.deltaTime;
                animator.SetBool("Slow Run", true);
            }

            // S�L�[�i����ړ��j
            if (Input.GetKey(KeyCode.S))
            {
                //transform.position -= 15.0f/speed * transform.forward * Time.deltaTime;
                animator.SetBool("Unarmed Run Back", true);
            }

            // D�L�[�i�E�ړ��j
            if (Input.GetKey(KeyCode.D))
            {
                //transform.position += speed * transform.right * Time.deltaTime;
                animator.SetBool("Right Strafe", true);
            }

            // A�L�[�i���ړ��j
            if (Input.GetKey(KeyCode.A))
            {
                //transform.position -= speed * transform.right * Time.deltaTime;
                animator.SetBool("Left Strafe", true);
            }

            //�L�[�𗣂�����
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

    // �Փˎ��̏���
    void OnCollisionEnter(Collision collision)
    {
        //�ǂƏo���ɏՓ˂��Ă���Ԃ�
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Goal"))
        {
            // ���x�����������A��莞�Ԑ�������
            currentSpeed = collisionSpeed;
            collisionTimer = collisionDuration;
        }

        //�G�ɐG�ꂽ��
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
