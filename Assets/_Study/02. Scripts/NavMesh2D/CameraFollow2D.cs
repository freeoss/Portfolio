using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = player.position + offset;
    }
}
