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
    [SerializeField] private bool isStoppedAttack;
   
    public Action<MagicEnum> OnplayerCastMagicByBtn;
    public Action<MagicEnum, int> OnplayerCastMagicChargindValue;

    [SerializeField] private bool _isCharging;

    private void Awake()
    {
        _animatorController = transform.parent.GetComponent<AnimatorController>();
    }

    private void StopMagicAction()
    {
        isStoppedAttack = false;
    }
    public void ChangeCharginValue(MagicEnum magic, int charging)
    {
        if (magic.Equals(MagicEnum.Comet))
        {
            Debug.Log("ChangeCharginValue" + MagicEnum.Comet + "Iniciando aparição da Magia");
            _charging = charging;
        }
       
    }
    IEnumerator TimeMagicOff(float time, GameObject Comet)
    {
        yield return new WaitForSeconds(time);
        Destroy(Comet);
        //yield return new WaitForSeconds(1f);
    }
    public float step;


    public void CastMagicComet(MagicEnum magic)
    {
        Debug.Log("CHEGOU NA MAGIA COMETA");
        if (magic.Equals(MagicEnum.Comet) && isStoppedAttack == false)
        {
            isStoppedAttack = true;
            _cometMagic.gameObject.SetActive(true);
            _cometMagic.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

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
    private void Update()
    {
        step = _speed * Time.deltaTime; // calculate distance to move

        _cometMagic.transform.position = Vector2.MoveTowards(_cometMagic.transform.position, _target.position, step);
        //_iceMagic.transform.position = Vector3.MoveTowards(Vector3.zero, new Vector3(10,1,1), step);
    }
      private void OnEnable()
    {
        _cometMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop += StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation += CastMagicComet;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }
    private void OnDisable()
    {
        _cometMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop -= StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation -= CastMagicComet;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }
}