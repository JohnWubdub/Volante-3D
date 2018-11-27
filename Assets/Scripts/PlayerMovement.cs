using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //move speed
    [Header("Move Speed: ")]
    public float moveSpeed = 5f; //start
    public float maxMoveSpeed = 25f; //max
    public float minMoveSpeed = 5f; //min

    //turn speeds

        //Max Temp
        [Header("Temporary Turn Speed: ")]
        public float baseMaxTurnSpeed = 1f;
        public float tempMaxTurnSpeed = 2.5f;
        private float maxNum = 2.5f;
        private float minNum = 1;

        //right
        [Header("Right Turn Speed: ")]
        public float rightTurnSpeed = 0.5f; //start
        public float rightMaxTurnSpeed = 1f; //max
        public float rightMinTurnSpeed = 0.5f; //min

        //left
        [Header("Left Turn Speed: ")]
        public float leftTurnSpeed = 0.5f; //start
        public float leftMaxTurnSpeed = 1f; //max
        public float leftMinTurnSpeed = 0.5f; //min

        

    //move Scaling
    [Header("Scaling: ")]
    public float moveScale = 0.7f;
    public float turnScale = 0.4f;
    public float rotateScale = 0.7f;

    private float scaleTimer = 0f;
    private float startScaleTimer = .2f;

    //rotation locks
    float lockRotation = 0f;
    float rightAngleLock = 269f;
    float leftAngleLock = 91f;
    
    //pseudo rotation
    public float pseudoCurrentAngle = 0f;
    float pseudoMaxAngle = 90f;
    float pseudoMinAngle = 1f;


    Rigidbody rb; //the plane









    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scaleTimer = startScaleTimer;
    }

    private void FixedUpdate() //movement my man
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed); //this is the working move forwards line. (this is the line that will actuallymove the object forward 
    }

    void Update()
    {
        Turning();

        Rotating();

        Speed();

        Checking();
    }









    void Turning()
    {
        if (Input.GetKey(KeyCode.D)) // turning the aircraft to the RIGHT
        {
            transform.Rotate(new Vector3(0, 1, 0) * rightTurnSpeed, Space.Self);
            Debug.Log("Turning Right");
        }

        if (Input.GetKey(KeyCode.A)) //turning the aircraft to the LEFT
        {
            transform.Rotate(new Vector3(0, -1, 0) * leftTurnSpeed, Space.Self);
            Debug.Log("Turning Left");
        }    
    }









    void Speed()
    {
        if (Input.GetKey(KeyCode.P)) //adding speed
        {
            scaleTimer -= Time.deltaTime;
            if (scaleTimer <= 0) //checking if it's less than the max
            {
                moveSpeed += moveScale; //adding
                rightTurnSpeed -= turnScale;//smaller turning radius
                leftTurnSpeed -= turnScale; //smaller turning radius
                this.GetComponent<EngineSound>().PitchChanger(moveSpeed);
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
                this.GetComponent<EngineSound>().PitchChanger(moveSpeed);
                scaleTimer = startScaleTimer; //reset
                
            }
        }
    }








    void Rotating() //find a way to carry over the speed from one part to another!!!!
    {
        
        float leftHolder = leftTurnSpeed;
        float rightHolder = rightTurnSpeed;

        //input
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -1) * rotateScale, Space.Self); //rotating right
            pseudoCurrentAngle -= (1 * rotateScale);

        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 1) * rotateScale, Space.Self); //rotating left
            pseudoCurrentAngle += (1 * rotateScale);
        }


        //Speed changes based on Angle of rotation

        //RIGHT 360 - 270 could change later to allow for a middle spot between rotating and not 
        if (transform.rotation.eulerAngles.z < 360 && transform.rotation.eulerAngles.z >= rightAngleLock) //just checking to see if it is in the left area
        {
            rightMaxTurnSpeed = tempMaxTurnSpeed;
            rightTurnSpeed = ScaleSpeed(rightTurnSpeed, pseudoMinAngle, pseudoMaxAngle, maxNum, minNum);
        }
        else //reset
        { 
            rightMaxTurnSpeed = baseMaxTurnSpeed;
            rightTurnSpeed = rightHolder;
        }


        //LEFT 0 - 90
        if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z <= leftAngleLock) //just checking to see if it is in the left area
        {
            leftMaxTurnSpeed = tempMaxTurnSpeed;
            leftTurnSpeed = ScaleSpeed(leftTurnSpeed, pseudoMinAngle, pseudoMaxAngle, maxNum, minNum);
        }
        else //reset
        {
            leftMaxTurnSpeed = baseMaxTurnSpeed;
            leftTurnSpeed = leftHolder;
        }


    }





    private float ScaleSpeed(float turnSpeed, float minAngle, float maxAngle, float maxNum, float minNum)
    {
        float newSpeed =  ( ( (maxNum - minNum) * (Mathf.Abs(pseudoCurrentAngle) - minAngle) ) / (maxAngle - minAngle)) + minNum ;
        turnSpeed = newSpeed;
        Debug.Log("newSpeed" + newSpeed);
       
        return turnSpeed;
    }
  

    //state machine


    void Checking()
    {
        //Turn speed check
        if (rightTurnSpeed <= rightMinTurnSpeed) //right min
        {
            rightTurnSpeed = rightMinTurnSpeed;
        }
        if (leftTurnSpeed <= leftMinTurnSpeed) //left min
        {
            leftTurnSpeed = leftMinTurnSpeed;
        }
        if (rightTurnSpeed >= rightMaxTurnSpeed) //right max
        {
            rightTurnSpeed = rightMaxTurnSpeed;
        }
        if (leftTurnSpeed >= leftMaxTurnSpeed) //left max
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


        //locking the X rotation
        transform.rotation = Quaternion.Euler(lockRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);


        //blocking further rotation check

        
        if(transform.rotation.eulerAngles.z <= rightAngleLock && transform.rotation.eulerAngles.z >= 180) //right rotation
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rightAngleLock); //cannot modify so there needs to be a converter 
            pseudoCurrentAngle = -pseudoMaxAngle;
        }
        if (transform.rotation.eulerAngles.z >= leftAngleLock && transform.rotation.eulerAngles.z <= 180) //left rotation
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, leftAngleLock); //cannot modify so there needs to be a converter
            pseudoCurrentAngle = pseudoMaxAngle;
        }
        

    }
}
