using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HitCameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CombatController _combatController;
    [SerializeField] private float shakeIntensity = 2f;
    [SerializeField] private float shakeDuration = 0.2f;

    private void OnEnable()
    {
        _combatController.OnHitShake += StartShake;
    }

    private void OnDisable()
    {
        _combatController.OnHitShake -= StartShake;
    }

    private void StartShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;
            noise.m_AmplitudeGain = x;
            noise.m_FrequencyGain = y;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}
