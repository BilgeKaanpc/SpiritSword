using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class swordsPanelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText, damageText, levelText, elementText, nameText;
    [SerializeField] List<SwordsDetails> swordsList = new List<SwordsDetails>();
    [SerializeField] GameObject swordArea;
    int skillCount;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        skillCount = swordsList.Count;
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
    private GameObject previous;
    public void CreateSword(int index)
    {
        if(previous != null)
        {
            Destroy(previous);
        }

        GameObject sword = Instantiate(swordsList[index].Sword, transform.position, Quaternion.identity);
        if (index >= 49)
        {
            sword.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            sword.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        sword.transform.position = new Vector3(-22f, 0.8f, -1.37f);
        previous = sword;
    }
    public void fixUI(int index)
    {
        nameText.text = swordsList[index].Name;
        descriptionText.text = swordsList[index].Description;
        damageText.text = swordsList[index].Damage.ToString();
        levelText.text = swordsList[index].Level.ToString();
        elementText.text = swordsList[index].Element;
        CreateSword(index);
    }

}
[System.Serializable]
public class SwordsDetails
{
    public string _name;
    public string _description;
    public int _damage;
    public int _level;
    public string _element;
    public GameObject _sword;

    public SwordsDetails(string name, string description, int damage, int level, string element,GameObject sword)
    {
        _sword = sword;
        _name = name;
        _description = description;
        _damage = damage;
        _level = level;
        _element = element;
    }

    public GameObject Sword
    {
        get { return _sword; }
        set { _sword = value; }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public string Element
    {
        get { return _element; }
        set { _element = value; }
    }
}