using UnityEngine;

public class StudyDelegate : MonoBehaviour
{
    public delegate void MyDelegate(string s);
    public MyDelegate onKeyDown;

    public KeyCode keyCode;
    
    void Start()
    {
        onKeyDown += Respond;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            string keyString = keyCode.ToString();
            onKeyDown?.Invoke(keyString);
        }
    }

    private void Respond(string str)
    {
        Debug.Log($"{str} 키 누름");
    }
}