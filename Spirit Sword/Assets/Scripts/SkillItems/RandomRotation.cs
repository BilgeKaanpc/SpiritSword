using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomRotate = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(transform.rotation.x, randomRotate, transform.rotation.z);
    }

}
