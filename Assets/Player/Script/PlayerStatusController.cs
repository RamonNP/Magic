using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField] private bool _isflying;
    [SerializeField] private int _manaBse;
    [SerializeField] private int _LifeBase;
    [SerializeField] private Attributes attributes;

    public Action<bool> OnplayerFlying;

    public bool Isflying { get => _isflying; }


    public void ToFly(bool fly)
    {
        _isflying = fly;
        OnplayerFlying?.Invoke(fly);
    }
}
