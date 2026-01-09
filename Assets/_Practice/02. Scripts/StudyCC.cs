using System;
using UnityEngine;

public class StudyCC : MonoBehaviour
{
    private CharacterController cc;

    private Vector3 verticalVelocity;
    
    private float moveSpeed = 5f;
    private float rotSpeed = 10f;

    public float gravity = -30f;
    public float jumpPower = 10f;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized; // 입력 방향
        Vector3 moveVector = Vector3.zero;

        // if (inputDir != Vector3.zero)
        if (inputDir.magnitude >= 0.1f) // DeadZone 설정
        {
            moveVector = inputDir * moveSpeed;
            
            Quaternion targetRot =  Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }

        Jump();

        Vector3 finalVector = (moveVector + verticalVelocity) * Time.deltaTime;
        cc.Move(finalVector);

        // CollisionFlags flags = cc.Move(finalVector);
        //
        // if ((flags & CollisionFlags.Below) != 0)
        //     Debug.Log("발에 닿음");
        //
        // if ((flags & CollisionFlags.Above) != 0)
        //     Debug.Log("머리에 닿음");
        //
        // if ((flags & CollisionFlags.Sides) != 0)
        //     Debug.Log("옆에 닿음");
    }

    // private void OnCollisionEnter(Collision other) // 동작 X
    // {
    //     Debug.Log("충돌 이벤트");
    // }

    private void OnTriggerEnter(Collider other) // 동작 O
    {
        Debug.Log("감지 이벤트");
    }

    private void Jump()
    {
        if (cc.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -1f;
        
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
            verticalVelocity.y = jumpPower;
        
        verticalVelocity.y += gravity * Time.deltaTime; // 가상 중력
    } 
}