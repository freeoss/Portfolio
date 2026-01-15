using UnityEngine;

public class FieldCamera : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 offset, minBounds, maxBounds;
    [SerializeField] private float smoothSpeed = 5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        Vector3 destination = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.localPosition, destination, smoothSpeed);

        smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x);
        smoothPosition.z = Mathf.Clamp(smoothPosition.z, minBounds.z, maxBounds.z);

        transform.position = smoothPosition;
    }
}
