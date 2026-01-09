using UnityEngine;

public class StudyParameter : MonoBehaviour
{
    public int number = 10;
    public int number1 = 10;
    public int number2 = 10;

    public int[] numbers = { 1, 2, 3, 4, 5 };

    public GameObject houseRoof;
    public GameObject postUI;
    public GameObject npc;

    void Start()
    {
        // NormalParameter(number);
        
        // RefParameter(ref number);
        
        // OutParameter(out number, out number1, out number2);
        
        // ArrayParameter(numbers);
        
        // ParamsParameter(1, 2, 3, 4, 5);
        // ParamsParameter(10, 20, 30);
        // ParamsParameter(numbers);
        
        SetActiveGameObject(true, houseRoof, postUI, npc); // 매개변수로 들어간 모든 오브젝트 켜기
        SetActiveGameObject(false, houseRoof); // 매개변수로 들어간 모든 오브젝트 끄기
    }

    private void NormalParameter(int n)
    {
        Debug.Log("호출 전 : " + n);
        n = 100;

        Debug.Log("호출 후: " + number);
    }

    private void RefParameter(ref int n)
    {
        Debug.Log("호출 전 : " + n);
        n = 100;

        Debug.Log("호출 후: " + number);
    }

    private void OutParameter(out int n, out int n1, out int n2)
    {
        n = 100;
        n1 = 100;
        n2 = 100;
    }

    private void ArrayParameter(int[] nums)
    {
        string str = "";
        foreach (var num in nums)
            str += num + " ";

        Debug.Log(str);
    }

    private void ParamsParameter(params int[] nums)
    {
        string str = "";
        foreach (var num in nums)
            str += num + " ";

        Debug.Log(str);
    }

    private void SetActiveGameObject(bool isActive, params GameObject[] objs)
    {
        foreach (var obj in objs)
            obj.SetActive(isActive);
    }
}