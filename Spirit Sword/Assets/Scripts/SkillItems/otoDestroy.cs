using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otoDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(otoDest());
    }

    IEnumerator otoDest()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
