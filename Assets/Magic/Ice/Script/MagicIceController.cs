using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicIceController : MonoBehaviour
{
    [SerializeField] private GameObject _iceMagicprefab;
    [SerializeField] private Transform _iceMagicOrigem;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private MagicController _magicController;
    [SerializeField] private int _charging;





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
            Debug.Log("CastMagicIce" + _iceMagicOrigem.position + "Iniciando aparição da Magia");
            //TODO criar poll de magia, não instanciar.
            GameObject _iceMagic = Instantiate(_iceMagicprefab, _iceMagicOrigem.position, _iceMagicprefab.gameObject.transform.rotation);
            _iceMagic.transform.localScale *= _charging;
            _iceMagic.gameObject.SetActive(true);
            //_iceMagic.GetComponent<Rigidbody2D>().AddForce(_iceMagicOrigem.right * 200);
            StartCoroutine(TimeMagicOff(2f, _iceMagic));
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
