using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private bool _isGround;

    public bool IsGround { get => _isGround; set => _isGround = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.CompareTag("GroundCheck"))
        {
            IsGround = true;
        }       
    }    

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.CompareTag("GroundCheck"))
        {
            IsGround = false;
        }       
    }
    
}
