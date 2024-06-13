using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 500f; // 마우스 민감도
    public float rotationX; // X 축의 위치
    public float rotationY; // Y 축의 위치

    void Update() {
        float MouseMoveX = Input.GetAxisRaw("Mouse X"); // 마우스 X축 움직임 값
        float MouseMoveY = Input.GetAxisRaw("Mouse Y"); // 마우스 Y축 움직임 값

        rotationY += MouseMoveX * sesitivity * Time.deltaTime;
        rotationX += MouseMoveY * sesitivity * Time.deltaTime;

        if(rotationX > 35f) {
            rotationX = 35f; // 위로 35도 이상 고개가 넘어가기 못하게
        }

        if(rotationX < -30f) {
            rotationX = -30f; // 아래로 30도 이상 고개가 내려가지 못하게
        }

        // eulerAngles : 오브젝트를 회전시키는 함수
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}
