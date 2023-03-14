using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFire : MonoBehaviour
{
    public GameObject[] fires;

    void Start()
    {
        fires = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            fires[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 4; i++)
            {
                fires[i].SetActive(!fires[i].activeSelf);
            }
        }
    }
}