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

    public Action<MagicEnum> OnplayerCastMagicByBtn;
    public Action<MagicEnum, int> OnplayerCastMagicChargindValue;

    public Action<bool> OnplayerChargind;

    [SerializeField] private float _chargingRate = 1.0f;
    [SerializeField] private float[] _chargingStages = { 3.0f, 5.0f, 7.0f };


    //remover serializableFIld
    [SerializeField] private bool _isCharging = false;
    [SerializeField] private float _charging = 0.0f;
    [SerializeField] private float _charge = 1.0f;

    private void Awake()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
    }

    public float Charging { get => _charging;}

    private void OnEnable()
    {
        _playerController.OnplayerFlip += Flip;
        StopMagic.OnMagicStop += StopMagicAction;
    }

    private void OnDisable()
    {
        _playerController.OnplayerFlip -= Flip;
    }
    private void Update()
    {
        if (_isCharging)
        {
            _charging += Time.deltaTime * _chargingRate;
            for (int i = 0; i < _chargingStages.Length; i++)
            {
                if (_charging >= _chargingStages[i])
                {
                    _charge = _chargingStages[i];
                }
            }
            if (_charge == _chargingStages[_chargingStages.Length - 1])
            {
                _isCharging = false;
            }
            if(_charge > 0)
                _magicFire.transform.localScale = new Vector3(_charge, _charge, 1);//_originOfMagic.localScale;
        }
        
    }

    public void BtnChargingDown()
    {
        _isCharging = true;
        OnplayerChargind?.Invoke(_isCharging);
    }       
    public void BtnCastMagicByName(String name)
    {
        _playerController.IsAttacking = true;
        MagicEnum magicEnum;
        Enum.TryParse<MagicEnum>(name, out magicEnum);

        _isCharging = false;
        _charging = 0.0f;
        _charge = 1.0f;

        OnplayerChargind?.Invoke(_isCharging);
        OnplayerCastMagicByBtn?.Invoke(magicEnum);
        //OnplayerCastMagicChargindValue?.Invoke(magicEnum, (int)Charging);
        Debug.Log("Dispara name Iniciado Pelo BTN"+ name +" Chaging"+ Charging);
    }
    public void CastFire()
    {
        
        _magicFire.transform.position = _originOfMagic.position;
        Debug.Log(_charge);
        //_magicFire.transform.localScale = new Vector3(_charge, _charge, 1);//_originOfMagic.localScale;
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
    private void StopMagicAction()
    {
        _playerController.IsAttacking = false;
    }
}
public enum MagicEnum
{
    None,
    Fire,
    Fireball,
    Shield,
    MagicIce,
    Comet,
    Water,
    Tornado,
    Winter
}
