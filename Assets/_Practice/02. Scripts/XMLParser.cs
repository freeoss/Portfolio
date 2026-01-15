using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLParser : MonoBehaviour
{
    [Serializable]
    public class CharacterData
    {
        public string CharID;
        public string Name;
        public int HP;
        public int Attack;

        public CharacterData() {}
        public CharacterData(string charID, string name, int HP, int attack)
        {
            this.CharID = charID;
            this.Name = name;
            this.HP = HP;
            this.Attack = attack;
        }
    }

    [Serializable]
    [XmlRoot("Characters")]
    public class CharacterList
    {
        [XmlElement("Character")]
        public List<CharacterData> characters;
    }

    [SerializeField] private List<CharacterData> characterDatas = new List<CharacterData>();

    private void Start()
    {
        TextAsset dataFile = Resources.Load<TextAsset>("XMLData");
        string data = dataFile.text;
        
        ParsingData(data);
    }

    void ParsingData(string data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterList));

        using (StringReader reader = new StringReader(data))
        {
            CharacterList loadedData = (CharacterList)serializer.Deserialize(reader);
            characterDatas = loadedData.characters;
        }

        foreach (var characterData in characterDatas)
        {
            Debug.Log($"{characterData.CharID} / {characterData.Name} / {characterData.HP} / {characterData.Attack}");
        }
    }
}
