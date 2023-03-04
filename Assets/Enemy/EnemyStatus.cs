using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    [SerializeField] private Attributes _attribute;

    private void Start()
    {
        _attribute.CalculateATK();
    }

    public EnemyEnum Enemy { get => _enemyEnum; set => _enemyEnum = value; }
    public Attributes Attribute { get => _attribute; set => _attribute = value; }
}

public enum EnemyEnum
{
    Viking,
    Necromancher
}








