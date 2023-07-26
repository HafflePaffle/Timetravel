using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedLimiter = 0.7f;
    public Rigidbody2D rb;
    private float inputHorizontal;
    private float inputVertical;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }
   
    void FixedUpdate()
    {
        if (inputHorizontal != 0  || inputVertical != 0)
        {
            if(inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
