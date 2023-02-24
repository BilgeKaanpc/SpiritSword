using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Skeleton_lvl1")
        {
            StartCoroutine(push(other));
        }
        if (other.gameObject.tag == "Spider")
        {
            StartCoroutine(push(other));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Skeleton_lvl1")
        {
            pushMethod(other);
        }
        if (other.gameObject.tag == "spider")
        {
            pushMethod(other);
        }
    }
    void pushMethod(Collider other)
    {

        Vector3 direction = other.transform.position - transform.position;
        direction = direction.normalized;
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        other.gameObject.GetComponent<Rigidbody>().AddForce(direction * 2000, ForceMode.Impulse);
    }
    IEnumerator push(Collider other)
    {

        other.gameObject.GetComponent<skeletonController>().canMove = false;
        yield return new WaitForSeconds(0.5f);
        other.gameObject.GetComponent<skeletonController>().canMove = true;
    }
}
