using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrap : MonoBehaviour
{
    public GameObject fire;
    private float tempFire;
    private void FixedUpdate()
    {
        tempFire = tempFire + Time.deltaTime;
        if (tempFire > 2)
        {
            //GameObject gameObject1 = Instantiate(fire, transform.position, Quaternion.Euler(0, 0, 0));
            tempFire = 0;
            //Destroy(gameObject1, 2);
        }
    }

}
