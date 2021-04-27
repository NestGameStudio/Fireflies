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

    private delegate void SkillFunction(float amount,int cost);

    private List<SkillFunction> ActiveSkill;

    uiShopAnimation uiShopAnimation;

    private void OnEnable() {
        if(!IsShop) {
            Upgrades = new List<Upgrade>();
            ActiveSkill = new List<SkillFunction>();
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
            PickSkill(TSkillOption1,0);
            PickSkill(TSkillOption2,1);
            PickSkill(TSkillOption3,2);
        }
        if(IsShop && !Acessed) {
            Upgrades = new List<Upgrade>();
            ActiveSkill = new List<SkillFunction>();
            for(int i=0;i<Setup.Instance.PlayerValue.upgrades.Count;i++) {
                Upgrades.Add(Setup.Instance.PlayerValue.upgrades[i]);
            }
            BackButton.SetActive(true);
            PickSkill(SkillOption1,0);
            PickSkill(SkillOption2,1);
            PickSkill(SkillOption3,2);
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
        uiShopAnimation = GetComponent<uiShopAnimation>();
    }

    private void PickSkill(Button SkillOption,int s) {
        int common = Setup.Instance.PlayerValue.CommonWeight;
        int rare = Setup.Instance.PlayerValue.RareWeight;
        int epic = Setup.Instance.PlayerValue.EpicWeight;
        int legendary = Setup.Instance.PlayerValue.LegendaryWeight;
        int total = common + rare + epic + legendary;

        int rnd = Random.Range(0,total);
        if(rnd < legendary) {
            ChooseUpgradeList(UpgradeRarity.Legendary,SkillOption,s);
        }
        if(rnd >= legendary && rnd < legendary + epic) {
            ChooseUpgradeList(UpgradeRarity.Epic,SkillOption,s);
        }
        if(rnd >= legendary + epic && rnd < legendary + epic + rare) {
            ChooseUpgradeList(UpgradeRarity.Rare,SkillOption,s);
        }
        if(rnd >= legendary + epic + rare && rnd < total) {
            ChooseUpgradeList(UpgradeRarity.Common,SkillOption,s);
        }
    }

    private void ChooseUpgradeList(UpgradeRarity ur, Button SkillOption,int s) {
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
        AddButtonFunction(skill.nome, skill, SkillOption,s);
        SkillOption.gameObject.GetComponent<SkillDescription>().AddDescription(skill.description);
    }

    private void AddButtonFunction(string name, Upgrade skill, Button SkillOption,int s) {
        PlayerValues p = Setup.Instance.PlayerValue;
        if(IsShop) {
            SkillOption.onClick.AddListener(delegate() {CheckCost(skill,SkillOption,s);});
        }
        switch(name) {
            case "Crit% Up":
                ActiveSkill.Add(p.IncreaseCritChance);
                Debug.Log("Add Critico");
                break;
            case "Damage Up":
                ActiveSkill.Add(p.IncreaseDamage);
                Debug.Log("Add Damage");
                break;
            case "Time to Breathe":
                ActiveSkill.Add(p.IncreaseInvicibility);
                Debug.Log("Add Invencivel");
                break;
            case "Bonus Jump":
                ActiveSkill.Add(p.IncreaseMaxJumpCharges);
                Debug.Log("Add pulo extra");
                break;
            case "Master Jumper":
                ActiveSkill.Add(p.IncreaseImpulseForce);
                Debug.Log("Add pulo forte");
                break;
            case "Vampirism Up":
                ActiveSkill.Add(p.IncreaseLifeSteal);
                Debug.Log("Add lifesteal");
                break;
            case "Health Up":
                ActiveSkill.Add(p.IncreaseMaxHealth);
                Debug.Log("Add Vida");
                break;
            default:
                Debug.Log("Deu ruim no nome");
                break;
        }
        if(!IsShop) {
            SkillOption.onClick.AddListener(delegate() {DontCheckCost(skill,s,SkillOption);});
        }
        
    }

    private void CheckCost(Upgrade skill, Button SkillOption,int s) {
        if(MoneyManager.instance.money < skill.cost) {
            //HUDManager.instance.moneyUI.LowMoney();
            uiShopAnimation.noMoney(SkillOption.gameObject);
            return;
        }
        else {
            uiShopAnimation.skillSelect(SkillOption.gameObject);
        }
        ActiveSkill[s](skill.effects[0].amount,skill.cost);
        MoneyManager.instance.perderDinheiro(skill.cost);
        SkillOption.interactable = false;
    }

    private void DontCheckCost(Upgrade skill,int s,Button SkillOption) {
        uiShopAnimation.skillSelect(SkillOption.gameObject);
        ActiveSkill[s](skill.effects[0].amount,skill.cost);
        gameObject.SetActive(false);
        HealthManager.instance.Player.GetComponent<CollisionCheck>().DesligaFog();
        HealthManager.instance.UnFreezePlayer();
    }

}
