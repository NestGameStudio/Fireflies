using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPicker : MonoBehaviour
{
    
    public Button SkillOption1;
    public Button SkillOption2;
    public Button SkillOption3;

    public List<Upgrade> Upgrades;

    void Start()
    {
        PickSkill(SkillOption1);
        PickSkill(SkillOption2);
        PickSkill(SkillOption3);
    }

    
    void Update()
    {
        
    }

    private void PickSkill(Button SkillOption) {
        int common = Setup.Instance.PlayerValue.CommonWeight;
        int rare = Setup.Instance.PlayerValue.RareWeight;
        int epic = Setup.Instance.PlayerValue.EpicWeight;
        int legendary = Setup.Instance.PlayerValue.LegendaryWeight;
        int total = common + rare + epic + legendary;

        int rnd = Random.Range(0,total);
        // Lendario
        if(rnd < legendary) {
            ChooseUpgradeList(UpgradeRarity.Legendary,SkillOption);
        }
        // Epico
        if(rnd >= legendary && rnd < legendary + epic) {
            ChooseUpgradeList(UpgradeRarity.Epic,SkillOption);
        }
        // Raro
        if(rnd >= legendary + epic && rnd < legendary + epic + rare) {
            ChooseUpgradeList(UpgradeRarity.Rare,SkillOption);
        }
        // Comum
        if(rnd >= legendary + epic + rare && rnd < total) {
            ChooseUpgradeList(UpgradeRarity.Common,SkillOption);
        }
    }

    private void ChooseUpgradeList(UpgradeRarity ur, Button SkillOption) {
        List<Upgrade> upgrade = new List<Upgrade>();
        for(int i=0;i < Upgrades.Count; i++) {
            if(Upgrades[i].rarity == ur) {
                upgrade.Add(Upgrades[i]);
            }
        }
        int rnd = Random.Range(0,upgrade.Count);
        Upgrade skill = upgrade[rnd];

        for(int i=0;i<Upgrades.Count;i++) {
            if(Upgrades[i].name.Contains(skill.name) || skill.name.Contains(Upgrades[i].name)) {
                Upgrades.RemoveAt(i);
            }
        }


        SkillOption.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = skill.icon;
        SkillOption.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = skill.cost.ToString();
    }

}
