using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GoogleDataManager : MonoBehaviour
{
    [Serializable]
    public class CharacterData
    {
        [JsonProperty("ID")] public string characterID;
        [JsonProperty("Name")]public string name;
        [JsonProperty("Hp")]public int hp;
        [JsonProperty("Attack")]public int attack;
        
        public CharacterData(string characterID, string name, int hp, int attack)
        {
            this.characterID = characterID;
            this.name = name;
            this.hp = hp;
            this.attack = attack;
        }
    }

    private FirebaseDatabase database;
    private DatabaseReference reference;
    
    public string sheetURL;
    public string dbURL;
    
    public List<CharacterData>  characterDatas = new List<CharacterData>();

    private void Awake()
    {
        database = FirebaseDatabase.GetInstance(dbURL);
        reference = database.RootReference;
    }

    private void Start()
    {
        SheetToFirebase().Forget();
    }

    public async UniTaskVoid SheetToFirebase()
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetURL);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("데이터 로드 실패");
            return;
        }

        string csvData = www.downloadHandler.text;
        List<CharacterData> data = ParseCSV(csvData);

        string jsonData = JsonConvert.SerializeObject(data);

        await reference.Child("GameData").Child("Characters").SetRawJsonValueAsync(jsonData);
        
        Debug.Log("Firebase 저장 완료");

    }

    // Sheet에서 csv 받아오고 List로 파싱하는 기능
    private List<CharacterData> ParseCSV(string csvData)    
    {
        string[] lines = csvData.Split("\n");
    
        for (int i = 0; i < lines.Length; i++)
        {
            string[] rows = lines[i].Split(",");
    
            CharacterData newData = new CharacterData(rows[0], rows[1], int.Parse(rows[2]), int.Parse(rows[3]));
            characterDatas.Add(newData);
        }
    
        return characterDatas;
    }
    
    // private IEnumerator Start()
    // {
    //     UnityWebRequest www = UnityWebRequest.Get(URL);
    //     yield return www.SendWebRequest();
    //
    //     string data = www.downloadHandler.text;
    //     Debug.Log(data);
    //
    //     string[] lines = data.Split("\n");
    //
    //     for (int i = 0; i < lines.Length; i++)
    //     {
    //         string[] rows = lines[i].Split(",");
    //
    //         CharacterData newData = new CharacterData(rows[0], rows[1], int.Parse(rows[2]), int.Parse(rows[3]));
    //         characterDatas.Add(newData);
    //     }
    //
    //     Debug.Log($"파싱 완료 -> 총 {characterDatas.Count} 개 데이터 완료");
    // }
}
