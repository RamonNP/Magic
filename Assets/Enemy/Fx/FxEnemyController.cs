using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxEnemyController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spriteEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeAnimation;
    [SerializeField] private Color _colorAnimation;
    public Action OnFxFinished;

    public void FXDamagePlay()
    {
        Debug.Log("FXFXFX");
        _animator.enabled = true;
        _animator.transform.GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer item in _spriteEnemy)
        {
            item.color = _colorAnimation;
        }
        StartCoroutine(MagicDuration());
    }
    public void FXDamageStop()
    {
        foreach (SpriteRenderer item in _spriteEnemy)
        {
            item.color = new Color(255, 255, 255, 255);
        }
        _animator.enabled = false;
        _animator.transform.GetComponent<SpriteRenderer>().enabled = false;
        OnFxFinished?.Invoke();
    }


    IEnumerator MagicDuration()
    {
        yield return new WaitForSeconds(_timeAnimation);
        FXDamageStop();
    }
}
