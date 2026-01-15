using System;
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
    }
}
