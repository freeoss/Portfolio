using UnityEngine;
using UnityEngine.EventSystems;

public class FieldHarvest : IField
{
    private Camera mainCamera;

    public FieldHarvest()
    {
        mainCamera = Camera.main;
    }
    
    public void FieldAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, 1 << 15))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                int tileX = tile.arrayPos.x;
                int tileY = tile.arrayPos.y;
                
                tile.HarvestCrop();
            }
        }
    }
}
