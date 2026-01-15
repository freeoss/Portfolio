using UnityEngine;
using UnityEngine.Pool;

public class UnityPoolManager : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public GameObject preFab;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateObject, GetObject, ReleaseObject);
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(preFab);
        Debug.Log("오브젝트 생성");
        
        return obj;
    }

    public void GetObject(GameObject obj)
    {
        Debug.Log("오브젝트 꺼내기");
        obj.SetActive(true);
    }

    private void ReleaseObject(GameObject obj)
    {
        Debug.Log("오브젝트 넣기");
        obj.SetActive(false);
    }
}
