using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField ]float power;
    // Start is called before the first frame update
    void Start()
    {
        power = 8 + (PlayerPrefs.GetFloat("bonusDamageLevel") - 1) * (PlayerPrefs.GetFloat("bonusDamageLevel"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void xpAdd(float givenXp)
    {
        Debug.Log("girdiyo");
        GameObject lvlController = GameObject.Find("LevelController");
        Debug.Log(lvlController);
        PlayerPrefs.SetFloat("XP", PlayerPrefs.GetFloat("XP") + givenXp);
        Debug.Log(PlayerPrefs.GetFloat("XP"));
        lvlController.GetComponent<levelController>().nowXp = PlayerPrefs.GetFloat("XP");
        if (lvlController.GetComponent<levelController>().xp[PlayerPrefs.GetInt("Level") - 1] <= PlayerPrefs.GetFloat("XP"))
        {

            PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") + 1);


            PlayerPrefs.SetFloat("XP", 0);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            GameObject main = GameObject.Find("MainCharacter");
            main.GetComponent<CharController>().lvlText.text = PlayerPrefs.GetInt("Level").ToString();
        }
        lvlController.GetComponent<levelController>().xpText.text = PlayerPrefs.GetFloat("XP") + "/" + lvlController.GetComponent<levelController>().xp[PlayerPrefs.GetInt("Level") - 1];
        lvlController.GetComponent<levelController>().xpBar.fillAmount = PlayerPrefs.GetFloat("XP") / lvlController.GetComponent<levelController>().xp[PlayerPrefs.GetInt("Level") - 1];
    }
    IEnumerator Kill(GameObject enemy)
    {
        if(enemy.tag == "Skeleton_lvl1")
        {
            xpAdd(enemy.GetComponent<skeletonController>().givenXp);
            enemy.GetComponent<Animator>().Play("SkeletonOutlaw@Dead00");
            enemy.GetComponent<skeletonController>().isAlive = false;
            enemy.GetComponent<skeletonController>().healt = 0;
            enemy.GetComponent<skeletonController>().canvas.enabled = false;
            enemy.GetComponent<CapsuleCollider>().enabled = false;
            enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(1);

            enemy.GetComponent<Animator>().enabled = false;
            yield return new WaitForSeconds(3);
            Destroy(enemy);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Skeleton_lvl1")
        {
            other.gameObject.GetComponent<skeletonController>().healt -= power;
            if (other.gameObject.GetComponent<skeletonController>().healt <= 0)
            {

                StartCoroutine(Kill(other.gameObject));
            }
            else
            {
                other.gameObject.GetComponent<Animator>().Play("SkeletonOutlaw@Damage00");
                StartCoroutine(other.gameObject.GetComponent<skeletonController>().noDamage());
                
                
            }
        }
    }
}
