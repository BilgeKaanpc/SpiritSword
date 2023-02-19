using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class littleSkillPanelManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI description, time, tier, nameText;
    [SerializeField] Image littleSkillImage;

    [SerializeField] List<LittleSkill> LittleSkillList = new List<LittleSkill>();
    int skillCount;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {

        skillCount = LittleSkillList.Count;
        fixUI(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goLeft()
    {
        if (index == 0)
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
        if (index + 1 >= skillCount)
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
    public void fixUI(int index)
    {
        nameText.text = LittleSkillList[index].Name;
        description.text = LittleSkillList[index].Description;
        time.text = LittleSkillList[index].Time.ToString();
        tier.text = LittleSkillList[index].Tier;
        littleSkillImage.sprite = LittleSkillList[index].SkillImage;
    }

}

[System.Serializable]
public class LittleSkill
{
    public string _name;
    public int _time;
    public string _tier;
    public string _description;
    public Sprite _skillImage;
    public LittleSkill(string name, int time, string tier, string description,Sprite skillImage)
    {
        this.Name = name;
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