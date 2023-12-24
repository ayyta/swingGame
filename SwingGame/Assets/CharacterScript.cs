using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Grapple grapple;
    public Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        grapple = GetComponent<Grapple>();

        if (grapple == null)
        {
            Debug.LogError("GrapplingHook component not found on the GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Check for jump input

        if ( grapple.IsGrappling())
        {
            float input = Input.GetAxis("Horizontal");
            float swingForce = 10f;
            myRigidbody.AddForce(new Vector2(input * swingForce, 0));

            if ( Input.GetButtonDown("Jump"))
            {
                grapple.ReleaseGrapple();
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 14);
            }
        }

        else
        {
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
    
}
