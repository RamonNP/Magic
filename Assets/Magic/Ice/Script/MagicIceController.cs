using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicIceController : MonoBehaviour
{
    [SerializeField] private GameObject _iceMagic;
    [SerializeField] private Transform _iceMagicOrigem;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private MagicController _magicController;
    [SerializeField] private int _charging;
    // Adjust the speed for the application.
    [SerializeField] private float _speed = 1.0f;

    // The target (cylinder) position.
    private Transform _target;





    private void OnEnable()
    {
        _animatorController.OnplayerCastMagicByAnimation += CastIce;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }

    private void OnDisable()
    {
        _animatorController.OnplayerCastMagicByAnimation -= CastIce;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }
    public void CastIce(MagicEnum magic)
    {
        Debug.Log("CHEGOU NA MAGIA GELO");
        if (magic.Equals(MagicEnum.MagicIce))
        {

            _iceMagic.gameObject.SetActive(true);
            _iceMagic.transform.localPosition = _iceMagicOrigem.position;
            _iceMagic.transform.localScale = _iceMagicOrigem.localScale;

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

    private void Update()
    {
        //var step = _speed * Time.deltaTime; // calculate distance to move
        //_iceMagic.transform.position = Vector3.MoveTowards(_iceMagic.transform.position, _target.position, step);
    }

}
