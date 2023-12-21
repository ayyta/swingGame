using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody.velocity = Vector2.up * 14;
        }
        else
        {
            // Get input values for horizontal and vertical axes
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            myRigidbody.velocity = new Vector2(horizontalInput * 7f, myRigidbody.velocity.y);
        }
    }
}
