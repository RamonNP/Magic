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
    [SerializeField] private EnemyStatus _enemyStatus;
    [SerializeField] private Transform _enemyLabel;
    private void OnEnable()
    {
        Debug.Log("_enemyStatus "+_enemyStatus);
        _enemyStatus.OnPlayDie += ActiveToggleDissolve;
    }

    private void ActiveToggleDissolve()
    {
        Invoke("ToggleDissolve", 5.0f);
        Invoke("DisableEnemyLabel", 2f);
        Destroy(gameObject, 7);
    }

    private void DisableEnemyLabel()
    {
        _enemyLabel.gameObject.SetActive(false);
    }

    private void Start()
    {
        Debug.Log(_skinList.Count);
        _materialold = _skinList[0].material;
    }

    void Update()
    {
        Dissolving();

        if (!_isUseDissolving && !_isDissolving && _dissolveAmount < 0.4)
        {
            DeactveDissolveSystem();
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ToggleDissolve();
            }
        }
    }

    private void ToggleDissolve()
    {
        _isDissolving = !_isDissolving;
        if (!_isUseDissolving)
        {
            ActiveDissolveSystem();
        }
    }

    private void Dissolving()
    {
        if (_isUseDissolving)
        {
            _dissolveAmount += _isDissolving ? Time.deltaTime : -Time.deltaTime;
            _dissolveAmount = Mathf.Clamp01(_dissolveAmount);
            _materialDissolveEffect.SetFloat("_DissolveAmount", _dissolveAmount);
            if (!_isDissolving && Mathf.Approximately(_dissolveAmount, 0f))
            {
                _isUseDissolving = false;
            }
        }
    }

    private void ActiveDissolveSystem()
    {
        _skinList.ForEach(item => item.material = _materialDissolveEffect);
        _isUseDissolving = true;
    }
    private void DeactveDissolveSystem()
    {
        _skinList.ForEach(item => item.material = _materialold);
    }
}
