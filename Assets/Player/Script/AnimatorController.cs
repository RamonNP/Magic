using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private MagicController _magicController;
    public Action<MagicEnum> OnplayerCastMagicByAnimation;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _magicController.OnplayerCastMagicByBtn += CastFire;
    }
    private void OnDisable()
    {
        
        _magicController.OnplayerCastMagicByBtn -= CastFire;
    }

    public void MagicControllerCastFireByAnimation()
    {
        Debug.Log("Dispara Fire Iniciando apari��o da Magia");
        _magicController.CastFire();
        
    }    
    public void MagicControllerCastFireBallByAnimation()
    {
        Debug.Log("Dispara "+ MagicEnum.Fireball + " Iniciando apari��o da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fireball);
    }
    public void CastFire(MagicEnum magic)
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
