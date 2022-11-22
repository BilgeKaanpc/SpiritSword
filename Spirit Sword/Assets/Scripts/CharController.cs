using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;
    [SerializeField] GameObject sword;
    public static bool isTouch = true;
    int attackCount = 0;
    bool canTurn = true;
    public Button strongAttackButton;
    bool doubleJump = false;
    [SerializeField] float jumpPower;
    [SerializeField] float doubleJumpPower;

    [SerializeField] Animator animator;

    public static int healt;

    public GameObject head;
    bool attackMoment = false;
    //Animations
    [SerializeField] Animation walkAnimation;

    Vector3 stop = new Vector3(0, 0, 0);
    // joystik
    public float speed;
    public FixedJoystick veriableJoyStick;
    public Rigidbody rb;
    public Transform tr;
    Vector3 oldPosition;
    Vector3 direction;
    bool isGround = false;
    void Start()
    {
        sword.GetComponent<BoxCollider>().enabled = false;
        healt = 100;
        //Initialization our angles of camera
        xAngle = 0.0f;
        yAngle = 0.0f;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }
    
    private void FixedUpdate()
    {
        if (!attackMoment)
        {
            direction = tr.forward * veriableJoyStick.Vertical + tr.right * veriableJoyStick.Horizontal;
            rb.velocity = new Vector3(direction.x * speed * Time.fixedDeltaTime, rb.velocity.y, direction.z * speed * Time.fixedDeltaTime);
            //rb.velocity = direction * speed * Time.fixedDeltaTime;
        }
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
   
    public void Jump()
    {
        if (!attackMoment && jumpController.canJump && animator.GetInteger("attack") == 0)
        {

            animator.Play("JumpStart_Normal_InPlace_SwordAndShield");
            doubleJump = true;
            rb.velocity = new Vector3(rb.velocity.x, 1f * jumpPower, rb.velocity.z);
            
        }else if (doubleJump)
        {
            animator.Play("JumpAir_Spin_InPlace_SwordAndShield");

            rb.velocity = new Vector3(rb.velocity.x, 1f * jumpPower, rb.velocity.z);

            doubleJump = false;
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
        if (!attackMoment && animator.GetInteger("attack") == 0)
        {
            if (veriableJoyStick.Vertical > 0.9f)
            {
                animator.speed = 1;
                animator.SetInteger("walk", 2);
            }
            else if (veriableJoyStick.Horizontal > 0.3f)
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
    IEnumerator attackMomentChange()
    {


        attackMoment = true;
        sword.GetComponent<BoxCollider>().enabled = true;

        rb.velocity = new Vector3(0, 0, 0);

        animator.speed = 1.5f;
        switch (attackCount)
        {
            case 0:

                animator.Play("Attack01_SwordAndShiled");
                attackCount++;
                break;
            case 1:
                animator.Play("Attack02_SwordAndShiled");
                attackCount++;
                break;
            case 2:
                animator.Play("Attack03_SwordAndShiled");
                attackCount = 0;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.3f);

        sword.GetComponent<BoxCollider>().enabled = false;
        attackMoment = false;
        animator.speed = 1;

    }
    
    public void strongAttack()
    {
        if (canTurn )
        {
            jumpController.canJump = false;
            StartCoroutine(strongAttackCounter());
        }
    }
    IEnumerator strongAttackCounter()
    {
        canTurn = false;
        strongAttackButton.interactable = false;
        animator.speed = 1;
        sword.GetComponent<BoxCollider>().enabled = true;
        animator.SetInteger("attack", 5);
        yield return new WaitForSeconds(5);
        animator.SetInteger("attack", 0);
        sword.GetComponent<BoxCollider>().enabled = false;
        jumpController.canJump = true;
        yield return new WaitForSeconds(15);
        strongAttackButton.interactable = true;
        canTurn = true;
    }
    public void attackAnimation()
    {
        if (!attackMoment && animator.GetInteger("attack") ==0 &&  jumpController.canJump)
        {

            StartCoroutine(attackMomentChange());

        }

        
    }
}
