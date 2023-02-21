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
    [SerializeField] GameObject newLevelPanel , plusDamageImage;
    [SerializeField] GameObject swordLight;

    [SerializeField] public List<Sword> swords = new List<Sword>();

    [SerializeField] TextMeshProUGUI oldLevel, newLevel, plusHealth, plusDamage,swordName,newSwordText;
    [SerializeField] GameObject levelUpEffect;

    //Test 
    [SerializeField] int Level;
    [SerializeField] float xp;


    public GameObject head;
    bool attackMoment = false;
    //Animations

    Vector3 stop = new Vector3(0, 0, 0);
    // joystik
    public float speed;
    public FixedJoystick veriableJoyStick;
    public Rigidbody rb;
    public Transform tr;
    Vector3 oldPosition;
    Vector3 direction;
    float duration = 0;
    float fullDuration = 0;
    [SerializeField] Transform newSwordPosition;
    GameObject newSword;
    [SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] GameObject mainGamePanel;
    public bool pause = false;
    public static int sceneCounter = 0;
    bool animationTurn = false;

    //Formules
    //Bonus Damage Formul (n-1).n   -  n = PLayerPrefs.getint("damage");
    // bonus health Formul (5/2) * ((n-1)n)      n  = PLayerPrefs.getint("health");
    // speed formul  250 + PLayerPrefs.getFloat("speed")    -      pref += 10;
    //  regen formul  1.5f - PlayerPrefs.getFloat("speed")     -       pref+= 0.1f;  


    public void returnMenu()
    {
        sceneCounter = 0;
        SceneManager.LoadScene(0);
    }

    public void SwordChange(int index)
    {
        if (index >= 49)
        {
            sword.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        sword.GetComponent<BoxCollider>().size = swords[index].Size;
        sword.GetComponent<MeshFilter>().sharedMesh = swords[index].Mesh;
        sword.GetComponent<MeshRenderer>().sharedMaterial = swords[index].Material;
    }

    GameObject levelupAnim;
    public void LevelUpAnimation()
    {
        levelupAnim = Instantiate(levelUpEffect, transform.position, transform.rotation);
        animator.Play("LevelUp_Battle_SwordAndShield");
        levelupAnim.SetActive(true);
        newLevelPanel.SetActive(true);
        pause = true;
        mainGamePanel.SetActive(false);
        rb.velocity = new Vector3(0, 0, 0);
        if (PlayerPrefs.GetInt("Level") %2 == 1)
        {
            swordLight.SetActive(true);
            animationTurn = true;
            string square = (Mathf.Pow((-1), PlayerPrefs.GetInt("Level") + 1)).ToString();
            Debug.Log(square);
            int index = (((2 * PlayerPrefs.GetInt("Level")) + int.Parse(square) + 1)) / 4;
            Vector3 playerTransform = new Vector3(transform.position.x - 2.5f, 1, transform.position.z);
            newSword = Instantiate(swords[index - 1].SwordObject, newSwordPosition);
            plusDamageImage.SetActive(true);
            plusHealth.text = "+" + ((5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20)) - (5 * (Mathf.Pow((PlayerPrefs.GetInt("Level") - 1), 2) - (PlayerPrefs.GetInt("Level") - 1) + 20))).ToString();
            oldLevel.text = (PlayerPrefs.GetInt("Level") - 1).ToString();
            newLevel.text = (PlayerPrefs.GetInt("Level")).ToString();
            plusDamage.text = "+" + (swords[index - 1].Power - swords[index - 2].Power).ToString();
            newSword.GetComponent<BoxCollider>().isTrigger = true;
            swordName.text = swords[index - 1].Name;
            newSwordText.text = "New Sword";


        }
        else
        {

            plusHealth.text = "+" + ((5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20)) - (5 * (Mathf.Pow((PlayerPrefs.GetInt("Level") - 1), 2) - (PlayerPrefs.GetInt("Level") - 1) + 20))).ToString();
            oldLevel.text = (PlayerPrefs.GetInt("Level") - 1).ToString();
            newLevel.text = (PlayerPrefs.GetInt("Level")).ToString();
            plusDamage.text = "";
            plusDamageImage.SetActive(false);
            swordName.text = "";
            newSwordText.text = "";
        }
        
    }

    public void closeNewLevelPanel()
    {
        levelupAnim.SetActive(false);
        newLevelPanel.SetActive(false);
        Destroy(newSword);
        plusDamageImage.SetActive(false);
        mainGamePanel.SetActive(true);
        pause = false;
        animationTurn = false;
        swordLight.SetActive(false);

    }
    public void LevelUp()
    {
        string square = (Mathf.Pow((-1), PlayerPrefs.GetInt("Level") + 1)).ToString();
        int index = (((2 * PlayerPrefs.GetInt("Level")) + int.Parse(square) + 1)) / 4;
        SwordChange(index-1);
        maxHealth = 5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20) + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel")));

        sword.GetComponent<sword>().power = swords[index-1].Power + (PlayerPrefs.GetFloat("bonusDamageLevel") - 1) * (PlayerPrefs.GetFloat("bonusDamageLevel"));

    }
    public void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 30f;
        Vector3 newSpawnArea = new Vector3(spawnDirection.x, 0, spawnDirection.y);
        Vector3 spawnPoint = transform.position + newSpawnArea;
        Quaternion newRotation = Quaternion.Euler(enemy.transform.rotation.x, Random.Range(0, 360), enemy.transform.rotation.z);
        int randomIndex = Random.Range(0, 100);
        int level = PlayerPrefs.GetInt("Level");
        switch(level)
        {
            case < 2:
                if (randomIndex < 90)
                {

                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                break;
            case < 4:
                if(randomIndex < 50)
                {
                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }else if (randomIndex < 90)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                break;
            case < 6:
                if (randomIndex < 20)
                {
                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else if (randomIndex < 60)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if(randomIndex < 90)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                break;
            case < 10:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                break;
            case < 12:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                break;
            case < 14:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[5], spawnPoint, newRotation);
                }
                break;
            case < 16:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[5], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[6], spawnPoint, newRotation);
                }
                break;
            case > 16:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[5], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[6], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[7], spawnPoint, newRotation);
                }
                break;
            default:
                break;
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("Level", Level);
        if(sceneCounter == 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            sceneCounter = 1;
            SceneManager.LoadScene(1);
        }
        LevelUp();
        InvokeRepeating(nameof(Spawn), 2f, 2f);
        speed = 250 + PlayerPrefs.GetFloat("speed");
        sword.GetComponent<BoxCollider>().enabled = false;
        healt = 5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20) + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel")));

        lvlText.text = PlayerPrefs.GetInt("Level").ToString();
        maxHealth = healt;
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
        if (animationTurn)
        {
            
            newSword.transform.Rotate(Vector3.up, Time.deltaTime * 100);
            
        }
        if (healt<=0 && !restart.activeInHierarchy)
        {
            StartCoroutine(buttonActive());
        }
       
        
        healtText.text = healt+"/"+maxHealth;
        healthBar.fillAmount = healt / maxHealth;
        if (isAlive)
        {
            if (!attackMoment)
            {
                if (!pause)
                {

                    direction = tr.forward * veriableJoyStick.Vertical + tr.right * veriableJoyStick.Horizontal;
                    rb.velocity = new Vector3(direction.x * speed * Time.fixedDeltaTime, rb.velocity.y, direction.z * speed * Time.fixedDeltaTime);
                }
                else
                {
                    animator.SetInteger("walk", 0);
                    rb.velocity = new Vector3(0, 0, 0);
                }
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


        if (isAlive && !pause)
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
        GameObject method = GameObject.Find("SkillManager");
        StartCoroutine(method.GetComponent<Skills>().skillDuration_1(20));
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
    [System.Serializable]
public class Sword
{
    public float _power;
    public string _name;
    public GameObject _swordObject;
    Material _material;
    Mesh _mesh;
    Vector3 _size;

    public Sword(float powerC , GameObject swordObject)
    {
        _power = powerC;
        _swordObject = swordObject;

    }

    public GameObject SwordObject
        {
            get { return _swordObject;}
        }
    public float Power
    {
        get { return _power; }
        set { _power = value; }
    }
    public string Name
    {
        get { return _name;}
        set { _name = value;}
    }
    public Material Material
    {
        get { return _swordObject.GetComponent<MeshRenderer>().sharedMaterial; }
        set { _material = value; }
    }
    public Mesh Mesh
    {
        get { return _swordObject.GetComponent<MeshFilter>().sharedMesh; }
        set { _mesh = value; }
    }
    public Vector3 Size
    {
        get { return _swordObject.GetComponent<BoxCollider>().size; }
        set { _size = value; }
    }
}
}


