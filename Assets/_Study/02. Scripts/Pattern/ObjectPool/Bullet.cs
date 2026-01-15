using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public StudyObjectPool pool;
    private UnityPoolManager poolManager;

    private void Awake()
    {
        poolManager = FindFirstObjectByType<UnityPoolManager>();
    }

    private void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }

    private void ReturnPool()
    {
        // pool.EnqueueObject(gameObject);
        poolManager.pool.Release(gameObject);
    }
}
