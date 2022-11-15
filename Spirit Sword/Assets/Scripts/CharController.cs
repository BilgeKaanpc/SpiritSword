using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;
    public static bool isTouch = true;

    [SerializeField] Animator animator;

    public GameObject head;

    //Animations
    [SerializeField] Animation walkAnimation;

    Vector3 stop = new Vector3(0, 0, 0);
    // joystik
    public float speed;
    public FixedJoystick veriableJoyStick;
    public Rigidbody rb;
    public Transform tr;
    Vector3 oldPosition;
    void Start()
    {
        //Initialization our angles of camera
        xAngle = 0.0f;
        yAngle = 0.0f;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }
    
    private void FixedUpdate()
    {
        Vector3 direction = tr.forward * veriableJoyStick.Vertical + tr.right * veriableJoyStick.Horizontal;
        rb.velocity = direction * speed * Time.fixedDeltaTime;

        Debug.Log("x deðeri:" + veriableJoyStick.Vertical);
        Debug.Log("y deðeri:" + veriableJoyStick.Horizontal);
        Debug.Log("z deðeri:" + direction.z);


        // z ileri geri
        // x sag sol



        if (direction != stop)
            {
                isTouch = true;
            }
            else
            {
                isTouch = false;
            }
        
        
    }
    public void StopAnimation()
    {
        if (gameObject.GetComponent<Animator>().enabled == true)
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
    void Update()
    {
        if (veriableJoyStick.Vertical > 0.9f)
        {
            animator.speed = 1;
            animator.SetInteger("walk", 2);
        }else if (veriableJoyStick.Horizontal > 0.3f)
        {
            animator.speed = veriableJoyStick.Horizontal;
            animator.SetInteger("walk", -3);
        }
        else if (veriableJoyStick.Horizontal < -0.3f)
        {
            animator.speed = -veriableJoyStick.Horizontal;
            animator.SetInteger("walk", -2);
        }
        else if (veriableJoyStick.Vertical > 0.0f)
        {
            animator.SetInteger("walk", 1);
            animator.speed = veriableJoyStick.Vertical;
        }
        else if (veriableJoyStick.Vertical == 0)
        {
            animator.SetInteger("walk", 0);
            animator.speed = 1;
        }
        else if (veriableJoyStick.Vertical < 0)
        {
            animator.SetInteger("walk", -1);
            animator.speed = -veriableJoyStick.Vertical;
        }



        if (!isTouch)
        {

            //Check count touches
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    firstpoint = Input.GetTouch(0).position;


                    xAngTemp = xAngle;
                    yAngTemp = yAngle;

                }
                //Move finger by screen
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    secondpoint = Input.GetTouch(0).position;

                    xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 500.0f / Screen.width;
                    yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 200f / Screen.height;
                    //Rotate camera
                    this.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);

                }
                //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree




            }
            if (Input.touchCount == 0)
            {
                this.transform.rotation = transform.rotation;
            }
        }
        else
        {

            //Check count touches
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    firstpoint = Input.GetTouch(1).position;
                    Debug.Log(firstpoint);


                    xAngTemp = xAngle;
                    yAngTemp = yAngle;

                }
                //Move finger by screen
                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    secondpoint = Input.GetTouch(1).position;

                    xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 500.0f / Screen.width;
                    yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 200f / Screen.height;
                    //Rotate camera
                    this.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
                }
                //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree




            }
            if (Input.touchCount == 0)
            {
                this.transform.rotation = transform.rotation;
            }
        }


    }
}
