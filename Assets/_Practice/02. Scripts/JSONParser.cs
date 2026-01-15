using System;
using System.Collections.Generic;
using UnityEngine;

public class JSONParser : MonoBehaviour
{
    [Serializable]
    public class CharacterListWrapper
    {
        public List<CharacterData> characters;
    }
    
    [Serializable]
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
        TextAsset dataFile = Resources.Load<TextAsset>("JSONData");
        string data = dataFile.text;
        
        ParsingData(data);
    }

    void ParsingData(string data)
    {
        CharacterListWrapper wrapper = JsonUtility.FromJson<CharacterListWrapper>(data);

        foreach (var characterData in wrapper.characters)
        {
            characterDatas.Add(characterData);
        }
    }
}
