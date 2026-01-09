using UnityEngine;

public class FieldArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.cameraAction?.Invoke(1, 11);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.cameraAction?.Invoke(1, 0);
        }
    }
}
