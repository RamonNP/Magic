using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFire : MonoBehaviour
{
   
    void Start()
    {
        
    }

 
    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 5)
        {
            Destroy(this.gameObject);
        }
    }
}
