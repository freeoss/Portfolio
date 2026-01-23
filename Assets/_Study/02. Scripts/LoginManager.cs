using System.Collections.Generic;
using Firebase.Database;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public class User
    {
        public string NickName;
        public string UnitList;
        public int Level;
        public int Damage;
        public int Gold;

        public User(string nickName, string unitList, int level, int damage, int gold)
        {
            this.NickName = nickName;
            this.UnitList = unitList;
            this.Level = level;
            this.Damage = damage;
            this.Gold = gold;
        }
    }

    private FirebaseDatabase database;
    private DatabaseReference reference;
    private UnityMainThreadDispatcher dispatcher;

    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button loginButton;

    private string NickName;
    private int Level = 1;
    private int Damage = 10;
    private int Gold = 100;

    private Dictionary<string, bool> unitDics = new Dictionary<string, bool>();

    private void Start()
    {
        database = FirebaseDatabase.GetInstance("https://hellofirebase-9303f-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();
        
        unitDics.Add("Unit0", false);
        
        loginButton.onClick.AddListener(CreateData);
    }

    private void CreateData()
    {
        NickName = nickNameInput.text;
        reference.Child("UserInfo").OrderByChild("NickName").EqualTo(NickName).GetValueAsync().ContinueWith(task =>
        {
            Debug.Log("결과 받음");
            if (task.IsFaulted)
            {
                Debug.Log("task.IsFaulted");
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("task.IsCompleted");
                
                DataSnapshot snapshot = task.Result;
                Debug.Log("task.IsCompleted");
                Debug.Log("task.IsCompleted");

                if (snapshot.HasChildren)   // 이미 닉네임(키)이 있을 때
                {
                    dispatcher.Enqueue(() =>
                    {
                        infoText.text = "중복된 닉네임 입니다. ";
                    });
                }
                else
                {
                    Debug.Log("task.IsCompleted: 실패");
                
                    if (NickName.Length < 1)
                    {
                        dispatcher.Enqueue(() => { infoText.text = "닉네임을 입력하세요"; });
                    }
                    else
                    {
                        string unitList = JsonConvert.SerializeObject(unitDics);

                        User user = new User(NickName, unitList, Level, Damage, Gold);
                        string jsonData = JsonConvert.SerializeObject(user);

                        reference.Child("UserInfo").Push().SetRawJsonValueAsync(jsonData).ContinueWith(task1 =>
                        {
                            if (task1.IsFaulted)
                            {
                                Debug.Log("업로드 실패");
                                return;
                            }
                            else if (task1.IsCompleted)
                            {
                                Debug.Log("업로드 성공");
                                SceneManager.LoadScene(1);
                            }
                        });
                    }
                }
            }
        });
    }
}
