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
    [SerializeField] private PlayerStatusController _playerStatusController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _charging = 1;
    public float totalCharge = 3;
    public float totalChargeTime = 2;

    public Action<MagicEnum> OnplayerCastMagicByBtn;
    public Action<MagicEnum, int> OnplayerCastMagicChargindValue;
    public Action<bool> OnplayerChargind;

    [SerializeField] private bool _isCharging;

    public float Charging { get => _charging; set => _charging = value; }

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
        OnplayerChargind?.Invoke(_isCharging);
    }       

    public void BtnCastFire()
    {
        _magicFire.transform.GetChild(0).localScale = Vector3.one;
        _isCharging = false;
        if (Charging > 5)
            Charging = 3;
        _magicFire.transform.GetChild(0).localScale *= Charging;
        OnplayerCastMagicByBtn?.Invoke(MagicEnum.Fire);
        OnplayerCastMagicChargindValue?.Invoke(MagicEnum.Fire, (int) Charging);
        Debug.Log("Dispara Fire Iniciado Pelo BTN");
        Charging = 0;
    }
    public void BtnCastMagicByName(String name)
    {
        MagicEnum magicEnum;
        Enum.TryParse<MagicEnum>(name, out magicEnum);

        _isCharging = false;
        if (Charging > 5)
            Charging = 3;
        OnplayerCastMagicByBtn?.Invoke(magicEnum);
        OnplayerCastMagicChargindValue?.Invoke(magicEnum, (int)Charging);
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
            Charging += Time.deltaTime * ((totalCharge - 1) / totalChargeTime);
        }
        Charging = Mathf.Clamp(Charging, 1, totalCharge);

    }


}
public enum MagicEnum
{
    Fire,
    Fireball,
    Shield,
    MagicIce,
    Comet,
    Water,
    Tornado,
    Winter
}
