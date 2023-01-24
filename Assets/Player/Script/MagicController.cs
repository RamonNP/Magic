using Platformer.Magic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{

    [SerializeField]private GameObject _magicFire;
    [SerializeField]private MagicShieldController _magicShield;
    [SerializeField]private Transform _originOfMagic;
    [SerializeField]private PlayerStatusController _playerStatusController;
    [SerializeField]private PlayerController _playerController;

    private void OnEnable()
    {
        _playerController.OnplayerFlip += Flip;
    }

    private void OnDisable()
    {
        _playerController.OnplayerFlip -= Flip;
    }

    public void BtnCastFire()
    {
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
}
