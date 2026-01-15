using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FieldArea : MonoBehaviour, ITriggerEvent
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int fieldSize = new Vector2Int(10, 10);
    private float tileSize = 2f;

    private Camera mainCamera;
    private GameObject[,] tileArray;
    [SerializeField] private GameObject currCrop;

    private IField field;
    private FieldSeed seed;
    private FieldHarvest harvest;

    private bool isInteraction;

    [SerializeField] private GameObject[] cropPrefab;
    
    [SerializeField] private GameObject fieldUI;
    [SerializeField] private GameObject actionUI;
    [SerializeField] private GameObject cropUI;
    
    [SerializeField] private Button seedButton;
    [SerializeField] private Button harvestButton;
    [SerializeField] private Button[] selectCropButtons;
    [SerializeField] private Button backButton;
    
    void Start()
    {
        Init();
        CreateField();
    }

    private void Init()
    {
        tileArray = new GameObject[fieldSize.x, fieldSize.y];

        seed = new FieldSeed();
        harvest = new FieldHarvest();
        
        seedButton.onClick.AddListener(() =>
        {
            field = seed;
            actionUI.SetActive(false);
            cropUI.SetActive(true);
        });
        
        harvestButton.onClick.AddListener(() =>
        {
            field = harvest;
        });

        for (int i = 0; i < selectCropButtons.Length; i++)
        {
            int j = i;  // 클로져 이슈 방지
            selectCropButtons[i].onClick.AddListener(() =>
            {
                seed.selectCrop = cropPrefab[j];
            });
        }
        
        backButton.onClick.AddListener(() =>
        {
            actionUI.SetActive(true);
            cropUI.SetActive(false);
            seed.selectCrop = null;
        });
    }
    
    public void InteractionEnter()
    {
        isInteraction = true;
        CameraManager.OnChangedCamera("Player", "Field");
        fieldUI.SetActive(true);
            
        StartCoroutine(FieldRoutine());
    }

    public void InteractionExit()
    {
        isInteraction = false;
        CameraManager.OnChangedCamera("Field", "Player");
        fieldUI.SetActive(false);
    }
    
    private void CreateField()
    {
        float offsetX = (fieldSize.x - 1) * tileSize / 2f;
        float offsetY = (fieldSize.y - 1) * tileSize / 2f;

        for (int i = 0; i < fieldSize.x; i++)
        {
            for (int j = 0; j < fieldSize.y; j++)
            {
                float posX = transform.position.x + i * tileSize - offsetX;
                float posZ = transform.position.z + j * tileSize - offsetY;

                GameObject tileObj = Instantiate(tilePrefab, transform); // transform 스케일 1

                tileObj.layer = 15; // Field Layer를 15로 설정

                tileObj.name = $"Tile_{i}_{j}";
                tileObj.transform.position = new Vector3(posX, 0, posZ);
                tileArray[i, j] = tileObj;

                tileObj.GetComponent<Tile>().arrayPos = new Vector2Int(i, j);
            }
        }
    }

    IEnumerator FieldRoutine()
    {
        while (isInteraction)
        {
            field?.FieldAction();
            yield return null;
        }
    }
}