using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 4f;
    [SerializeField] private ArrowType _arrowType;
    public Action<Collider2D, Arrow> OnArrowCollision;

    private Rigidbody2D _rigidbody;

    public float Speed { get => _speed; set => _speed = value; }
    public float Lifetime { get => _lifetime; set => _lifetime = value; }
    public ArrowType ArrowType { get => _arrowType; set => _arrowType = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //OnArrowCollision?.Invoke(collision, _arrowType);
            Debug.Log("Arrow hit a monster");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnArrowCollision?.Invoke(collision, this);
            //Debug.Log("Arrow hit a monster");
        }
    }
}
public enum ArrowType
{
    RegularArrow,       // Flecha Normal
    FireArrow,          // Flecha de Fogo
    IceArrow,           // Flecha de Gelo
    PoisonArrow,        // Flecha de Veneno
    ExplosiveArrow,     // Flecha Explosiva
    ElectricArrow,      // Flecha Elétrica
    LightArrow,         // Flecha de Luz
    DarkArrow,          // Flecha Sombria
    HomingArrow,        // Flecha Guiada
    SplitArrow          // Flecha Dividida
}
