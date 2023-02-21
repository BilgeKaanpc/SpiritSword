using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1Axe : MonoBehaviour
{
    [SerializeField] int axePower;
    bool canHit = true;
    Animator heroAnimator;
    GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.Find("MainCharacter");
        heroAnimator = mainCharacter.GetComponent<Animator>();

    }

   
    IEnumerator attackWait(GameObject other)
    {

        canHit = false;
        if (!GetComponentInParent<Spider>())
        {
            yield return new WaitForSeconds(0.2f);
        }
        if (CharController.canHittable)
        {
            if (mainCharacter.GetComponent<CharController>().isAlive)
            {
                heroAnimator.Play("GetHit01_SwordAndShield");
                if (0 >= CharController.healt - axePower)
                {
                    CharController.healt = 0;
                    mainCharacter.GetComponent<CharController>().isAlive = false;
                    if(0 >=  PlayerPrefs.GetFloat("XP") - (PlayerPrefs.GetInt("Level") * 10))
                    {
                        PlayerPrefs.SetFloat("XP", 0);
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("XP", PlayerPrefs.GetFloat("XP") - (PlayerPrefs.GetInt("Level") * 10));

                    }
                    GameObject xpReset = GameObject.Find("LevelController");
                    xpReset.GetComponent<levelController>().xpText.text = PlayerPrefs.GetFloat("XP") + "/" + (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));
                    xpReset.GetComponent<levelController>().xpBar.fillAmount = PlayerPrefs.GetFloat("XP") / (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));
                    heroAnimator.Play("Die01_SwordAndShield");
                }
                else
                {
                    CharController.healt -= axePower;

                }
                StartCoroutine(other.GetComponent<CharController>().canheal());
            }
            
        }
        yield return new WaitForSeconds(0.7f);
        canHit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (canHit)
            {
                StartCoroutine(attackWait(other.gameObject));
            }
        }
    }
}
