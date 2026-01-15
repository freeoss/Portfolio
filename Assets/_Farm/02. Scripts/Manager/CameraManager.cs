using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform clearShot;

    private static event Action<string, string> onChangedCamera;
    
    private Dictionary<string, CinemachineCamera> cameraDics = new Dictionary<string, CinemachineCamera>();

    private void Awake()
    {
        if (!clearShot)
        {
            return;
        }

        for (int i = 0; i < clearShot.childCount; i++)
        {
            Transform child = clearShot.GetChild(i);
            CinemachineCamera cam = child.GetComponent<CinemachineCamera>();

            if (!cameraDics.ContainsKey(child.name))
            {
                cameraDics.Add(child.name, cam);
            }
        }
    }

    private void OnEnable()
    {
        onChangedCamera += SetCamera;
    }

    private void OnDisable()
    {
        onChangedCamera -= SetCamera;
    }

    private void SetCamera(string from, string to)
    {
        cameraDics[from].Priority = 0;
        cameraDics[to].Priority = 10;
    }
    
    public static void OnChangedCamera(string from, string to)
    {
        onChangedCamera?.Invoke(from, to);
    }
}
