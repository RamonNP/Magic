using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private bool _isflying;
    [SerializeField] private float _magicPower;

    public Action<bool> OnplayerFlying;

    public bool Isflying { get => _isflying; }


    public void ToFly(bool fly)
    {
        _isflying = fly;
        OnplayerFlying?.Invoke(fly);
    }
}
