using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spider : MonoBehaviour
{
    public bool canLook = false;
    public bool isAlive = true;
    float followX, followZ, distance;

    [SerializeField]public float followSpeed;
    [SerializeField] public float givenXp;
    [SerializeField] TextMeshProUGUI healtText;
    [SerializeField] Image healtBar;
    [SerializeField] public Canvas canvas;
    public float healt;

    Transform hero;
    Rigidbody rb;
    Transform tr;
    Animator animator;
    public bool canMove;
    bool randomWalk = true;
    float maxHealt;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        StartCoroutine(randomMove());
        maxHealt = healt;
        hero = GameObject.Find("MainCharacter").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        tr = transform;
        animator = gameObject.GetComponent<Animator>();
    }

    IEnumerator randomMove()
    {

        yield return new WaitForSeconds(5);
        randomWalk = false;

        animator.SetInteger("walking", 0);
    }
    // Update is called once per frame
    void Update()
    {

        healtText.text = healt.ToString();
    }
    private void FixedUpdate()
    {
        if (!hero.GetComponent<CharController>().pause)
        {
            if (transform.position.y < -2)
            {
                Destroy(gameObject);
            }

            if (hero.GetComponent<CharController>().isAlive)
            {
                if (isAlive)
                {

                    if (rb.velocity.magnitude > 1)
                    {
                        animator.SetBool("walking", true);
                    }
                    else
                    {
                        animator.SetBool("walking", false);
                    }


                    if (canLook)
                    {
                        healtBar.fillAmount = healt / maxHealt;
                        var lookPos = hero.position - transform.position;
                        lookPos.y = 0;
                        var rotation = Quaternion.LookRotation(lookPos);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);

                        distance = Vector3.Distance(transform.position, hero.position);


                        if (distance > 1.7)
                        {
                            if (canMove)
                            {
                                rb.velocity = transform.forward * followSpeed * Time.deltaTime;
                            }

                        }
                        if (distance< 2)
                        {
                            animator.Play("Attack1");

                            rb.velocity = new Vector3(0, 0, 0);
                        }
                    }


                }
                if (randomWalk)
                {
                    rb.velocity = rb.velocity = transform.forward * followSpeed * Time.deltaTime;
                    if (rb.velocity.magnitude > 1)
                    {
                        animator.SetInteger("walking", 1);
                    }
                    else
                    {
                        animator.SetInteger("walking", 0);
                    }
                }
            }
        }
    }
}
