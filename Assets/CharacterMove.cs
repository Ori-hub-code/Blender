using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterMove characterController; // CharacterController 에 3D 오브젝트를 적용하기 위한 변수
    public float moveSpeed; // 이동속도
    public float yVelocity = 0; // Y축 움직임


    float hAxis;
    float vAxis;

    bool isSide; //벽 충돌 유무
    Vector3 sideVec; //벽 충돌 방향 저장

    Vector3 moveVec;

    void Update()
    {
        GetInput();
        Move();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= new Vector3(0, 0.1f, 0);
        }
        else if(Input.GetKey(KeyCode.Space)) 
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        // normalized : 방향 값이 1로 보정된 벡터 -> 대각선 값을 고정시키기 위해 사용. 원래 대각선 값은 루트2가 나옴
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // TransformDirection : 해당 transform 의 로컬공간에서의 벡터를 월드공간에서의 벡터로 바꿔주는 함수.
        moveVec = transform.TransformDirection(moveVec);

        moveVec *= moveSpeed;
        moveVec.y = 0; // 위/아래로 이동이 안되게 y 값 고정


        //충돌하는 방향은 무시
        if (isSide && moveVec == sideVec)
        {
            moveVec = Vector3.zero;
        }

        // 달리기
        transform.position += moveVec * moveSpeed * Time.deltaTime;

    }

    void Turn()
    {
        // 회전 (방향전환)
        // LookAt() : 지정된 벡터를 향해서 회전시키는 함수
        transform.LookAt(transform.position + moveVec);
    }

    //벽 충돌 In 체크
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = true;
            sideVec = moveVec;
        }
    }

    //벽 충돌 Out 체크
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = false;
            sideVec = Vector3.zero;
        }
    }
}
