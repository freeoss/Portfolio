using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 100f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
}
