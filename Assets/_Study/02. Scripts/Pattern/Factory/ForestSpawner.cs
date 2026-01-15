using System.Collections;
using UnityEngine;

public class ForestSpawner : MonsterFactory
{
    protected override MonsterCore CreateMonster(string type)
    {
        MonsterCore monster = null;

        switch (type)
        {
            case "Slime":
                monster = new Slime();
                break;
            case "Orc":
                monster = new Orc();
                break;
        }

        return monster;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            int ranMonster = Random.Range(0, 2);
            string monsterType = ranMonster == 0 ? "Slime" : "Orc";
            Spawn(monsterType);

            yield return new WaitForSeconds(1f);
        }
    }
}
