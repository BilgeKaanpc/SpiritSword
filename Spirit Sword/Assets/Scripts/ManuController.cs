using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ManuController : MonoBehaviour
{
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject equipmentPanel;
    [SerializeField] GameObject skillsPanel;
    [SerializeField] GameObject mapsPanel;
    [SerializeField] GameObject cam;
    int position = 0;

    public List<float> xp;

    [SerializeField] public List<float> swordsPower;

    //Texts
    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI totalHealtText;
    [SerializeField] TextMeshProUGUI totalDamageText;
    [SerializeField] TextMeshProUGUI regenText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI skillPointsText;
    [SerializeField] TextMeshProUGUI totalHealtSourceText;
    [SerializeField] TextMeshProUGUI totalDamageourceText;
    [SerializeField] TextMeshProUGUI regenourceText;
    [SerializeField] TextMeshProUGUI speedsourceText;


    //Bars
    [SerializeField] Image xpBar;
    [SerializeField] Image healthBar;
    [SerializeField] Image damageBar;
    [SerializeField] Image regenBar;
    [SerializeField] Image speedBar;


    //Upgrade Buttons

    [SerializeField] Button healthButton;
    [SerializeField] Button damageButton;
    [SerializeField] Button regenButton;
    [SerializeField] Button speedButton;

    //Buttons
    [SerializeField] Button swordsPanelButton;
    [SerializeField] Button skillPanelButton;
    [SerializeField] Button littleSkillButton;

    //Panels
    [SerializeField] GameObject swordsPanelUI;
    [SerializeField] GameObject skillPanelUI;
    [SerializeField] GameObject littleSkillPanelUI;

    //max Values
    float maxSpeedLvl = 100;
    float maxRegenLvl = 13f;
    float maxHealthLvl = 37;
    float maxDamageLvl = 30;

    // Start is called before the first frame update
    void Start()
    {
        swordsPower = new List<float>()
        {
            10,
            13,
            17,
            22,
            28, 
            35,
            43,
            52,
            62,
            73,
            85,
            98,
            112,
            127,
            143,
            160,
            178,
            197,
            217,
            238,
            260,
            283,
            307,
            332,
            358,
            385,
            413,
            442,
            472,    
            503,
            535,
            568,
            602,
            637,
            673,
            710,
            748,
            787,
            827,
            868,
            910,
            953,
            997,
            1042,
            1088,
            1135,
            1183,
            1232,
            1282,
            1500,
        };

        xp = new List<float>()
        {
            100,200,400,800,1600
        };

       
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (!PlayerPrefs.HasKey("MaxHealt"))
        {
            PlayerPrefs.SetFloat("MaxHealt", 1);
        }
        if (!PlayerPrefs.HasKey("XP"))
        {
            PlayerPrefs.SetFloat("XP", 0);
        }
        if (!PlayerPrefs.HasKey("bonusHealthLevel"))
        {
            PlayerPrefs.SetFloat("bonusHealthLevel", 1);
        }
        if (!PlayerPrefs.HasKey("speed"))
        {
            PlayerPrefs.SetFloat("speed", 0);   
        }
        if (!PlayerPrefs.HasKey("regen"))
        {
            PlayerPrefs.SetFloat("regen", 0f);
        }
        if (!PlayerPrefs.HasKey("bonusDamageLevel"))
        {
            PlayerPrefs.SetFloat("bonusDamageLevel", 1);
        }
        if (!PlayerPrefs.HasKey("skillPoints"))
        {
            PlayerPrefs.SetInt("skillPoints", 100);
        }



        UpgradeAllSkill();


    }

    // 5 * (Mathf.Pow(PlayerPrefs.GetFloat("MaxHealt"),2) - PlayerPrefs.GetFloat("MaxHealt") + 20) 
    // (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2)


    private void Update()
    {

    }
    public void UpgradeAllSkill()
    {

        string square = (Mathf.Pow((-1), PlayerPrefs.GetInt("Level") + 1)).ToString();
        int index = (((2 * PlayerPrefs.GetInt("Level")) + int.Parse(square) + 1)) / 4;
        lvlText.text = PlayerPrefs.GetInt("Level").ToString();
        expText.text = PlayerPrefs.GetFloat("XP") + "/" + (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));
        totalHealtText.text = (5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20) + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel")))).ToString();
        totalDamageText.text = (swordsPower[index - 1] + (PlayerPrefs.GetFloat("bonusDamageLevel") - 1) * (PlayerPrefs.GetFloat("bonusDamageLevel"))).ToString();
        regenText.text = (1.5f - (PlayerPrefs.GetFloat("regen")/10)).ToString();
        speedText.text = (250 + PlayerPrefs.GetFloat("speed")).ToString();
        skillPointsText.text = PlayerPrefs.GetInt("skillPoints").ToString();
        totalHealtSourceText.text = "(" + 5 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 20) + "/" + (5 / 2) * ((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) * (PlayerPrefs.GetFloat("bonusHealthLevel"))) + ")";
        totalDamageourceText.text = swordsPower[index - 1] + "/" + (PlayerPrefs.GetFloat("bonusDamageLevel") - 1) * (PlayerPrefs.GetFloat("bonusDamageLevel"))+")";
        regenourceText.text = "1.5/sn - " + (PlayerPrefs.GetFloat("regen")/10) + "/sn";
        speedsourceText.text = "(250 + " + PlayerPrefs.GetFloat("speed") + ")";

        if (PlayerPrefs.GetFloat("bonusHealthLevel")-1 != 0)
        {
            healthBar.fillAmount = (PlayerPrefs.GetFloat("bonusHealthLevel")-1) / maxHealthLvl;
        }
        if (PlayerPrefs.GetFloat("speed") != 0)
        {
            speedBar.fillAmount = PlayerPrefs.GetFloat("speed") / maxSpeedLvl ;
        }
        if (PlayerPrefs.GetFloat("regen") != 0)
        {
            regenBar.fillAmount = PlayerPrefs.GetFloat("regen") / maxRegenLvl;
        }
        if (PlayerPrefs.GetFloat("bonusDamageLevel") != 0)
        {
            damageBar.fillAmount = (PlayerPrefs.GetFloat("bonusDamageLevel")-1) / maxDamageLvl  ;
        }
        if (PlayerPrefs.GetFloat("XP") != 0)
        {
            xpBar.fillAmount = PlayerPrefs.GetFloat("XP") / (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));
        }

        if (PlayerPrefs.GetInt("skillPoints") ==0)
        {
            ButtonActive(false);
        }
        else
        {
            ButtonActive(true);
        }

        if (maxHealthLvl == PlayerPrefs.GetFloat("bonusHealthLevel")-1)
        {
            healthButton.interactable = false;
        }
        if (maxDamageLvl == PlayerPrefs.GetFloat("bonusDamageLevel")-1)
        {
            damageButton.interactable = false;
        }
        if (maxRegenLvl == PlayerPrefs.GetFloat("regen"))
        {
            regenButton.interactable = false;
        }
        if (maxSpeedLvl == PlayerPrefs.GetFloat("speed"))
        {
            speedButton.interactable = false;
        }

    }

    void ButtonActive(bool activeOr)
    {
        healthButton.interactable = activeOr;
        damageButton.interactable = activeOr;
        regenButton.interactable = activeOr;
        speedButton.interactable = activeOr;

    }

    public void UpgradeHealth()
    {
        PlayerPrefs.SetFloat("bonusHealthLevel", PlayerPrefs.GetFloat("bonusHealthLevel") + 1);
        PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") - 1);
        Debug.Log((PlayerPrefs.GetFloat("bonusHealthLevel") - 1) / maxHealthLvl);
        UpgradeAllSkill();
    }
    public void UpgradeDamage()
    {
        PlayerPrefs.SetFloat("bonusDamageLevel", PlayerPrefs.GetFloat("bonusDamageLevel") + 1);
        PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") - 1);
        UpgradeAllSkill();
    }
    public void UpgradeRegen()
    {
        PlayerPrefs.SetFloat("regen", PlayerPrefs.GetFloat("regen") + 1f);
        PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") - 1);
        UpgradeAllSkill();
    }
    public void UpgradeSpeed()
    {
        PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed") + 10);
        PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") - 1);
        UpgradeAllSkill();
    }


    public void SwordsPanel()
    {
        swordsPanelButton.interactable = false;
        skillPanelButton.interactable = true;
        littleSkillButton.interactable = true;

        swordsPanelUI.SetActive(true);
        skillPanelUI.SetActive(false);
        littleSkillPanelUI.SetActive(false);
    }
    public void SkillPanel()
    {

        swordsPanelButton.interactable = true;
        skillPanelButton.interactable = false;
        littleSkillButton.interactable = true;

        swordsPanelUI.SetActive(false);
        skillPanelUI.SetActive(true);
        littleSkillPanelUI.SetActive(false);
    }
    public void LittleSkillPanel()
    {

        swordsPanelButton.interactable = true;
        skillPanelButton.interactable = true;
        littleSkillButton.interactable = false;

        swordsPanelUI.SetActive(false);
        skillPanelUI.SetActive(false);
        littleSkillPanelUI.SetActive(true);
    }


    public void goHome()
    {
        StartCoroutine(Main());
    }
    public void goEquipmans()
    {
        StartCoroutine(Equipmans());
    }
    public void goSkills()
    {
        StartCoroutine(Skills());
    }
    public void goMap()
    {
       StartCoroutine(Map());
    }
    public void loadMap_1()
    {

        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
    //Camera Positions
    // main 0.07 - 1.8 - 6.12 / 9.7 - 28.5 - 0
    // Equipmans 2 - 1.7 - 0.3 / 14 - 135 - 0
    // Settings 0.1 - 1.5 - 1.6 / 2 - 180 - 0
    // Map -1.7 - 3 - 0 / 50 - 270 - 0
    private void FixedUpdate()
    {
        switch (position)
        {
            case 1:

                camMove(new Vector3(1.8f, 1.7f, 0.3f) , Quaternion.Euler(14,135,0));
                break;
            case 2:
                camMove(new Vector3(0.1f, 1.5f, 1.6f), Quaternion.Euler(2, 180, 0));
                break;
            case 3:
                camMove(new Vector3(-1.7f, 3f, 0f), Quaternion.Euler(50, 270, 0));
                break;
            default:
                camMove(new Vector3(0.07f,1.8f,6.12f), Quaternion.Euler(9.7f, 28.5f, 0));
                break;
        }
    }
    public void camMove(Vector3 target,Quaternion rotationTarget) {
        cam.transform.position = Vector3.Lerp(cam.transform.position, target, Time.deltaTime * 10);

        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, rotationTarget, Time.deltaTime * 100);
    }
    IEnumerator Main()
    {
        position = 0;
        equipmentPanel.SetActive(false);
        skillsPanel.SetActive(false);
        mapsPanel.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        homePanel.SetActive(true);
    }
    IEnumerator Equipmans()
    {
        position = 1;
        homePanel.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        equipmentPanel.SetActive(true);
    }
    IEnumerator Skills()
    {
        position = 2;
        homePanel.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        skillsPanel.SetActive(true);
    }
    IEnumerator Map()
    {
        position = 3;
        homePanel.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        mapsPanel.SetActive(true);
    }
}
