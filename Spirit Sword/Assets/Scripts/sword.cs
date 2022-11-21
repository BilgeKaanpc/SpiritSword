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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            if(other.gameObject.GetComponent<skeletonController>().healt > 0)
            {

                other.gameObject.GetComponent<skeletonController>().healt -= power;
                Debug.Log("Enemy: " + other.gameObject.GetComponent<skeletonController>().healt);
            }
        }
    }
}
