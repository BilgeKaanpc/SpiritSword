using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCircleRandomPosition : MonoBehaviour
{
    [SerializeField] GameObject flame;

    public Transform center;
    public float radius = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createFlame());
    }

    IEnumerator createFlame()
    {
        for(int i = 0; i <8; i++)
        {
            float angle = Random.Range(0.0f, 360.0f);
            float x = center.position.x + radius * Mathf.Cos(angle);
            float z = center.position.z + radius * Mathf.Sin(angle);
            Vector3 position = new Vector3(x, 1, z);
            GameObject flameObject =  Instantiate(flame, position,Quaternion.identity);
            flameObject.transform.parent = transform;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
