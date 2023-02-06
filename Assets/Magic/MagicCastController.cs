using Platformer.Magic.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCastController : MonoBehaviour
{

    [SerializeField] private GameObject _magicFire;
    [SerializeField] private MagicShieldController _magicShield;
    [SerializeField] private Transform _originOfMagic;
    [SerializeField] private PlayerStatus _playerStatusController;
    [SerializeField] private PlayerController _playerController;
    public float charging = 1;
    public float totalCharge = 3;
    public float totalChargeTime = 2;

    public Action<MagicEnum> OnplayerCastMagicByBtn;
    public Action<MagicEnum, int> OnplayerCastMagicChargindValue;

    [SerializeField] private bool _isCharging;


    private void OnEnable()
    {
        _playerController.OnplayerFlip += Flip;
    }

    private void OnDisable()
    {
        _playerController.OnplayerFlip -= Flip;
    }

    public void BtnChargingDown()
    {
        _isCharging = true;
    }       

    public void BtnCastFire()
    {
        _magicFire.transform.GetChild(0).localScale = Vector3.one;
        _isCharging = false;
        if (charging > 5)
            charging = 3;
        _magicFire.transform.GetChild(0).localScale *= charging;
        OnplayerCastMagicByBtn?.Invoke(MagicEnum.Fire);
        OnplayerCastMagicChargindValue?.Invoke(MagicEnum.Fire, (int) charging);
        Debug.Log("Dispara Fire Iniciado Pelo BTN");
        charging = 0;
    }
    public void BtnCastMagicByName(String name)
    {
        MagicEnum magicEnum;
        Enum.TryParse<MagicEnum>(name, out magicEnum);

        _isCharging = false;
        if (charging > 5)
            charging = 3;
        OnplayerCastMagicByBtn?.Invoke(magicEnum);
        OnplayerCastMagicChargindValue?.Invoke(magicEnum, (int)charging);
        Debug.Log("Dispara name Iniciado Pelo BTN"+ name);
    }
    public void CastFire()
    {
        
        _magicFire.transform.position = _originOfMagic.position;
        _magicFire.transform.localScale = _originOfMagic.localScale;
        _magicFire.gameObject.SetActive(true);
        Debug.Log("Dispara Fire Mafia FIre Iniciada");

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
        
        if (_isCharging == (true))
        {
            charging += Time.deltaTime * ((totalCharge - 1) / totalChargeTime);
        }
        charging = Mathf.Clamp(charging, 1, totalCharge);

    }


}
public enum MagicEnum
{
    Fire,
    Fireball,
    Shield,
    MagicIce,
    Comet,
    Winter
}
