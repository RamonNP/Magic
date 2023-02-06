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
        Debug.Log("Dispara Fire Iniciando apari��o da Magia");
        _magicController.CastFire();
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fire);

    }    
    public void MagicControllerCastFireBallByAnimation()
    {
        Debug.Log("Dispara "+ MagicEnum.Fireball + " Iniciando apari��o da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fireball);
    }
    public void MagicControllerCometByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Comet + " Iniciando apari��o da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Comet);
    }
    public void MagicControllerMagicIceByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.MagicIce + " Iniciando apari��o da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.MagicIce);
    }
    public void CastMagicByEnum(MagicEnum magic)
    {
        switch (magic)
        {
            case MagicEnum.Fire:
                Debug.Log("Dispara Fire Iniciando Anima��o do PLayer");
                _animator.Play("Casting Spells");    
                break;
            case MagicEnum.Fireball:
                Debug.Log("Dispara Fire Iniciando Anima��o do PLayer");
                _animator.Play("PlayerCastFireball");
                break;
            case MagicEnum.MagicIce:
                Debug.Log("Dispara Ice Iniciando Anima��o do PLayer");
                _animator.Play("CastIce");
                break;
            case MagicEnum.Comet:
                Debug.Log("Dispara Ice Iniciando Anima��o do PLayer");
                _animator.Play("CasMagicComet");
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
