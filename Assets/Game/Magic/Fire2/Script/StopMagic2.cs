using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMagic2 : MonoBehaviour
{


    
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
