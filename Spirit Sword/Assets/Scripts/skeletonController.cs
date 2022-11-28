using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class skeletonController : MonoBehaviour
{
    Rigidbody rb;
    Transform tr;
    Animator animator;
    [SerializeField] float followSpeed;
    [SerializeField] int power;
    [SerializeField] TextMeshProUGUI healtText;
    [SerializeField] GameObject axe;
    [SerializeField] public float givenXp;
    [SerializeField] Image healtBar;
    [SerializeField] public Canvas canvas;
    float followX, followZ, distance;
    bool canHit = true;
    int hitDuration = 0;
    public float healt;
    float maxHealt;
    Transform hero;
    public bool isAlive = true;
    public bool canLook = false;
    bool randomWalk = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(randomMove());
        maxHealt = healt;
        axe.GetComponent<BoxCollider>().enabled = false;

        hero = GameObject.Find("MainCharacter").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        tr = transform;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healtText.text = healt.ToString();
    }
    IEnumerator attackDration()
    {
        /*  canHealable += 5;
        while (canHealable > 0)
        {
            canHealable -= 1;
            yield return new WaitForSeconds(1);
        }*/
            axe.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);

        axe.GetComponent<BoxCollider>().enabled = false;
        canHit = true;
    }
    //hasat aldýktan sonra beklemesi
    public IEnumerator noDamage()
    {
        hitDuration ++;
       
            yield return new WaitForSeconds(1);
            hitDuration--;
       
    }

    IEnumerator randomMove()
    {

        yield return new WaitForSeconds(5);
        randomWalk = false;

        animator.SetInteger("walk", 0);
    }
    private void FixedUpdate()
    {
        if(transform.position.y < -2)
        {
            Destroy(gameObject);
        }
        if (hero.GetComponent<CharController>().isAlive){
            if (isAlive)
            {
                if (rb.velocity.magnitude > 1)
                {
                    animator.SetInteger("walk", 1);
                }
                else
                {
                    animator.SetInteger("walk", 0);
                }
                if (canLook)
                {
                    healtBar.fillAmount = healt / maxHealt;
                    var lookPos = hero.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
                    var direction = tr.forward + tr.right;
                    distance = Vector3.Distance(transform.position, hero.position);

                    if (distance < 2 && canHit && hitDuration == 0)
                    {
                        canHit = false;
                        animator.Play("SkeletonOutlaw@Attack01");
                        StartCoroutine(attackDration());
                        rb.velocity = new Vector3(0, 0, 0);
                    }
                    if (distance > 1.7)
                    {
                        rb.velocity = transform.forward * followSpeed * Time.deltaTime;

                    }

                   
                }
            }
            if (randomWalk)
            {
                rb.velocity = rb.velocity = transform.forward * followSpeed * Time.deltaTime;
                if (rb.velocity.magnitude > 1)
                {
                    animator.SetInteger("walk", 1);
                }
                else
                {
                    animator.SetInteger("walk", 0);
                }
            }
        }
        else
        {
            if (!GameObject.Find("MainCharacter").GetComponent<CharController>().restart.activeInHierarchy)
            {
                StartCoroutine(win());
            }
        }
        if (healt <= 0)
        {
            healt = 0;
        }
       
           
        
        //tr.LookAt(hero);
    }
    IEnumerator win()
    {
        animator.Play("SkeletonOutlaw@Idle01 0");
        yield return new WaitForSeconds(2);
    }
    IEnumerator death()
    {
        isAlive = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        
        yield return new WaitForSeconds(1);
        animator.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
       
    }
}
