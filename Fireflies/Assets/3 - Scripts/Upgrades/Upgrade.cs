using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType {
    JumpCharge,
    JumpForce,
    MaxHealth,
    Damage
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject {

    public string name;
    public string description;

    public Sprite icon;

    public class UpgradeStats{
        public UpgradeType type;
        public float amount; 

        public UpgradeStats(UpgradeType upgradeType, float upgradeAmount){
            type = upgradeType;
            amount = upgradeAmount;
        }
    }

    [SerializeField]
    public List<UpgradeStats> stats = new List<UpgradeStats>();
}
