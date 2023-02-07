using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{
    [SerializeField] private GameObject _chargingEffects;
    [SerializeField] private MagicCastController _magicController;


    private void Update()
    {
        Debug.Log(_magicController.Charging);
        if (_magicController.Charging > 1.5f)
        {
            _chargingEffects.SetActive(true);
        } else
        {
            _chargingEffects.SetActive(false);
        }
    }

    public void PlayCharging()
    {
        
    }
}
