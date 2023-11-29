using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour{
    [SerializeField] Slider HPBar;
    public float maxHP = 150.0f; //Default value


    //Hp related methods
    public void SetHP(float amount) { HPBar.value = amount / maxHP; }
    public void SetMaxHP(float amount) { maxHP = amount; }

}
