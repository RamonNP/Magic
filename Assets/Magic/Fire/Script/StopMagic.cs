using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMagic : MonoBehaviour
{
    public Action OnMagicStop;

    public void Stop()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        OnMagicStop?.Invoke();
        Debug.Log("Dispara Fire Termino Da magia DESATIVANDO");
    }    
    public void Play()
    {

    }
}
