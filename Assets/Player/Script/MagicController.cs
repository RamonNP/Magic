using Platformer.Magic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{

    [SerializeField] private GameObject _magicFire;
    [SerializeField] private MagicShieldController _magicShield;
    [SerializeField] private Transform _originOfMagic;
    [SerializeField] private PlayerStatusController _playerStatusController;
    [SerializeField] private PlayerController _playerController;
    public float charging = 1;
    public float totalCharge = 3;
    public float totalChargeTime = 2;

    [SerializeField] private bool _isFireCharging;


    private void OnEnable()
    {
        _playerController.OnplayerFlip += Flip;
    }

    private void OnDisable()
    {
        _playerController.OnplayerFlip -= Flip;
    }

    public void BtnFireChargingDown()
    {
        Debug.Log("DOWN");
        _isFireCharging = true;
    }    

    public void BtnCastFire()
    {
        Debug.Log("UP");
        _magicFire.transform.GetChild(0).localScale = Vector3.one;
        _isFireCharging = false;
        if (charging > 5)
            charging = 3;
        _magicFire.transform.GetChild(0).localScale *= charging;
        charging = 0;
       _playerController.CastFire();

    }
    public void CastFire()
    {
        
        _magicFire.transform.position = _originOfMagic.position;
        _magicFire.transform.localScale = _originOfMagic.localScale;
        _magicFire.gameObject.SetActive(true);
        
    }
    public void CastShield()
    {
        if(_playerStatusController.Isflying)
        {
            _magicShield.DisableShield();
        } else
        {
            _playerController.MoveUpFly();
            _magicShield.EnableShield();
        }
        _playerStatusController.ToFly(!_playerStatusController.Isflying);
    }

    public void Flip(bool flip)
    {
        if (flip)
        {
            _originOfMagic.transform.localScale = new Vector3(-1, 1, 1);
        }
        else 
        {
            _originOfMagic.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void Update()
    {
        if (_isFireCharging == (true))
        {
            charging += Time.deltaTime * ((totalCharge - 1) / totalChargeTime);
        }
        charging = Mathf.Clamp(charging, 1, totalCharge);




    }

}
