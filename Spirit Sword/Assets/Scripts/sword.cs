using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField ]int power;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Kill(GameObject enemy)
    {
        if(enemy.tag == "Skeleton_lvl1")
        {
            enemy.GetComponent<Animator>().Play("SkeletonOutlaw@Dead00");
            enemy.GetComponent<skeletonController>().isAlive = false;
            enemy.GetComponent<skeletonController>().healt = 0;
            enemy.GetComponent<skeletonController>().canvas.enabled = false;
            enemy.GetComponent<CapsuleCollider>().enabled = false;
        }
        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(1);

        enemy.GetComponent<Animator>().enabled = false;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            other.gameObject.GetComponent<skeletonController>().healt -= power;
            if (other.gameObject.GetComponent<skeletonController>().healt <= 0)
            {

                StartCoroutine(Kill(other.gameObject));
            }
            else
            {
                other.gameObject.GetComponent<Animator>().Play("SkeletonOutlaw@Damage00");
                
            }
        }
    }
}
