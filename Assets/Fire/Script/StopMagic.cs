using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMagic : MonoBehaviour
{
    public float charging = 1;
    public float totalCharge = 3;
    public float totalChargeTime = 2;
    public void Stop()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }    
    public void Play()
    {
        //if()
        //this.gameObject.GetComponent<Animator>().Play("Fire"); ;
    }
}
