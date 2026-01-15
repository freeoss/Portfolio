using UnityEngine;

public class PoolController : MonoBehaviour
{
    // public StudyObjectPool pool;
    public UnityPoolManager poolManager;
    public Transform shootPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // GameObject bullet = pool.DequeueObject();
            GameObject bullet = poolManager.pool.Get(); 
            bullet.transform.position = shootPoint.position;
            
            // 총알 발사 기능
        }
    }
}
