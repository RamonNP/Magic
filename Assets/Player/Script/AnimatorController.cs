using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private MagicCastController _magicController;
    public Action<MagicEnum> OnplayerCastMagicByAnimation;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _magicController.OnplayerCastMagicByBtn += CastMagicByEnum;
    }
    private void OnDisable()
    {
        
        _magicController.OnplayerCastMagicByBtn -= CastMagicByEnum;
    }

    public void MagicControllerCastFireByAnimation()
    {
        Debug.Log("Dispara Fire Iniciando aparição da Magia");
        _magicController.CastFire();
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fire);

    }    
    public void MagicControllerCastFireBallByAnimation()
    {
        Debug.Log("Dispara "+ MagicEnum.Fireball + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fireball);
    }
    public void MagicControllerCometByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Comet + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Comet);
    }
    public void MagicControllerWaterByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Water + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Water);
    }
    public void MagicControllerMagicIceByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.MagicIce + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.MagicIce);
    }
    public void MagicControllerMagicTornadoByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Tornado + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Tornado);
    }
    public void CastMagicByEnum(MagicEnum magic)
    {
        switch (magic)
        {
            case MagicEnum.Fire:
                Debug.Log("Dispara Fire Iniciando Animação do PLayer");
                _animator.Play("Casting Spells");    
                break;
            case MagicEnum.Fireball:
                Debug.Log("Dispara Fire Iniciando Animação do PLayer");
                _animator.Play("PlayerCastFireball");
                break;
            case MagicEnum.MagicIce:
                Debug.Log("Dispara Ice Iniciando Animação do PLayer");
                _animator.Play("CastIce");
                break;
            case MagicEnum.Comet:
                Debug.Log("Dispara " +magic+" Iniciando Animação do PLayer");
                _animator.Play("CastMagicComet");
                break;
            case MagicEnum.Water:
                Debug.Log("Dispara " + magic + " Iniciando Animação do PLayer");
                _animator.Play("CastWaterMagic");
                break;
            case MagicEnum.Tornado:
                Debug.Log("Dispara " + magic + " Iniciando Animação do PLayer");
                _animator.Play("CastTornadoMagic");
                break;
            case MagicEnum.Shield:
                break;
            case MagicEnum.Winter:
                break;
            default:
                break;
        }
        if (magic.Equals(MagicEnum.Fire))
        {
        }
    }
}
