using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    private CharacterController cc;

    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public float rotSpeed = 10f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (moveInput.magnitude >= 0.1f)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
            cc.Move(moveDir * moveSpeed * Time.deltaTime); // 움직이는 기능

            Quaternion targetRot =  Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime); // 회전하는 기능
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
    
}