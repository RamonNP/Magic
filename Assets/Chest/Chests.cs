using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private Transform coin;
    [SerializeField] private bool colPlayer;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (colPlayer && Input.GetKeyDown("o"))
        {
            anim.enabled = true;
            DropCoin();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            colPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            colPlayer = false;
        }
    }
    void DropCoin()
    {
        Instantiate(coin, transform.position, transform.rotation);
    }
}
