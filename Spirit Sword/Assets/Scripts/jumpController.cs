using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpController : MonoBehaviour
{
    public static bool canJump = true;

    [SerializeField] Animator animator;
    [SerializeField] GameObject onGroundEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (!canJump)
            {
                animator.Play("JumpEnd_Normal_InPlace_SwordAndShield");
                animator.SetInteger("walk", 0);
                canJump = true;
                StartCoroutine(fallAnimation(transform.position,transform.rotation));
               
            }
        }
    }
    IEnumerator fallAnimation(Vector3 position,Quaternion rotation)
    {
        GameObject animation = Instantiate(onGroundEffect,position,rotation);
        yield return new WaitForSeconds(1);
        Destroy(animation);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            
            canJump = true;
        }
    }
}
