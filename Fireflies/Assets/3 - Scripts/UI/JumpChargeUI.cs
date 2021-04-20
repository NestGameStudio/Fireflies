using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RadialLayoutGroup))]
public class JumpChargeUI : MonoBehaviour
{
    private RadialLayoutGroup radialLayout;
    public GameObject chargeObj;

    [HideInInspector]
    public List<GameObject> chargeList;

    private int chargeCount = 0;

    [Header("Debug")]
    public float offsetPadding = 10f;

    void Awake(){
        radialLayout = GetComponent<RadialLayoutGroup>();
    }

    public void Setup(int count){
        chargeCount = count;
        chargeList.Clear();

        for(int i = 0; i < count; i++){
            AddCharge();
        }

        UpdateCharges(0);
    }

    //Instancia novo filho de carga no Radial Layout Group
    public void AddCharge(){
        GameObject newCharge = Instantiate(chargeObj, transform.position, Quaternion.identity, transform);
        chargeList.Add(newCharge);

        chargeCount = chargeList.Count;

        AdjustUI();
    }

    public void RemoveCharge(){
        chargeList.RemoveAt(chargeList.Count-1);

        chargeCount = chargeList.Count;

        AdjustUI();
    }

    public void UpdateCharges(int currentCharges){
        for(int i = 0; i < chargeCount; i++){
            if(i < currentCharges){
                chargeList[i].GetComponent<Image>().color = Color.white;
            } else {
                chargeList[i].GetComponent<Image>().color = Color.gray;
            }
            
        }
    }

    //Ajeita centralização da UI baseado em chargeCount;
    private void AdjustUI(){
        float offset = (chargeCount-1)*offsetPadding;

        radialLayout.StartAngle = 270 - offset;
        radialLayout.MinAngle = 180 - offset;
        radialLayout.MaxAngle = 180 + offset;
    }

}
