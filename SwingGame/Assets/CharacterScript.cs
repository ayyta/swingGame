using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * 10;
        }
        else
        {
            // Get input values for horizontal and vertical axes
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the movement direction
            Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

            // Set the velocity based on the calculated movement direction
            myRigidbody.velocity = new Vector2(movement.x * 5, movement.y * 5);
        }
    }


}
