using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 moveDir;
    private float moveSpeed = 5f;
    private float rotSpeed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveDir = new Vector3(h, v, 0);
    }

    private void LateUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;

        if (moveDir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(transform.forward, moveDir);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, rotSpeed));
        }
    }
}
