using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeatherType
{
    Sun, Rain, Snow,
}
    

public class WeatherSystem : MonoBehaviour
{
    public WeatherType weatherType;

    [SerializeField] private GameObject[] weatherParticles;

    public static event Action<WeatherType> weatherChanged;
    
    IEnumerator Start()
    {
        int weatherCount = weatherParticles.Length;
        int ranIndex = Random.Range(0, weatherCount);
        
        while (true)
        {
            ranIndex = Random.Range(0, weatherParticles.Length);

            for (int i = 0; i < weatherCount; i++)
            {
                weatherParticles[i].SetActive(ranIndex == i);
            }

            weatherType = (WeatherType)ranIndex;
            
            weatherChanged?.Invoke(weatherType);
            
            yield return new WaitForSeconds(10f);
        }
    }
}
