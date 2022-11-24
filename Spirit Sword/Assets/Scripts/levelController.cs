using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelController : MonoBehaviour
{
    public List<float> xp;
    public float nowXp;
    public TextMeshProUGUI xpText;
    public Image xpBar;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        xp = new List<float>()
        {
            100,200,400,800,1600
        };
        nowXp = PlayerPrefs.GetFloat("XP");
        xpText.text = PlayerPrefs.GetFloat("XP") + "/" + xp[PlayerPrefs.GetInt("Level")-1];
        xpBar.fillAmount = PlayerPrefs.GetFloat("XP") / xp[PlayerPrefs.GetInt("Level")-1];
        
    }

   
}
