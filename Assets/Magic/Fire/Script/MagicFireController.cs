using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireController : MonoBehaviour
{
    [SerializeField] private GameObject _magic;
    [SerializeField] private Transform _magicOrigem;
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
        _scaleBackup = _magic.transform.localScale;
        _magic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop += StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation += CastMagic;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }

    private void StopMagicAction()
    {
        isStoppedAttack = false;
    }

    private void OnDisable()
    {
        _magic.transform.GetChild(0).GetComponent<StopMagic>().OnMagicStop -= StopMagicAction;
        _animatorController.OnplayerCastMagicByAnimation -= CastMagic;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }
    public void CastMagic(MagicEnum magic)
    {
        if (magic.Equals(MagicEnum.Fire) && isStoppedAttack == false)
        {
            Debug.Log("CHEGOU NA MAGIA "+ magic);
            _magic.transform.localScale = _scaleBackup;
            isStoppedAttack = true;
            _magic.gameObject.SetActive(true);
            _magic.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            _target.localPosition = _magicOrigem.position;
            _magic.transform.localPosition = _magicOrigem.position;
            _magic.transform.localScale = _magicOrigem.localScale * _magicController.Charging;
            if (_magicOrigem.localScale.x > 0)
            {
                _target.position += new Vector3(3, 0, 0);
            }
            else
            {
                _target.position += new Vector3(-3, 0, 0);
            }
            _magicController.Charging = 0;
            //StartCoroutine(TimeMagicOff(2f, _iceMagic));
        }
    }

    public void ChangeCharginValue(MagicEnum magic, int charging)
    {
        if (magic.Equals(MagicEnum.Fire))
        {
            Debug.Log("ChangeCharginValue" + MagicEnum.Fire + "Iniciando aparição da Magia");
            _charging = charging;
        }
    }
    public float step;

    private void Update()
    {
        step = _speed * Time.deltaTime; // calculate distance to move

        _magic.transform.position = Vector2.MoveTowards(_magic.transform.position, _target.position, step);
        //_iceMagic.transform.position = Vector3.MoveTowards(Vector3.zero, new Vector3(10,1,1), step);
    }
}
