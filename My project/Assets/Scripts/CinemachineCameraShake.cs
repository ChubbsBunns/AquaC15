using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraShake : MonoBehaviour
{
    public static CinemachineCameraShake instance;                                  //Singleton behaviour for convenient calling on multiple scripts
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;  //Cinemachine noise variable whose amplitude is changed for camera shake
    private float shakeTimer;
    private float shakeTotalTimer;
    private float startingIntensity;
    private void Awake()
    {
        instance = this;
        cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        //Set amplitude to intensity and start timer that lerps it to 0
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTotalTimer = time;
        startingIntensity = intensity;
    }

    private void Update()
    {
        //Lerp amplitude to 0
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(0f, startingIntensity, shakeTimer / shakeTotalTimer);
        }
    }
}
