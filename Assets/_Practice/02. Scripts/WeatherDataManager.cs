using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class WeatherDataManager : MonoBehaviour
{
    #region JSON 데이터 클래스
    [Serializable]
    public class WeatherApiResponse
    {
        public Response response;
    }
    

    [Serializable]
    public class Response
    {
        public Header header;
        public Body body;
    }

    [Serializable]
    public class Header
    {
        public string resultCode;
        public string resultMsg;
    }

    [Serializable]
    public class Body
    {
        public string dataType;
        public Items items;
        public string pageNo;
        public string numOfRows;
        public string totalCount;
    }

    [Serializable]
    public class Items
    {
        public List<Item> item;
    }

    [Serializable]
    public class Item
    {
        public string baseDate;
        public string baseTime;
        public string category;
        public string fcstDate;
        public string fcstTime;
        public string fcstValue;
        public string nx;
        public string ny;
    }
    #endregion
    
    private string URL = "https://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst?";

    public string key;
    public string pageNo;
    public string numOfRows;
    public string dataType;
    public string baseDate;
    public string baseTime;
    public string axisX, axisY;

    IEnumerator Start()
    {
        URL += $"serviceKey={key}&pageNo={pageNo}&numOfRows={numOfRows}&dataType={dataType}&base_date={baseDate}&base_time={baseTime}&nx={axisX}&ny={axisY}";
            
        UnityWebRequest www = UnityWebRequest.Get(URL);
        Debug.Log(www.url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("www.error : " + www.error);
        }
        else
        {
            string rawData = www.downloadHandler.text;
            Debug.Log("전달받은 데이터 : " + rawData);

            WeatherApiResponse weatherData = JsonUtility.FromJson<WeatherApiResponse>(rawData);

            foreach (var item in weatherData.response.body.items.item)
            {
                if (item.category == "PCP")
                {
                    Debug.Log($"강수 : {item.fcstValue}");
                }
                else if (item.category == "SNO")
                {
                    Debug.Log($"적설 : {item.fcstValue}");
                }
            }
        }
    }
}
