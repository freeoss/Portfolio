using System;
using Farm;
using UnityEngine;

public class Fruit : MonoBehaviour, ITriggerEvent, IItem
{
    public Inventory Inven { get; private set; }
    public GameObject Obj { get; private set; }
    public string ItemName { get; private set; }
    
    [field: SerializeField]
    public Sprite Icon { get; private set; }

    private void Awake()
    {
        Inven = FindFirstObjectByType<Inventory>();
        Obj = gameObject;
        
        ItemName = gameObject.name.Replace("(Clone)", "");
    }

    public void InteractionEnter()
    {
        Get();
    }

    public void InteractionExit() { }
    
    public void Get()
    {
        PoolManager.Instance.ReleaseObject(ItemName, gameObject);
        Debug.Log(gameObject.name + "을 획득했습니다.");
        
        // Inventory에 획득한 정보 전달
        Inven.GetItem(this);
    }

    public void Use()
    {
        switch (ItemName)
        {
            case "Carrot_Fruit":
                DataManager.Instance.SetGold(50).Forget();
                break;
            case "Corn_Fruit":
                DataManager.Instance.SetGold(20).Forget();
                break;
            case "Eggplant_Fruit":
                DataManager.Instance.SetGold(30).Forget();
                break;
            case "Pumpkin_Fruit":
                DataManager.Instance.SetGold(10).Forget();
                break;
            case "Tomato_Fruit":
                DataManager.Instance.SetGold(51).Forget();
                break;
            case "Turnip_Fruit":
                DataManager.Instance.SetGold(60).Forget();
                break;
        }
    }
}
