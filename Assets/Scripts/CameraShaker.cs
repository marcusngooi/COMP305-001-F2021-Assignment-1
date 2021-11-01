using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance { get; private set; }

    // private variables
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Instance = this;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        timer = time;
    }
}
