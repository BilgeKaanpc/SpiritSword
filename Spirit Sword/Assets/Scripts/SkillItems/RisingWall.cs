using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWall : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(destroyObject());
    }

    private void FixedUpdate()
    {
        
        float newHeight = Mathf.Lerp(transform.position.y, 0.5f, Time.deltaTime * 7);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }


    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
