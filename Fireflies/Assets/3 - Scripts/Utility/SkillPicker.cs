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
        if(rnd < legendary) {
            ChooseUpgradeList(UpgradeRarity.Legendary,SkillOption);
        }
        if(rnd >= legendary && rnd < legendary + epic) {
            ChooseUpgradeList(UpgradeRarity.Epic,SkillOption);
        }
        if(rnd >= legendary + epic && rnd < legendary + epic + rare) {
            ChooseUpgradeList(UpgradeRarity.Rare,SkillOption);
        }
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
        Debug.Log("Escolhido: " + skill.nome + " " + skill.rarity);


        for(int i=0;i<Upgrades.Count;i++) {
            if(Upgrades[i].nome == skill.nome) {
                Debug.Log("Removido: " + Upgrades[i].nome + " " + Upgrades[i].rarity);
                Upgrades.RemoveAt(i);
                i = 0;
            }
        }
        

        SkillOption.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = skill.icon;
        SkillOption.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = skill.cost.ToString();
        AddButtonFunction(skill.nome, skill, SkillOption);
        SkillOption.gameObject.GetComponent<SkillDescription>().AddDescription(skill.description);
    }

    private void AddButtonFunction(string name, Upgrade skill, Button SkillOption) {
        PlayerValues p = Setup.Instance.PlayerValue;

        SkillOption.onClick.AddListener(delegate() {CheckCost(skill,SkillOption);});
        switch(name) {
            case "Wombo Combo":
                SkillOption.onClick.AddListener(delegate() {});
                break;
            case "Crit% Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseCritChance(skill.effects[0].amount,skill.cost);});
                break;
            case "Damage Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseDamage(skill.effects[0].amount,skill.cost);});
                break;
            case "Hold Limit":
                SkillOption.onClick.AddListener(delegate() {});
                break;
            case "Time to Breathe":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseInvicibility(skill.effects[0].amount,skill.cost);});
                break;
            case "Bonus Jump":
                SkillOption.onClick.AddListener(delegate() {});
                break;
            case "Master Jumper":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseImpulseForce(skill.effects[0].amount,skill.cost);});
                break;
            case "Vampirism Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseLifeSteal(skill.effects[0].amount,skill.cost);});
                break;
            case "Health Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseMaxHealth(skill.effects[0].amount,skill.cost);});
                break;
            case "Perfect Timing":
                SkillOption.onClick.AddListener(delegate() {});
                break;
        }
        
    }

    private void CheckCost(Upgrade skill, Button SkillOption) {
        if(MoneyManager.instance.money < skill.cost) {
                HUDManager.instance.moneyUI.LowMoney();
            return;
        }
        SkillOption.interactable = false;
    }

}
