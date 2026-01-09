using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyLambda : MonoBehaviour
{
    public delegate void MyDelegate(string s);

    public MyDelegate myDelegate;

    public Button buttonUI;

    private List<int> numbers = new List<int>();

    public Button[] buttons;
    
    private void Start()
    {
        // myDelegate = (s) => Debug.Log(s);
        // myDelegate?.Invoke("Hello Unity");
        // buttonUI.onClick.AddListener(() => OnClick(20));

        for (int i = 0; i < 10; i++)
        {
            numbers.Add(i);
        }

        numbers.RemoveAll(n => n % 2 == 0);

        // 클로져(Closer) 이슈
        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => OnClick(j));
        }
    }

    public void OnLog()
    {
        // Debug.Log("Hello Unity");
    }

    public void OnClick(int id)
    {
        
    }
}
