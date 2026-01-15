using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
    public GameObject fruit;
    public float growthTime;
    public int maxFruitCount;
}
