using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity;
    public float jumpTreshold;
    public float jumpForce;
    public float fallMass;
    /// <summary>
    /// Represents how how far are player's eye from center of the screen horizontally.
    /// Value is between <-1, 1> with -1 being on left and 1 being on right
    /// </summary>
    private float eyeHorizontalFactor;
    /// <summary>
    /// Represents how how far are player's eye from center of the screen vertically.
    /// Value is between <-1, 1> with -1 being on bottom and 1 being on top
    /// </summary>
    private float eyeVerticalFactor;
    private Vector2Int screenCenter;
    private float moveHorizontal;
    private float moveVertical;
    private Rigidbody rb;
    private SphereCollider sc;
    private bool grounded;
    private float distToGround;
    private Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                                     // get hold of Rigidbody
        sc = GetComponent<SphereCollider>();                                                // get hold of collider
        distToGround = sc.bounds.extents.y;                                                 // calc distance to ground
        freezeYPos();
        eyeHorizontalFactor = eyeVerticalFactor = 0.0f;                                     // Reset values just in case
        screenCenter = new Vector2Int(Screen.width / 2, Screen.height / 2);                 // calculate the screen center
        grounded = true;
    }

    void FixedUpdate()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();                                      // get GazePoint from Tobii Eye Tracker

        // Doesnt work at the moment
        if (!gazePoint.IsValid)                                                             // Is User looking on screen ?
        {
            Time.timeScale = 0.0f;                                                          // Stop any calculations
        }
        else
        {
            Time.timeScale = 1.0f;                                                          // Resumes time "flow"
            setEyeHorizontalFactor(gazePoint);                                              // Calculate horizontal eye movement from screen center
            setEyeVerticalFactor(gazePoint);                                                // Calculate vertical eye movement from screen center
        }

        prevPosition = transform.position;                                                  // save transform values before physics

        HandleMovement();
        HandleJump();

        grounded = isGrounded();
        if (grounded)
        {
            clearRBConstraints();
            rb.mass = 1;
        }
        //Debug.Log("Eye Position: <" + eyeHorizontalFactor + ", " + eyeVerticalFactor + ">");
    }

    private void Update()
    {
        if (prevPosition.y < transform.position.y)  // is the player falling ?
        {
            rb.mass = fallMass;
        }
    }

    private void HandleMovement()
    {
        // move forward with W or Up Arrow Key
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = eyeHorizontalFactor;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * acceleration);
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    private void HandleJump()
    {
        if ((eyeVerticalFactor >= jumpTreshold) && grounded)
        {
            clearRBConstraints();
            freezeXPos();
            Vector3 jump = Vector3.up * jumpForce;
            rb.AddForce(jump);
        }
    }

    private void setEyeVerticalFactor(GazePoint gazePoint)
    {
        int GazeCenterDeviation = (int)gazePoint.Screen.y - screenCenter.y;
        eyeVerticalFactor = GazeCenterDeviation / (float)screenCenter.y;
    }

    private void setEyeHorizontalFactor(GazePoint gazePoint)
    {
        int GazeCenterDeviation = (int)gazePoint.Screen.x - screenCenter.x;
        eyeHorizontalFactor = GazeCenterDeviation / (float)screenCenter.x;
    }

    private void freezeXPos()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX;
    }

    private void freezeYPos()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;                              // Dont let the ball to jump up accidentally                                                               
    }

    private void clearRBConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            //setCount();
            //setCountText();
        }
    }

    /*
    private void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    private void setCount()
    {
        if(++count >= 11)
        {
            enableWinText();
        }
    }

    private void enableWinText()
    {
        winText.gameObject.SetActive(true);
    }
    */
}