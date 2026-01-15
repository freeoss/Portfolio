using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;

    private Vector3 moveInput;
    private Vector3 moveVector;
    private Vector3 verticalVelocity;

    private float currSpeed;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    
    [SerializeField] private float rotSpeed = 10f;
    [SerializeField] private float gravity = -30f;

    [SerializeField] private GameObject inventoryUI;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Turn();
        SetAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();

        if (interactable != null)
        {
            interactable.InteractionEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();

        if (interactable != null)
        {
            interactable.InteractionExit();
        }
    }

    private void Move()
    {
        if (moveInput.magnitude >= 0.1f)
        {
            bool isRun = Input.GetKey(KeyCode.LeftShift);
            currSpeed = isRun ? runSpeed : walkSpeed;
            
            if (cc.isGrounded && verticalVelocity.y < 0)    // 바닥에 닿은 상태, 중력 초기화
            {
                verticalVelocity.y = -1f;
            }

            verticalVelocity.y += gravity * Time.deltaTime;

            moveVector = moveInput.normalized * currSpeed;

            Vector3 finalMovemont = (moveVector + verticalVelocity) * Time.deltaTime;
            cc.Move(finalMovemont);  // 캐릭터 컨트롤러 움직이는 기능
        }
        else
        {
            currSpeed = 0;
            cc.Move(verticalVelocity * Time.deltaTime);
        }
    }

    private void Turn()
    {
        if (moveInput.magnitude >= 0.1f)
        {
            Quaternion targetrotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotSpeed * Time.deltaTime);
        }
    }

    private void SetAnimation()
    {
        // anim.SetFloat("Speed", currSpeed);
        anim.SetFloat("Speed", currSpeed, 0.1f, Time.deltaTime);
    }

    private void OnMove(InputValue value)   // New Input System에서 데이터를 가져오는 기능 
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveInput = new Vector3(inputDir.x, 0, inputDir.y);
    }

    private void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            bool isActive = inventoryUI.gameObject.activeSelf;
            inventoryUI.gameObject.SetActive(!isActive);
        }
    }
}
