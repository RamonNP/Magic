using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxIceEnemyDamage : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spriteEnemy;
    [SerializeField] private SpriteRenderer _spriteIce;
    public Action OnFxFinished;

    public void FXDamageIcePlay()
    {
        _spriteIce.enabled = true;
        foreach (SpriteRenderer item in _spriteEnemy)
        {
            item.color = new Color(0, 0, 255, 255);
        }
        StartCoroutine(MagicDuration());
    }
    public void FXDamageIceStop()
    {
        foreach (SpriteRenderer item in _spriteEnemy)
        {
            item.color = new Color(255, 255, 255, 255);
        }
        _spriteIce.enabled = false;
        OnFxFinished?.Invoke();
    }


    IEnumerator MagicDuration()
    {
        yield return new WaitForSeconds(2);
        FXDamageIceStop();
    }
}
