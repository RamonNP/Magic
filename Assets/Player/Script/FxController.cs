using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{
    [SerializeField] private MagicCastController _magicController;
    [SerializeField] private Material _material;
    Coroutine _coroutineChangeColorCharging;


    private void Awake()
    {
        //StartCoroutine(ChangeColorCharging());
    }

    private void Update()
    {

    }

    public void PlayCharging()
    {
        
    }

    IEnumerator ChangeColorCharging()
    {

        Color c = _material.color;
        while (true)
        {
            if (_magicController.Charging > 1.5f)
            {
                Debug.Log(_magicController.Charging);
                c.b = 255;
                c.g = 0;
                c.r = 0;
                _material.SetColor("_Tint",c);
                yield return new WaitForSeconds(0.2f);
                c.b = 0;
                c.g = 0;
                c.r = 255;
                _material.SetColor("_Tint", c);
                yield return new WaitForSeconds(0.2f);
                c.b = 0;
                c.g = 255;
                c.r = 255;
                _material.SetColor("_Tint", c);
                yield return new WaitForSeconds(0.2f);
                c.b = 255;
                c.g = 0;
                c.r = 255;
                _material.SetColor("_Tint", c);
                yield return new WaitForSeconds(0.2f);
            } else
            {
                Debug.Log(_magicController.Charging);
                c.b = 0;
                c.g = 0;
                c.r = 0;
                _material.SetColor("_Tint", c);
                yield return new WaitForSeconds(0.2f);
            }
            yield return ((_magicController.Charging > 1.5f));

        }

    }
}
