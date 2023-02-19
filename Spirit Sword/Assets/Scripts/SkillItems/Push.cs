using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
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
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            Vector3 direction = other.transform.position - transform.position; // itme yönü
            direction = direction.normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * 100000, ForceMode.Impulse);

        }
    }

    IEnumerator push(Rigidbody rb ,Vector3 direction)
    {
        int i = 0;
        while (i < 100)
        {
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(direction * 10, ForceMode.Impulse);
        }
    }
}
