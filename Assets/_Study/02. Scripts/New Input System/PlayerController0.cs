using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController0 : MonoBehaviour
{
    private CharacterController cc;

    private Vector2 moveInput;
    
    private Vector3 verticalVelocity;
    
    private float moveSpeed = 5f;
    private float rotSpeed = 10f;

    public float gravity = -30f;
    public float jumpPower = 10f;
    
    public InputActionAsset inputAsset;

    private InputAction move;
    private InputAction attack;
    private InputAction jump;
    private InputAction interaction;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        
        move = inputAsset.actionMaps[0].actions[0];
        attack = inputAsset.actionMaps[0].actions[1];
        jump = inputAsset.actionMaps[0].actions[2];
        interaction = inputAsset.actionMaps[0].actions[3];

        // move = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        moveInput = move.ReadValue<Vector2>();
        
        Vector3 inputDir = new Vector3(moveInput.x, 0, moveInput.y).normalized; // 입력 방향
        Vector3 moveVector = Vector3.zero;

        if (inputDir.magnitude >= 0.1f)
        {
            moveVector = inputDir * moveSpeed;
            
            Quaternion targetRot =  Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }

        if (cc.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -1f;
        
        if (attack.WasPressedThisFrame()) // 1번 실행
            Debug.Log("Attack");

        if (jump.WasPressedThisFrame() && cc.isGrounded) // 1번 실행
            verticalVelocity.y = jumpPower;
        
        if (interaction.IsPressed()) // 여러번 실행
            Debug.Log("Interact");
        
        verticalVelocity.y += gravity * Time.deltaTime;
        
        Vector3 finalVector = (moveVector + verticalVelocity) * Time.deltaTime;
        cc.Move(finalVector);
    }
}