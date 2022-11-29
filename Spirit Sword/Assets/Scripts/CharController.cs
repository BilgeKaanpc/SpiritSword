using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharController : MonoBehaviour
{

    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;
    [SerializeField] GameObject sword;
    [SerializeField] TextMeshProUGUI healtText;
    [SerializeField] public TextMeshProUGUI lvlText;
    [SerializeField] TextMeshProUGUI skillDurationText;
    [SerializeField] Image healthBar;
    [SerializeField] Image skillDurationImage;
    [SerializeField] public GameObject restart;
    public static bool isTouch = true;
    int attackCount = 0;
    bool canTurn = true;
    public Button strongAttackButton;
    bool doubleJump = false;
    [SerializeField] float jumpPower;
    [SerializeField] float doubleJumpPower;
    float maxHealth;
    [SerializeField] Animator animator;
    public static bool canHittable = true;
    public static int canHealable = 0;
    public static float healt;
    public bool isAlive = true;

    [SerializeField] List<GameObject> swords = new List<GameObject>();

    public GameObject head;
    bool attackMoment = false;
    //Animations

    Vector3 stop = new Vector3(0, 0, 0);
    // joystik
    float speed;
    public FixedJoystick veriableJoyStick;
    public Rigidbody rb;
    public Transform tr;
    Vector3 oldPosition;
    Vector3 direction;
    float duration = 0;
    float fullDuration = 0;

    [SerializeField] GameObject enemy;

    //Formules
    //Bonus Damage Formul (n-1).n   -  n = PLayerPrefs.getint("damage");
    // bonus health Formul (5/2) * ((n-1)n)      n  = PLayerPrefs.getint("health");
    // speed formul  250 + PLayerPrefs.getFloat("speed")    -      pref += 10;
    //  regen formul  1.5f - PlayerPrefs.getFloat("speed")     -       pref+= 0.1f;  


    public void returnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwordChange(int index)
    {
        sword.GetComponent<BoxCollider>().size = swords[index].GetComponent<BoxCollider>().size;
        sword.GetComponent<MeshFilter>().sharedMesh = swords[index].GetComponent<MeshFilter>().sharedMesh;
        sword.GetComponent<MeshRenderer>().sharedMaterial = swords[index].GetComponent<MeshRenderer>().sharedMaterial;
    }


    public void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 30f;
        Vector3 newSpawnArea = new Vector3(spawnDirection.x, 0, spawnDirection.y);
        Vector3 spawnPoint = transform.position + newSpawnArea;
        Quaternion newRotation = Quaternion.Euler(enemy.transform.rotation.x, Random.Range(0, 360), enemy.transform.rotation.z);
        GameObject enemySpawn = Instantiate(enemy, spawnPoint, newRotation);
    }
    void Start()
    {
        SwordChange(PlayerPrefs.GetInt("Level"));
        Debug.Log(sword.GetComponent<BoxCollider>().size);
        InvokeRepeating(nameof(Spawn), 2f, 2f);
        speed = 250 +  + PlayerPrefs.GetFloat("speed");
        sword.GetComponent<BoxCollider>().enabled = false;
        healt = PlayerPrefs.GetFloat("MaxHealt") + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel")));

        lvlText.text = PlayerPrefs.GetInt("Level").ToString();
        maxHealth = healt;
        //Initialization our angles of camera
        xAngle = 0.0f;
        yAngle = 0.0f;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        StartCoroutine(HealtAdd());
    }
    
    IEnumerator buttonActive()
    {
        yield return new WaitForSeconds(1);
        restart.SetActive(true);

    }
    private void FixedUpdate()
    {
        SwordChange(PlayerPrefs.GetInt("Level"));
        if (healt<=0 && !restart.activeInHierarchy)
        {
            StartCoroutine(buttonActive());
        }
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            skillDurationImage.fillAmount = duration / fullDuration;

        }
        else
        {
            skillDurationImage.fillAmount = 0;

        }
       
        
        healtText.text = healt+"/"+maxHealth;
        healthBar.fillAmount = healt / maxHealth;
        if (isAlive)
        {
            if (!attackMoment)
            {
                direction = tr.forward * veriableJoyStick.Vertical + tr.right * veriableJoyStick.Horizontal;
                rb.velocity = new Vector3(direction.x * speed * Time.fixedDeltaTime, rb.velocity.y, direction.z * speed * Time.fixedDeltaTime);
                //rb.velocity = direction * speed * Time.fixedDeltaTime;
            }
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
    public IEnumerator canheal()
    {
        canHealable += 5;
        while (canHealable > 0)
        {
            canHealable -= 1;
            yield return new WaitForSeconds(1);
        }
    }
    public IEnumerator HealtAdd()
    {
       while (isAlive)
       {
           if (canHealable == 0)
           {
                if (healt < maxHealth)
                {
                    healt++;
                }
           }

           yield return new WaitForSeconds(1.5f - (PlayerPrefs.GetFloat("regen")/10));
        }
        
        
    }
    public void Jump()
    {
        if (isAlive)
        {

            if (!attackMoment && jumpController.canJump && animator.GetInteger("attack") == 0)
            {

                animator.Play("JumpStart_Normal_InPlace_SwordAndShield");
                doubleJump = true;
                rb.velocity = new Vector3(rb.velocity.x, 1f * jumpPower, rb.velocity.z);

            }
            else if (doubleJump)
            {
                animator.Play("JumpAir_Spin_InPlace_SwordAndShield");

                rb.velocity = new Vector3(rb.velocity.x, 1f * jumpPower, rb.velocity.z);

                doubleJump = false;
            }
        }


    }
    public void StopAnimation()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
       
        if (isAlive)
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

                    int index = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        if (Input.GetTouch(i).position.x > 750)
                        {
                            index = i;
                        }
                    }
                    if (Input.GetTouch(index).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(index).position;
                        xAngTemp = xAngle;
                        yAngTemp = yAngle;

                    }
                    //Move finger by screen
                    if (Input.GetTouch(index).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(index).position;

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "health")
        {
            Destroy(other.gameObject);
            if((healt + other.gameObject.GetComponent<AnimationScript>().health) > maxHealth)
            {
                healt = maxHealth;
            }
            else
            {

                healt += other.gameObject.GetComponent<AnimationScript>().health;
            }
        }
    }
    public void strongAttack()
    {
        if (isAlive)
        {

            if (canTurn)
            {
                jumpController.canJump = false;
                StartCoroutine(strongAttackCounter());
            }
        }
    }
    IEnumerator skillDuration()
    {
        duration = 20;
        fullDuration = duration;
        while(duration > 0)
        {
            skillDurationText.text = duration.ToString("0");
            yield return new WaitForSeconds(1);
        }
        skillDurationText.text = "";
    }
    IEnumerator strongAttackCounter()
    {
        canTurn = false;
        strongAttackButton.interactable = false;
        animator.speed = 1;
        sword.GetComponent<BoxCollider>().enabled = true;
        canHittable = false;
        StartCoroutine(skillDuration());
        animator.SetInteger("attack", 5);
        yield return new WaitForSeconds(5);
        animator.SetInteger("attack", 0);
        canHittable = true;
        sword.GetComponent<BoxCollider>().enabled = false;
        jumpController.canJump = true;
        yield return new WaitForSeconds(15);

        strongAttackButton.interactable = true;
        canTurn = true;
    }
    public void attackAnimation()
    {
        if (isAlive)
        {

            if (!attackMoment && animator.GetInteger("attack") == 0 && jumpController.canJump)
            {

                StartCoroutine(attackMomentChange());

            }
        }

        
    }
}
