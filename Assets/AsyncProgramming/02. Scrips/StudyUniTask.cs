using Cysharp.Threading.Tasks;
using UnityEngine;

public class StudyUniTask : MonoBehaviour
{
    
    
    private void Start()
    {
        BackgroudJob().Forget();    // 알아서 동작하는 기능
        
        SubMethod();
    }

    void SubMethod()
    {
        Debug.Log("서브 작업 실행");
    }

    private async UniTaskVoid BackgroudJob()
    {
        Debug.Log("백그라운드 작업 시작");

        await UniTask.WaitForSeconds(3f);
        
        Debug.Log("백그라운드 작업 종료");
    }
}
