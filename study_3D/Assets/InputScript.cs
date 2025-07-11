using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Animator anicon_PicoChan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // �Է�
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // �̵�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ȸ��
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        // �ִϸ�����
        bool isWalk = 0 < moveDirection.magnitude;
        // moveDirection.magnitude : ������ ���̸� ��ȯ�մϴ�.
        // �Է� ���� ������ ������ ���̰� 0���� Ŀ���鼭 True�� ��ȯ�մϴ�.
        anicon_PicoChan.SetBool("isWalk", isWalk);
        // anicon_PicoChan�̶�� �ִϸ����͸� ���� ������ �����մϴ�.
        // Bool Ÿ���� Parameter�� �����Ͽ��⿡ SetBool�Լ��� ����մϴ�.

        anicon_PicoChan.SetBool("isKick", Input.GetMouseButtonDown(0));

        anicon_PicoChan.SetBool("isPunch", Input.GetKey(KeyCode.Space));

    }
}
