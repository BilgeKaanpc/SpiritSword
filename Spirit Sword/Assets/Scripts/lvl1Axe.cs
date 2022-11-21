using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1Axe : MonoBehaviour
{
    [SerializeField] int axePower;
    bool canHit = true;
    Animator heroAnimator;
    // Start is called before the first frame update
    void Start()
    {
        heroAnimator = GameObject.Find("MainCharacter").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator attackWait()
    {
        yield return new WaitForSeconds(0.2f);
        heroAnimator.Play("GetHit01_SwordAndShield");

        yield return new WaitForSeconds(0.7f);
        canHit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (canHit)
            {

                canHit = false;
                CharController.healt -= axePower;
                Debug.Log("Hero: " + CharController.healt);
                StartCoroutine(attackWait());
            }
        }
    }
}
