﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContronller : MonoBehaviour
{
    public float walk_speed = 8f;
    public float jump_speed = 7f;
    

    // to keep our rigidbody
    Rigidbody rb;
    // to keep the collider
    Collider coll;

    //GameManager gameManager;

    //bewegungsgeschwindigkeit
    float rotationSpeed = 50f;

    //flag to keep track of whether a jump started
    bool pressed_jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        rb.MovePosition(new Vector3(0f, 0.01f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Player Walking
        WalkHandler();

        // Handle Player Jumping
        //JumpHandler();

        // Handle Mouse Rotation
        RotationHandler();
    }

    void OnTriggerEnter(Collider collider)
    {
        //GameManager gameManager = GetComponent<GameManager>();
        /*
        if ( collider.gameObject.tag == "Coin" )
        {
            print("grabbing coin ...");

            //Increase Score
            GameManager.instance.increaseScore(1);
            //Destroy Coin
            Destroy(collider.gameObject);
        }
        else if ( collider.gameObject.tag == "Enemy" )
        {
            print("Game Over!");
        }
        else
        */
        if ( collider.gameObject.tag == "Door" )
        {
            print("loading next level");
            //GameManager.instance.increaseLevel();
        }
    }

    //the camera rotates not the player itself
    void RotationHandler()
    {


        /*if ( Input.GetAxis("Mouse X") < 0 )
        {
            Debug.Log("handling Mouse movment left");
            transform.Rotate(0, -90 * Time.deltaTime, 0);
        }
        else if ( Input.GetAxis("Mouse X") > 0)
        {
            Debug.Log("handling mouse movment right");
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }*/

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

    }

    void WalkHandler()
    {

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); 

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        // Distance ( speed = distance / time --> distance = speed * time
        float distance = walk_speed * Time.deltaTime;

        // Input on X ( Horizontal )
        float hAxis = Input.GetAxis("Horizontal");

        // Input on Z ( Vertical )
        float vAxis = Input.GetAxis("Vertical");

        // Movment Vector
        Vector3 movment = new Vector3(hAxis * distance, 0f, vAxis * distance);
        //Vector3 movment = new Vector3(0f, 0f, vAxis * distance);

        // Current Position 
        Vector3 currPos = transform.position;

        // New Position
        Vector3 newPos = currPos + movment;

        // Move the Rigidbody
        rb.MovePosition(newPos);
/*
        // Input on X ( Horizontal )
        float hAxis = Input.GetAxis("Horizontal");

        // Input on Z ( Vertical )
        float vAxis = Input.GetAxis("Vertical");




        float distance = walk_speed * Time.deltaTime;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); 


        float dx = hAxis * distance * Mathf.Sin(Mathf.Deg2Rad * Camera.main.transform.rotation.eulerAngles.y);
        float dy = vAxis * distance * Mathf.Cos(Mathf.Deg2Rad * Camera.main.transform.rotation.eulerAngles.y);

        Vector3 movment = new Vector3(dx, 0f, dy);
        Vector3 currPos = transform.position;
        Vector3 newPos = currPos + movment;

        rb.MovePosition(newPos);*/
    }
    
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");

        bool is_grounded = CheckGrounded();

        if( jAxis > 0f )
        {
            if (!pressed_jump && is_grounded)
            {
                pressed_jump = true;
                // jumping vector
                Vector3 jumpVector = new Vector3(0f, jump_speed, 0f);

                // make the player jump by adding velocity
                rb.velocity = rb.velocity + jumpVector;
            }
        }
        else
        {
            pressed_jump = false;
        }
    }

    bool CheckGrounded()
    {
        // Object size in x
        float size_x = coll.bounds.size.x;
        float size_y = coll.bounds.size.y;
        float size_z = coll.bounds.size.z;

        // Position of the 4 Bottom corners of the game object
        // we need to add 0,01 in Y so that there is same distance betweent the floor and the point
        Vector3 corner_1 = transform.position + new Vector3(size_x / 2, -size_y / 2 + 0.01f, size_z / 2);
        Vector3 corner_2 = transform.position + new Vector3(-size_x / 2, -size_y / 2 + 0.01f, size_z / 2);
        Vector3 corner_3 = transform.position + new Vector3(size_x / 2, -size_y / 2 + 0.01f, -size_z / 2);
        Vector3 corner_4 = transform.position + new Vector3(-size_x / 2, -size_y / 2 + 0.01f, -size_z / 2);

        // send a short ray down the cube on all 4 corners to detect ground
        bool grounded_1 = Physics.Raycast(corner_1, new Vector3(0, -1, 0), 0.01f);
        bool grounded_2 = Physics.Raycast(corner_2, new Vector3(0, -1, 0), 0.01f);
        bool grounded_3 = Physics.Raycast(corner_3, new Vector3(0, -1, 0), 0.01f);
        bool grounded_4 = Physics.Raycast(corner_4, new Vector3(0, -1, 0), 0.01f);

        // if any corner is grounded the object is grounded
        return (grounded_1 || grounded_2 || grounded_3 || grounded_4);
    }
}