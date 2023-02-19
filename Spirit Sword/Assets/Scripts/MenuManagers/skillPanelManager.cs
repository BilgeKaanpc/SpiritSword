using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class skillPanelManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI description, damage, time, tier, nameText;
    [SerializeField] Image skillImage;


    [SerializeField] List<UltimateSkill> ultiList = new List<UltimateSkill>();
    int skillCount;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        skillCount = ultiList.Count;
        fixUI(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goLeft()
    {
        if(index == 0)
        {
            index = skillCount - 1;
            fixUI(index);
        }
        else
        {
            index--;
            fixUI(index);
        }
    }
    public void goRight()
    {
        if(index+1 >= skillCount)
        {
            index = 0;
            fixUI(index);
        }
        else
        {
            index++;
            fixUI(index);
        }
    }
    public void ChooseSkill()
    {

    }
    
    public void fixUI(int index)
    {
        nameText.text = ultiList[index].Name;
        description.text = ultiList[index].Description;
        damage.text = ultiList[index].Damage.ToString();
        time.text = ultiList[index].Time.ToString();
        tier.text = ultiList[index].Tier;
        skillImage.sprite = ultiList[index].SkillImage;
    }


}

[System.Serializable]
public class UltimateSkill
{
    public string _name;
    public int _damage;
    public int _time;
    public string _tier;
    public string _description;
    public Sprite _skillImage;
    public UltimateSkill(string name,int damage, int time, string tier, string description,Sprite skillImage)
    {
        this.Name = name;
        this.Damage = damage;
        this.Time = time;
        this.Tier = tier;
        this.Description = description;
        this.SkillImage = skillImage;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public Sprite SkillImage
    {
        get { return _skillImage; }
        set { _skillImage = value; }
    }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int Time
    {
        get { return _time; }
        set { _time = value; }
    }

    public string Tier
    {
        get { return _tier; }
        set { _tier = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}
