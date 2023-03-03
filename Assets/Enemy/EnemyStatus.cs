using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    [SerializeField] private int _life;
    public EnemyEnum Enemy { get => _enemyEnum; set => _enemyEnum = value; }
     void Update()
    {
       

    }
}

public enum EnemyEnum
{
    Viking,
    Necromancher
}








