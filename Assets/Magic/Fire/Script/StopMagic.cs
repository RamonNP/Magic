using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMagic : MonoBehaviour
{
    public void Stop()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }    
    public void Play()
    {
        //this.gameObject.GetComponent<Animator>().Play("Fire"); ;
    }
}
