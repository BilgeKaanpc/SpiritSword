using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills : MonoBehaviour
{
    //Effects
    [SerializeField] GameObject skill_1_item;
    [SerializeField] GameObject healthEffect;
    [SerializeField] GameObject rasenganEffect;
    [SerializeField] GameObject turningSwords;
    [SerializeField] GameObject flameMeteor;
    [SerializeField] GameObject magnetExplosion;
    [SerializeField] GameObject riseingWall;
    [SerializeField] GameObject pushSkill;
    [SerializeField] GameObject teleporteffect;
    [SerializeField] GameObject speedEffect;
    [SerializeField] GameObject hitableEffect;

    [SerializeField] GameObject fireSkillEffect;

    [SerializeField] TextMeshProUGUI skillDurationText_1;
    [SerializeField] TextMeshProUGUI skillDurationText_2;

    [SerializeField] Button button1, button2;
    [SerializeField] Image skillDurationImage_1, skillDurationImage_2;
    [SerializeField] Image skillImage_1, skillImage_2;

    [SerializeField] GameObject mainCharacter;

    [SerializeField] Animator animator;

    float duration_1;
    float fullDuration_1;
    float duration_2;
    float fullDuration_2;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (duration_1 > 0)
        {
            duration_1 -= Time.deltaTime;
            skillDurationImage_1.fillAmount = duration_1 / fullDuration_1;

        }
        else
        {
            skillDurationImage_1.fillAmount = 0;
            if(button1.interactable == false)
            {
                button1.interactable = true;
            }
        }
        if (duration_2 > 0)
        {
            duration_2 -= Time.deltaTime;
            skillDurationImage_2.fillAmount = duration_2 / fullDuration_2;

        }
        else
        {
            skillDurationImage_2.fillAmount = 0;
            if (button2.interactable == false)
            {
                button2.interactable = true;
            }

        }
    }

    public IEnumerator skillDuration_1(float duration)
    {
        duration_1 = duration;
        fullDuration_1 = duration_1;
        while (duration_1 > 0)
        {
            skillDurationText_1.text = duration_1.ToString("0");
            yield return new WaitForSeconds(1);
        }
        skillDurationText_1.text = "";
    }
    IEnumerator skillDuration_2(float duration)
    {
        duration_2 = duration;
        fullDuration_2 = duration_2;
        while (duration_2 > 0)
        {
            skillDurationText_2.text = duration_2.ToString("0");
            yield return new WaitForSeconds(1);
        }
        skillDurationText_2.text = "";
    }

    public void SkillButton_1()
    {
        button1.interactable = false;
        StartCoroutine(MagnetSkill_Duration());
        //StartCoroutine(createFlameMeteor());
        //StartCoroutine(turnSwords());
        //rasenShuriken();
        //mainCharacter.GetComponent<CharController>().strongAttack();
        //StartCoroutine(Skill_1());
    }


    [SerializeField] int whichSkill = 0;
    public void SkillButton_2()
    {
        button2.interactable = false;
        
        //healthSkill();
        //RiseingWall();
        Push();
        //StartCoroutine(RandomTeleport());
        //StartCoroutine(speedPowerUp());
        //StartCoroutine(Hitable());

    }
    IEnumerator speedPowerUp()
    {
        StartCoroutine(skillDuration_2(10));
        mainCharacter.GetComponent<CharController>().speed = 250 + PlayerPrefs.GetFloat("speed") + 300;
        GameObject right = Instantiate(speedEffect, mainCharacter.transform.position, Quaternion.identity);
        right.transform.parent = mainCharacter.transform;
        yield return new WaitForSeconds(5);
        mainCharacter.GetComponent<CharController>().speed = 250 + PlayerPrefs.GetFloat("speed");
        Destroy(right);
    }

    IEnumerator Hitable()
    {
        StartCoroutine(skillDuration_2(10));
        GameObject hitableEffectObject = Instantiate(hitableEffect, mainCharacter.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        hitableEffectObject.transform.parent = mainCharacter.transform;
        CharController.canHittable = false;
        yield return new WaitForSeconds(4);
        CharController.canHittable = true;
        Destroy(hitableEffectObject);
    }

    IEnumerator MagnetSkill_Duration()
    {
        StartCoroutine(skillDuration_1(15));
        GameObject magnet_1 = Instantiate(magnetExplosion, mainCharacter.transform.position + new Vector3(7,1,0), Quaternion.identity);
        GameObject magnet_2 = Instantiate(magnetExplosion, mainCharacter.transform.position + new Vector3(-7,1,0), Quaternion.identity);
        GameObject magnet_3 = Instantiate(magnetExplosion, mainCharacter.transform.position + new Vector3(0,1,7), Quaternion.identity);
        GameObject magnet_4 = Instantiate(magnetExplosion, mainCharacter.transform.position + new Vector3(0,1,-7), Quaternion.identity);
        yield return new WaitForSeconds(8);
        Destroy(magnet_1);
        Destroy(magnet_2);
        Destroy(magnet_3);
        Destroy(magnet_4);
    }

    
    IEnumerator RandomTeleport()
    {
        StartCoroutine(skillDuration_2(5));
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 10f;
        Vector3 newSpawnArea = new Vector3(spawnDirection.x, 0, spawnDirection.y);
        Vector3 spawnPoint = transform.position + newSpawnArea;
        mainCharacter.transform.position = new Vector3(spawnPoint.x, 0.5f, spawnPoint.z);
        GameObject tpEffect = Instantiate(teleporteffect, mainCharacter.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Destroy(tpEffect);
    }

    public IEnumerator Skill_1()
    {
        StartCoroutine(skillDuration_2(10));
        for(int i = 0; i < 30; i++)
        {
            GameObject arrow = Instantiate(skill_1_item, mainCharacter.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
        }
    }
    public void healthSkill()
    {
        StartCoroutine(skillDuration_2(5));
        Instantiate(healthEffect, new Vector3(mainCharacter.transform.position.x, mainCharacter.transform.position.y + 1, mainCharacter.transform.position.z), mainCharacter.transform.rotation);
        CharController.healt = 5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20) + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel")));
    }

    public void Push()
    {
        StartCoroutine(skillDuration_2(5));
        Instantiate(pushSkill, mainCharacter.transform.position + new Vector3(0,1,0), Quaternion.identity);
    }

    public void RiseingWall()
    {
        StartCoroutine(skillDuration_2(5));
        Vector3 spawnPosition = mainCharacter.transform.position +(mainCharacter.transform.forward * 3);
        Instantiate(riseingWall, spawnPosition + new Vector3(0,-1.5f,0), mainCharacter.transform.rotation);
    }

    public void rasenShuriken()
    {
        StartCoroutine(skillDuration_1(5));
        GameObject rasenShuriken = Instantiate(rasenganEffect, mainCharacter.transform.position + new Vector3(0, 1, 0), mainCharacter.transform.rotation *  Quaternion.Euler(1,-1,1));
    }

    IEnumerator turnSwords()
    {
        StartCoroutine(skillDuration_1(13));
        GameObject skill = Instantiate(turningSwords, mainCharacter.transform.position, Quaternion.identity);
        skill.transform.parent = mainCharacter.transform;
        yield return new WaitForSeconds(5);
        Destroy(skill);

    }

    IEnumerator createFlameMeteor()
    {
        StartCoroutine(skillDuration_1(15));
        GameObject fireEffect = Instantiate(fireSkillEffect,mainCharacter.transform.position,Quaternion.identity);
        GameObject flames =  Instantiate(flameMeteor, mainCharacter.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(fireEffect);
        yield return new WaitForSeconds(13);
        Destroy(flames);
    }
}
