using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType {
    JumpCharge,
    JumpForce,
    MaxHealth,
    Damage,
    HoldLimit,
    Invincibility,
    CritChance,
    LifeSteal,
    Combo,
    PerfectTiming


}

public enum UpgradeRarity {
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public class UpgradeEffect{
    [Tooltip("Attribute to which the upgrade will be applied")]
    public UpgradeType type;

    [Tooltip("Value that will be added to the selected attribute")]
    public float amount; 
    public UpgradeEffect(UpgradeType upgradeType, float upgradeAmount){
        type = upgradeType;
        amount = upgradeAmount;
    }
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class Upgrade : ScriptableObject {

    [Tooltip("Upgrade name")]
    public string nome;

    [Tooltip("Upgrade description that will be shown to the player")]
    [TextArea(3,10)]
    public string description;

    [Tooltip("Upgrade icon")]
    public Sprite icon;

    [Tooltip("Upgrade cost, shown when shopping")]
    public int cost;

    [Tooltip("Upgrade rarity, affects chance of appearance")]
    public UpgradeRarity rarity = UpgradeRarity.Common;

    [Header("Upgrade effects")]
    [Tooltip("List of upgrade effects. Effects will be applied following element order")]
    [SerializeField]
    public List<UpgradeEffect> effects;
}
