using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelController : MonoBehaviour
{
    public float nowXp;
    public TextMeshProUGUI xpText;
    public Image xpBar;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        nowXp = PlayerPrefs.GetFloat("XP");
        xpText.text = PlayerPrefs.GetFloat("XP") + "/" + (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));
        xpBar.fillAmount = PlayerPrefs.GetFloat("XP") / (50 * (Mathf.Pow(PlayerPrefs.GetInt("Level"), 2) - PlayerPrefs.GetInt("Level") + 2));

    }

   
}
