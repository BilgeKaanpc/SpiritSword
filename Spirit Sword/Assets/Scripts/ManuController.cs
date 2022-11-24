using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuController : MonoBehaviour
{
    //Camera Positions
    // main 0.07 - 1.8 - 6.12 / 9.7 - 28.5 - 0
    // Equipmans 2 - 1.7 - 0.3 / 14 - 135 - 0
    // Settings 0.1 - 1.5 - 1.6 / 2 - 180 - 0
    // Map -1.7 - 3 - 0 / 50 - 270 - 0

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Equipmans()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Sttings()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Map()
    {
        yield return new WaitForSeconds(1);
    }
}
