using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    [SerializeField] private Attributes _attribute;
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    [SerializeField] private List<GameObject> _listLootEspecial;
    [SerializeField] private List<GameObject> _listLoot;
    [SerializeField] private int _lootAmount;
    private float _spawnRadius = 2f;
    private float _spawnForce = 3f;

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
            DropLoot();
        }
    }

    public void DropLoot()
    {
        int numLoot = UnityEngine.Random.Range(0, _lootAmount); // gera um número aleatório de 0 a 5
        bool isSpecial = (UnityEngine.Random.Range(0, 100) < 5); // 5% de chance de ser um item especial

        if(isSpecial)
        {
            SpawnLootAndAddForce(_listLootEspecial);
        }

        for (int i = 0; i < numLoot; i++)
        {
            if (_listLoot.Count > 0)
            {
                SpawnLootAndAddForce(_listLoot);
            }
        }
    }

    private void SpawnLootAndAddForce(List<GameObject> listLoot)
    {
        int randomIndex = UnityEngine.Random.Range(0, listLoot.Count);
        GameObject lootItem = listLoot[randomIndex];
        Debug.Log(lootItem.name);
        Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(-1f, 5f), UnityEngine.Random.Range(-1f, 7f)).normalized * _spawnRadius;
        GameObject lootObject = Instantiate(lootItem, (Vector2)transform.position + randomPosition, Quaternion.identity);
        Rigidbody2D lootRigidbody = lootObject.GetComponent<Rigidbody2D>();
        lootRigidbody.AddForce(randomPosition * _spawnForce, ForceMode2D.Impulse);
    }
}

public enum EnemyEnum
{
    Viking,
    Necromancher
}








