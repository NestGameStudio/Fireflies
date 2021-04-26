using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPicker : MonoBehaviour
{
    public static bool IsShop = false;
    public GameObject BackButton;
    public Button SkillOption1;
    public Button SkillOption2;
    public Button SkillOption3;

    public Button TSkillOption1;
    public Button TSkillOption2;
    public Button TSkillOption3;
    private bool Acessed = false;

    private List<Upgrade> Upgrades;

    private void OnEnable() {
        if(!IsShop) {
            Upgrades = new List<Upgrade>();
            for(int i=0;i<Setup.Instance.PlayerValue.upgrades.Count;i++) {
                Upgrades.Add(Setup.Instance.PlayerValue.upgrades[i]);
            }
            BackButton.SetActive(false);
            SkillOption1.gameObject.SetActive(false);
            SkillOption2.gameObject.SetActive(false);
            SkillOption3.gameObject.SetActive(false);
            TSkillOption1.gameObject.SetActive(true);
            TSkillOption2.gameObject.SetActive(true);
            TSkillOption3.gameObject.SetActive(true);
            PickSkill(TSkillOption1);
            PickSkill(TSkillOption2);
            PickSkill(TSkillOption3);
        }
        if(IsShop && !Acessed) {
            Upgrades = new List<Upgrade>();
            for(int i=0;i<Setup.Instance.PlayerValue.upgrades.Count;i++) {
                Upgrades.Add(Setup.Instance.PlayerValue.upgrades[i]);
            }
            BackButton.SetActive(true);
            PickSkill(SkillOption1);
            PickSkill(SkillOption2);
            PickSkill(SkillOption3);
            Acessed = true;
        }
        if(IsShop) {
            BackButton.SetActive(true);
            TSkillOption1.gameObject.SetActive(false);
            TSkillOption2.gameObject.SetActive(false);
            TSkillOption3.gameObject.SetActive(false);
            SkillOption1.gameObject.SetActive(true);
            SkillOption2.gameObject.SetActive(true);
            SkillOption3.gameObject.SetActive(true);
        }
    }

    void Start()
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

        for(int i=0;i<Upgrades.Count;i++) {
            if(Upgrades[i].nome == skill.nome) {
                Upgrades.RemoveAt(i);
                i = 0;
            }
        }
        

        SkillOption.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = skill.icon;
        if(IsShop) {
            SkillOption.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = skill.cost.ToString();
        }
        else {
            SkillOption.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        AddButtonFunction(skill.nome, skill, SkillOption);
        SkillOption.gameObject.GetComponent<SkillDescription>().AddDescription(skill.description);
    }

    private void AddButtonFunction(string name, Upgrade skill, Button SkillOption) {
        PlayerValues p = Setup.Instance.PlayerValue;
        if(IsShop) {
            SkillOption.onClick.AddListener(delegate() {CheckCost(skill,SkillOption);});
        }
        switch(name) {
            case "Crit% Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseCritChance(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add Critico");
                break;
            case "Damage Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseDamage(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add Damage");
                break;
            case "Time to Breathe":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseInvicibility(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add Invencivel");
                break;
            case "Bonus Jump":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseMaxJumpCharges(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add pulo extra");
                break;
            case "Master Jumper":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseImpulseForce(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add pulo forte");
                break;
            case "Vampirism Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseLifeSteal(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add lifesteal");
                break;
            case "Health Up":
                SkillOption.onClick.AddListener(delegate() {p.IncreaseMaxHealth(skill.effects[0].amount,skill.cost);});
                Debug.Log("Add Vida");
                break;
            default:
                Debug.Log("Deu ruim no nome");
                break;
        }
        if(!IsShop) {
            SkillOption.onClick.AddListener(delegate() {DontCheckCost();});
        }
        
    }

    private void CheckCost(Upgrade skill, Button SkillOption) {
        if(MoneyManager.instance.money < skill.cost) {
            HUDManager.instance.moneyUI.LowMoney();
            return;
        }
        SkillOption.interactable = false;
    }

    private void DontCheckCost() {
        gameObject.SetActive(false);
        HealthManager.instance.Player.GetComponent<CollisionCheck>().DesligaFog();
        HealthManager.instance.UnFreezePlayer();
    }

}
