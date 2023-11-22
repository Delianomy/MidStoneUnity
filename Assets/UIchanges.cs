using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIchanges : MonoBehaviour
{
    public Slider HPslider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetMaxHP(float health) { 
        
        HPslider.value = health;
        HPslider.maxValue = health;
    }

    public void SetHP(float health)
    {
        HPslider.value = health;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
