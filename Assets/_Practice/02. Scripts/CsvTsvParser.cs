using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CsvTsvParser : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string CharID;
        public string Name;
        public int HP;
        public int Attack;

        public CharacterData(string charID, string name, int HP, int attack)
        {
            this.CharID = charID;
            this.Name = name;
            this.HP = HP;
            this.Attack = attack;
        }
    }

    [SerializeField] private List<CharacterData> characterDatas = new List<CharacterData>();

    private void Start()
    {
        TextAsset dataFile = Resources.Load<TextAsset>("TSVData");
        string data = dataFile.text;
        
        ParsingData(data);
    }

    void ParsingData(string data)
    {
        string[] rows = data.Split('\n');   // 단락 변경 기준으로 자르기

        foreach (string row in rows)
        {
            Debug.Log(row);
        }
        Debug.Log(rows.Length);
        for (int i = 1; i < rows.Length; i++)
        {Debug.Log(i);
            string row = rows[i].Trim();    // 공백 제거
            string[] col = row.Split('\t');  // 콤마 기준으로 자르기
            Debug.Log("col: count: " + col.Length);
            CharacterData characterData = new CharacterData(col[0], col[1], int.Parse(col[2]), int.Parse(col[3]));
            characterDatas.Add(characterData);
        }
    }
}
