using UnityEngine;

public class HouseArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.cameraAction?.Invoke(2, 11);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.cameraAction?.Invoke(2, 0);
        }
    }
}
