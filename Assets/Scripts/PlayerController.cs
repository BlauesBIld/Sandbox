using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * (Time.deltaTime * 5f));
        } else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * (Time.deltaTime * 5f));
        } else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * (Time.deltaTime * 5f));
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * (Time.deltaTime * 5f));
        }
    }
}
