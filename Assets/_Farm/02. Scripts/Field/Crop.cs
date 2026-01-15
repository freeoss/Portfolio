using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum CropState { Level1, Level2, Level3 }
    public CropState cropState;

    [SerializeField] private CropData data;

    private float growthTime;
    private float growthTimeOrigin;

    void Awake()
    {
        growthTimeOrigin = data.growthTime;
        growthTime = growthTimeOrigin;
    }

    private void OnEnable()
    {
        SetState(CropState.Level1);
        WeatherSystem.weatherChanged += SetGrowth;
        
        StartCoroutine(GrowthRoutine());
    }

    private void OnDisable()
    {
        WeatherSystem.weatherChanged -= SetGrowth;
    }

    IEnumerator GrowthRoutine()
    {
        yield return new WaitForSeconds(growthTime);
        SetState(CropState.Level2);
        
        yield return new WaitForSeconds(growthTime);
        SetState(CropState.Level3);
    }

    private void SetState(CropState newState)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == (int)newState);
        }

        cropState = newState;
    }

    private void SetGrowth(WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.Sun:
                growthTime = growthTimeOrigin * 1f;
                break;
            case WeatherType.Rain:
                growthTime = growthTimeOrigin * 1.3f;
                break;
            case WeatherType.Snow:
                growthTime = growthTimeOrigin * 2f;
                break;
        }
    }

    public void SetCropData(out GameObject fruit, out int maxCount)
    {
        fruit = data.fruit;
        maxCount = data.maxFruitCount;
    }
}
