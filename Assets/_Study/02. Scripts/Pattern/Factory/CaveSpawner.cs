using UnityEngine;

public class CaveSpawner : MonsterFactory
{
    protected override MonsterCore CreateMonster(string type)
    {
        MonsterCore monster = null;

        switch (type)
        {
            case "Bat":
                monster = new Dragon();
                break;
            case "Skeleton":
                monster = new Dragon();
                break;
            case "Dragon":
                monster = new Dragon();
                break;
        }

        return monster;
    }
}
