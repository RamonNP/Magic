using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{


    private float _rotationSpeed = 100f;
    private Vector3 _rotation;
    public Transform point;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotation = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));

        transform.Rotate(-_rotation * _rotationSpeed * Time.deltaTime);
        rayHit();
    }

    void rayHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, transform.TransformDirection(Vector2.up), 10f);
        if (hit)
        {
            float distance = Vector2.Distance(point.position, hit.point);
            Debug.DrawRay(point.position, point.TransformDirection(Vector2.up) * distance, Color.black);
            Debug.Log(hit.transform.name);
        }
    }
}
