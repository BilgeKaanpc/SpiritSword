using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

    public bool isAnimated = false;

    public bool isRotating = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;
    public int health;
   


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       
        
        if(isAnimated)
        {
            if(isRotating)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            }


        }
	}
}
