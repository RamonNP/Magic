using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffectController : MonoBehaviour
{
    [SerializeField] private Material _materialDissolveEffect;
    [SerializeField] private Material _materialold;

    [SerializeField] private float _dissolveAmount;
    [SerializeField] private bool _isDissolving;
    [SerializeField] private bool _isUseDissolving;
    [SerializeField] private List<Renderer> _skinList;

    private void Start()
    {
        Debug.Log(_skinList.Count);
        _materialold = _skinList[0].material;
    }

    void Update()
    {
        if(_isUseDissolving)
        {
            if(_isDissolving)
            {
                _dissolveAmount = Mathf.Clamp01(_dissolveAmount + Time.deltaTime);
            } else
            {
                _dissolveAmount = Mathf.Clamp01(_dissolveAmount - Time.deltaTime);
            }
            _materialDissolveEffect.SetFloat("_DissolveAmount", _dissolveAmount);
            if(_dissolveAmount == 0)
            {
                _isUseDissolving = false;
            }

        }
    
        if(Input.GetKeyDown(KeyCode.T))
        {
            _isDissolving = !_isDissolving;
            if(!_isUseDissolving)
            {
                ActiveDissolveSystem();
            } else
            {
                
            }
        }
        if(!_isUseDissolving && !_isDissolving && _dissolveAmount < 0.4 )
        {
            DeactveDissolveSystem();
        }
      
    }

    private void ActiveDissolveSystem()
    {
        foreach (var item in _skinList)
        {
            item.material = _materialDissolveEffect;
        }
       
        _isUseDissolving = true;
    }
    private void DeactveDissolveSystem()
    {
        foreach (var item in _skinList)
        {
            item.material = _materialold;
        }
   
    }
}
