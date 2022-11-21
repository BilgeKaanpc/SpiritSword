using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skeletonController : MonoBehaviour
{
    Rigidbody rb;
    Transform tr;
    Animator animator;
    [SerializeField] float followSpeed;
    [SerializeField] int power;
    [SerializeField] TextMeshPro healtText;
    [SerializeField] GameObject axe;
    float followX, followZ, distance;
    bool canHit = true;
    public int healt;
    Transform hero;
    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
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
            axe.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);

        axe.GetComponent<BoxCollider>().enabled = false;
        canHit = true;
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {

            var lookPos = hero.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            var direction = tr.forward + tr.right;
            distance = Vector3.Distance(transform.position, hero.position);

            if (distance < 2 && canHit)
            {
                canHit = false;
                animator.Play("SkeletonOutlaw@Attack01");
                StartCoroutine(attackDration());
                rb.velocity = new Vector3(0, 0, 0);
            }
            if (distance > 1.7)
            {
                rb.velocity = new Vector3(lookPos.x * followSpeed * Time.fixedDeltaTime, rb.velocity.y, lookPos.z * followSpeed * Time.fixedDeltaTime);

            }

            if (rb.velocity.magnitude > 1)
            {
                animator.SetInteger("walk", 1);
            }
            else
            {
                animator.SetInteger("walk", 0);
            }
        }
        
        //tr.LookAt(hero);
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
        if(other.gameObject.tag == "sword")
        {
            if(healt < 0)
            {
                animator.Play("SkeletonOutlaw@Dead00");

                StartCoroutine(death());
                
            }
            else
            {
                animator.Play("SkeletonOutlaw@Damage00");

            }
        }
    }
}
