using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] float damage;
    [SerializeField] bool isShouldWait = false;
    bool canHit = false;
    private void Start()
    {
        canHit = false;
        if (isShouldWait)
        {
            StartCoroutine(waitForExplosion());
        }
    }
    IEnumerator waitForExplosion()
    {
        yield return new WaitForSeconds(4);
        canHit = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            DamageForSkeleton(other);
        }
    }


    public void DamageForSkeleton(Collider other)
    {
        StartCoroutine(hitEffectDestroy(other));
        other.gameObject.GetComponent<skeletonController>().healt -= damage;
        if (other.gameObject.GetComponent<skeletonController>().healt <= 0)
        {

            // StartCoroutine(Kill(other.gameObject));
            GameObject mainC = GameObject.Find("MainCharacter");

            mainC.GetComponentInChildren<sword>().Kill(other.gameObject);
        }
        else
        {
            other.gameObject.GetComponent<Animator>().Play("SkeletonOutlaw@Damage00");



        }
        if (gameObject.tag == "destroyableSkill")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(gameObject.tag != "oneHitSkill")
        {

            if (other.gameObject.tag == "Skeleton_lvl1")
            {
                if(isShouldWait)
                {
                    if (canHit)
                    {
                        DamageForSkeleton(other);
                    }
                }
                else
                {
                    DamageForSkeleton(other);
                }


            }
        }
        else
        {
            if (other.gameObject.tag == "Skeleton_lvl1")
                {
                    DamageForSkeleton(other);
                }
        }
    }


    IEnumerator hitEffectDestroy(Collider other)
    {
        Vector3 collisionPoint = new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z);
        GameObject hit = Instantiate(hitEffect, collisionPoint, Quaternion.identity);
        hit.transform.parent = other.transform;
        yield return new WaitForSeconds(0.5f);
        Destroy(hit);
    }
}
