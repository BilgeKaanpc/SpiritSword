using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookControl : MonoBehaviour
{
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
        lookActivity(other, true);
    }

    public void lookActivity(Collider other,bool action)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "skeletonLook":
                    GetComponentInParent<skeletonController>().canLook = action;
                    break;
                case "spiderLook":
                    GetComponentInParent<Spider>().canLook = action;
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        lookActivity(other, false);
    }
}
