using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Animator anicon_PicoChan;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Transform character;
    [SerializeField] Animator anicon;
    [SerializeField] float moveSpeed_set; // 이동 속도
    [SerializeField] int attackRange;
    [SerializeField] int attackAngle;

    public float jumpPower=5; // 점프력
    public int MaxJumpCount=3; // 최대 점프 횟수
    [SerializeField] int nowJumpCount=0; // 현재 점프 횟수

    void Awake()
    {
        nowJumpCount = MaxJumpCount;
    }

    void Attack()
    {
        anicon_PicoChan.SetBool("isPunch", Input.GetKey(KeyCode.E));
    }

    public void AttackMonster()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitColliders)
        {
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                Vector3 directionToTarget = (monster.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(transform.forward, directionToTarget);

                float angleThreshold = Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad);

                if (dot >= angleThreshold)
                {
                    // 범위 내 몬스터에게 피해
                    monster.Damaged();
                }
            }
        }
    }

    // 공격 범위 시각화 (Scene 뷰에서만 보임)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 forward = transform.forward;
        Quaternion leftRotation = Quaternion.Euler(0, -attackAngle / 2, 0);
        Quaternion rightRotation = Quaternion.Euler(0, attackAngle / 2, 0);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * attackRange);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && 0 < nowJumpCount)
        {
            rigid.velocity = Vector3.up * jumpPower;
            nowJumpCount--;
            //anicon_PicoChan.SetTrigger("jump");
            anicon_PicoChan.SetBool("jumpEnd", false);
        }

        if (rigid.velocity.y <= 0 && Physics.Raycast(character.position + (Vector3.up * 0.1f), Vector3.down, 0.2f, LayerMask.GetMask("Ground")))
        {
            nowJumpCount = MaxJumpCount;
            anicon_PicoChan.SetBool("jumpEnd", true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        // 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        // 애니메이터
        bool isWalk = 0 < moveDirection.magnitude;
        // moveDirection.magnitude : 백터의 길이를 반환합니다.
        // 입력 값을 받으면 백터의 길이가 0보다 커지면서 True를 반환합니다.
        anicon_PicoChan.SetBool("isWalk", isWalk);
        // anicon_PicoChan이라는 애니메이터를 담을 변수를 생성합니다.
        // Bool 타입의 Parameter를 생성하였기에 SetBool함수를 사용합니다.

        anicon_PicoChan.SetBool("isKick", Input.GetMouseButtonDown(0));

        //anicon_PicoChan.SetBool("isPunch", Input.GetKey(KeyCode.E));

        Jump();

        Attack();

    }
}
