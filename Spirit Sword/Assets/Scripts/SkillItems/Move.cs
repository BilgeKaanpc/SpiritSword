using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] GameObject mainChar;
    Rigidbody rb;
    bool canBack;
    private void Start()
    {
        canBack = false;
        rb = gameObject.GetComponent<Rigidbody>();
        if(gameObject.tag == "bumerang")
        {
            StartCoroutine(turnBack());
        }
        else
        {
            StartCoroutine(DestroyGameObject());

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "bumerang")
        {

            if (other.gameObject.tag == "Player" && canBack)
            {
                StartCoroutine(DestroyGameObject());
            }
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = gameObject.transform.forward * -1 * speed * Time.deltaTime;
        if(gameObject.tag == "bumerang")
        {
           
                rb.velocity = gameObject.transform.forward * 1 * speed * Time.deltaTime;
            if (canBack)
            {

                GameObject person = GameObject.Find("MainCharacter");
                var lookPos = person.transform.position - transform.position;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
            
        }
    }
    
    IEnumerator turnBack()
    {
        yield return new WaitForSeconds(5);
        canBack = true;
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
