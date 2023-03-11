using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    [SerializeField] private Attributes _attribute;
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    private bool _isDie;

    public Action OnPlayDie;
    private void Start()
    {
        _attribute.Initialize();
        _attribute.CalculateATK();
    }

    public EnemyEnum Enemy { get => _enemyEnum; set => _enemyEnum = value; }
    public Attributes Attribute { get => _attribute; set => _attribute = value; }
    public bool IsDie { get => _isDie; }

    public void SetIsDie(bool die) 
    { 
        _isDie = die;
        if(die)
        {
            OnPlayDie?.Invoke();
            transform.GetComponent<CapsuleCollider2D>().enabled = false; // Desativa o collider do inimigo
                                           
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;// Desativa a gravidade e movimento horizontal do Rigidbody2D
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}

public enum EnemyEnum
{
    Viking,
    Necromancher
}








