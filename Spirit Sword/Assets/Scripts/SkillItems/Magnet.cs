using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, new Vector3(transform.position.x,0,transform.position.z), 10 * Time.deltaTime);
        }
    }

}
