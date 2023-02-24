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
        if(other.gameObject.tag == "spider")
        {
            DamageForSpider(other);
        }
    }


    public void DamageForSkeleton(Collider other)
    {
        StartCoroutine(hitEffectDestroy(other, 1.5f));
        other.gameObject.GetComponent<skeletonController>().healt -= damage;
        if (other.gameObject.GetComponent<skeletonController>().healt <= 0)
        {
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
    public void DamageForSpider(Collider other)
    {
        StartCoroutine(hitEffectDestroy(other,0.5f));
        other.gameObject.GetComponent<Spider>().healt -= damage;
        if (other.gameObject.GetComponent<Spider>().healt <= 0)
        {
            GameObject mainC = GameObject.Find("MainCharacter");
            mainC.GetComponentInChildren<sword>().Kill(other.gameObject);
        }
        else
        {
            other.gameObject.GetComponent<Animator>().Play("TakeDamage");
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
            if(other.gameObject.tag == "spider")
            {
                if (isShouldWait)
                {
                    if (canHit)
                    {
                        DamageForSpider(other);
                    }
                }
                else
                {
                    DamageForSpider(other);
                }

            }
        }
    }


    IEnumerator hitEffectDestroy(Collider other,float heigh)
    {
        Vector3 collisionPoint = new Vector3(other.transform.position.x, other.transform.position.y + heigh, other.transform.position.z);
        GameObject hit = Instantiate(hitEffect, collisionPoint, Quaternion.identity);
        hit.transform.parent = other.transform;
        yield return new WaitForSeconds(0.5f);
        Destroy(hit);
    }
}
