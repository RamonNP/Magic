using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCollisionController : MonoBehaviour
{

    [SerializeField] private MagicStatus _magicStatus;

    private void Awake()
    {
        _magicStatus = this.GetComponent<MagicStatus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            _magicStatus.OnMagicalCollision?.Invoke(_magicStatus, collision.transform.GetComponent<EnemyStatus>(), TypeItem.Staff);
        }
    }

}
