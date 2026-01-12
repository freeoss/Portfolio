using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int arrayPos;

    private bool isCreate = false;
    
    public void CreateCrop(GameObject cropPrefab)
    {
        if (isCreate)
        {
            return;
        }

        isCreate = true;

        GameObject cropObj = Instantiate(cropPrefab);
        cropObj.transform.SetParent(transform);
        cropObj.transform.localPosition = Vector3.zero;
    }
}
