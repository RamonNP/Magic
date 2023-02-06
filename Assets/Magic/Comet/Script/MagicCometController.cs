using Platformer.Magic.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCometController : MonoBehaviour
{

    [SerializeField] private GameObject _cometMagic;
    [SerializeField] private Transform _cometMagicOrigem;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private MagicCastController _magicController;
    [SerializeField] private int _charging;
    // Adjust the speed for the application.
    [SerializeField] private float _speed = 1.0f;

    // The target (cylinder) position.
    [SerializeField] private Transform _target;
    private bool isStoppedAttack;
    public float charging = 1;
    public float totalCharge = 3;
    public float totalChargeTime = 2;

    public Action<MagicEnum> OnplayerCastMagicByBtn;
    public Action<MagicEnum, int> OnplayerCastMagicChargindValue;

    [SerializeField] private bool _isCharging;
 


    private void OnEnable()
    {
        _cometMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop += StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation += CastMagicComet;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }

    private void StopMagicAction()
    {
        isStoppedAttack = false;
    }

    private void OnDisable()
    {
        _cometMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop -= StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation -= CastMagicComet;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }
    public void CastMagicComet(MagicEnum magic)
    {
        Debug.Log("CHEGOU NA MAGIA COMETA");
        if (magic.Equals(MagicEnum.Comet) && isStoppedAttack == false)
        {
            isStoppedAttack = true;
            _cometMagic.gameObject.SetActive(true);

            _target.localPosition = _cometMagicOrigem.position;
            _cometMagic.transform.localPosition = _cometMagicOrigem.position;
            _cometMagic.transform.localScale = _cometMagicOrigem.localScale;
            if (_cometMagicOrigem.localScale.x > 0)
            {
                _target.position += new Vector3(3, 0, 0);
            }
            else
            {
                _target.position += new Vector3(-3, 0, 0);
            }
            

            //StartCoroutine(TimeMagicOff(2f, _iceMagic));
        }
    }
    public void ChangeCharginValue(MagicEnum magic, int charging)
    {
        if (magic.Equals(MagicEnum.Comet))
        {
            Debug.Log("ChangeCharginValue" + MagicEnum.Comet + "Iniciando aparição da Magia");
            _charging = charging;
        }
        IEnumerator TimeMagicOff(float time, GameObject Comet)
        {
            yield return new WaitForSeconds(time);
            Destroy(Comet);
            //yield return new WaitForSeconds(1f);
        }
    }
}