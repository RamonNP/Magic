using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicIceController : MonoBehaviour
{
    [SerializeField] private GameObject _iceMagic;
    [SerializeField] private Transform _iceMagicOrigem;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private MagicCastController _magicController;
    [SerializeField] private int _charging;
    // Adjust the speed for the application.
    [SerializeField] private float _speed = 1.0f;

    // The target (cylinder) position.
    [SerializeField] private Transform _target;
    private bool isStoppedAttack;

    private Vector3 _scaleBackup;

    private void OnEnable()
    {
        _scaleBackup = _iceMagic.transform.localScale;
        _iceMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop += StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation += CastIce;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }

    private void StopMagicAction()
    {
        isStoppedAttack = false;
    }

    private void OnDisable()
    {
        _iceMagic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop -= StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation -= CastIce;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }
    public void CastIce(MagicEnum magic)
    {
        if (magic.Equals(MagicEnum.MagicIce) && isStoppedAttack == false)
        {
            Debug.Log("CHEGOU NA MAGIA GELO");
            _iceMagic.transform.localScale = _scaleBackup;
            isStoppedAttack = true;
            _iceMagic.gameObject.SetActive(true);
            _iceMagic.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

            _target.localPosition = _iceMagicOrigem.position;
            _iceMagic.transform.localPosition = _iceMagicOrigem.position;

            _iceMagic.transform.localScale = _iceMagicOrigem.localScale * _magicController.Charging;
            if(_iceMagicOrigem.localScale.x > 0)
            {
                _target.position += new Vector3(3, 0, 0);
            } else
            {
                _target.position += new Vector3(-3, 0, 0);
            }
            _magicController.Charging = 0;
            //StartCoroutine(TimeMagicOff(2f, _iceMagic));
        }
    }

    public void ChangeCharginValue(MagicEnum magic, int charging)
    {
        if (magic.Equals(MagicEnum.MagicIce))
        {
            Debug.Log("ChangeCharginValue" + MagicEnum.MagicIce + "Iniciando aparição da Magia");
            _charging = charging;
        }
    }
    IEnumerator TimeMagicOff(float time, GameObject iceMagic)
    {
        yield return new WaitForSeconds(time);
        Destroy(iceMagic);
        //yield return new WaitForSeconds(1f);
    }
    public float step;

    private void Update()
    {
        step = _speed * Time.deltaTime; // calculate distance to move

        _iceMagic.transform.position = Vector2.MoveTowards(_iceMagic.transform.position, _target.position, step);
        //_iceMagic.transform.position = Vector3.MoveTowards(Vector3.zero, new Vector3(10,1,1), step);
    }

}
