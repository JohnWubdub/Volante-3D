using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //move speed
    public float moveSpeed = 3f; //start
    private float maxMoveSpeed = 15f; //max
    private float minMoveSpeed = 3f; //min

    //turn speeds
    public float rightTurnSpeed = .9f; //start
    public float rightMaxTurnSpeed = .9f; //max
    public float rightMinTurnSpeed = .2f; //min

    public float leftTurnSpeed = .9f; //start
    public float leftMaxTurnSpeed = .9f; //max
    public float leftMinTurnSpeed = .2f; //min


    private float scaleTimer = 0f;
    private float startScaleTimer = .3f;

    Rigidbody2D rb; //the plane

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleTimer = startScaleTimer;
    }

    private void FixedUpdate() //movement my man
    {
        rb.MovePosition(transform.position + transform.up * Time.deltaTime * moveSpeed); //this is the working move forwards line. (this is the line that will actuallymove the object forward
    }

    void Update()
    {
        MainMovement();

        Checking();
    }




    void MainMovement()
    {
        if (Input.GetKey(KeyCode.D)) // turning the aircraft to the RIGHT
        {
            transform.Rotate(new Vector3(0, 0, -1) * rightTurnSpeed);
        }

        if (Input.GetKey(KeyCode.A)) //turning the aircraft to the LEFT
        {
            transform.Rotate(new Vector3(0, 0, 1) * leftTurnSpeed);
        }

        if (Input.GetKey(KeyCode.P)) //adding speed
        {
            scaleTimer -= Time.deltaTime;
            if (scaleTimer <= 0) //checking if it's less than the max
            {
                moveSpeed += moveScale; //adding
                rightTurnSpeed -= turnScale;//smaller turning radius
                leftTurnSpeed -= turnScale; //smaller turning radius
                scaleTimer = startScaleTimer; //reset 
            }
        }

        if (Input.GetKey(KeyCode.L)) //subtracting speed
        {
            scaleTimer -= Time.deltaTime;
            if (scaleTimer <= 0) //checking if it's more than the min
            {
                moveSpeed -= moveScale; //subtracting
                rightTurnSpeed += turnScale; //smaller turning radius
                leftTurnSpeed += turnScale; //smaller turning radius
                scaleTimer = startScaleTimer; //reset
            }
        }
    }


    void Checking()
    {
        //Turn speed check
        if (rightTurnSpeed < rightMinTurnSpeed) //right min
        {
            rightTurnSpeed = rightMinTurnSpeed;
        }
        if (leftTurnSpeed < leftMinTurnSpeed) //left min
        {
            leftTurnSpeed = leftMinTurnSpeed;
        }
        if (rightTurnSpeed > rightMaxTurnSpeed) //right max
        {
            rightTurnSpeed = rightMaxTurnSpeed;
        }
        if (leftTurnSpeed > leftMaxTurnSpeed) //left max
        {
            leftTurnSpeed = leftMaxTurnSpeed;
        }


        //move speed check
        if (moveSpeed < minMoveSpeed) //min speed
        {
            moveSpeed = minMoveSpeed;
        }
        if (moveSpeed > maxMoveSpeed) //max speed
        {
            moveSpeed = maxMoveSpeed;
        }
    }
}
