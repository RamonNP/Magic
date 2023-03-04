using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireBallController : MonoBehaviour
{
    [SerializeField]private GameObject _fireMagicprefab;
    [SerializeField]private Transform _fireMagicOrigem;
    [SerializeField]private AnimatorController _animatorController;
    [SerializeField]private MagicCastController _magicController;
    [SerializeField]private int _charging;

    private void Awake()
    {
        _animatorController = transform.parent.GetComponent<AnimatorController>();
    }

    private void OnEnable()
    {
        _animatorController.OnplayerCastMagicByAnimation += CastMagicFireball;
        _magicController.OnplayerCastMagicChargindValue += ChangeCharginValue;
    }

    private void OnDisable()
    {
        _animatorController.OnplayerCastMagicByAnimation -= CastMagicFireball;
        _magicController.OnplayerCastMagicChargindValue -= ChangeCharginValue;

    }

    // Update is called once per frame
    public void CastMagicFireball(MagicEnum magic)
    {
        if(magic.Equals(MagicEnum.Fireball)) {
            Debug.Log("CastMagicFireball" + MagicEnum.Fireball + "Iniciando aparição da Magia");
            //TODO criar poll de magia, não instanciar.
            GameObject _fireMagic = Instantiate(_fireMagicprefab, _fireMagicOrigem.position, _fireMagicOrigem.rotation);
            _fireMagic.transform.localScale *= _charging;
            _fireMagic.gameObject.SetActive(true);
            _fireMagic.GetComponent<Rigidbody2D>().AddForce(_fireMagicOrigem.right * 200);
            StartCoroutine(TimeMagicOff(2f, _fireMagic));
        }
    }    // Update is called once per frame
    public void ChangeCharginValue(MagicEnum magic, int charging)
    {
        if(magic.Equals(MagicEnum.Fireball)) {
            Debug.Log("ChangeCharginValue" + MagicEnum.Fireball + "Iniciando aparição da Magia");
            _charging = charging;
        }
    }
    IEnumerator TimeMagicOff(float time, GameObject fireMagic)
    {
        yield return new WaitForSeconds(time);
        Destroy(fireMagic);
        //yield return new WaitForSeconds(1f);
    }
}
