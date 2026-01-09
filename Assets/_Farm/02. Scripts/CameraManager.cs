using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Action<int, int> cameraAction;
    
    [SerializeField] private CinemachineClearShot clearShot;
    [SerializeField] private CinemachineCamera[] cameras;

    private void OnEnable()
    {
        cameraAction += SetCamera;
    }

    private void OnDisable()
    {
        cameraAction -= SetCamera;
    }

    private void Start()
    {
        cameras = clearShot.GetComponentsInChildren<CinemachineCamera>();
    }

    public void SetCamera(int index, int priority)
    {
        cameras[index].Priority = priority;
    }
}
