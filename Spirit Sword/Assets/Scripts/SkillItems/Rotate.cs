using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    private void FixedUpdate()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
