using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private GameObject _knockbackForcePrefab;
    [SerializeField] private Transform _knockbackPosition;

    public Transform KnockbackPosition { get => _knockbackPosition; set => _knockbackPosition = value; }

    public void ApplyKnockback(float kx)
    {
        _knockbackPosition.localPosition = new Vector3( kx, _knockbackPosition.localPosition.y, 0);
        GameObject knockback = Instantiate(_knockbackForcePrefab, _knockbackPosition.position, Quaternion.identity);
        Destroy(knockback, 0.2f);
    }

}
