using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShieldController : MonoBehaviour
{
    [SerializeField] private Transform _characterCenter;

    private void Update()
    {
        this.transform.position = _characterCenter.position;
    }

    public void EnableShield()
    {
        this.gameObject.SetActive(true);
    }    
    public void DisableShield()
    {
        this.gameObject.SetActive(false);
    }
}
