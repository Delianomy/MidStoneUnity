using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public float maxhp = 150;
    public float currenthp;
    float timer = 0;
    float damage_cooldown = 2.0f;
    bool hitTaken = false;
    // Start is called before the first frame update

    public UIchanges healthbar;

    private void Start()
    {
        currenthp = maxhp;
        healthbar.SetMaxHP(maxhp);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!hitTaken)
            {
                hitTaken = true;
                currenthp -= 20.0f;
                healthbar.SetMaxHP(currenthp);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currenthp <= 0) {
            currenthp = 0;
            Debug.Log("Ow lmao you are dead af");
            
        }
        if (hitTaken)
        {

            timer += Time.deltaTime;

            if (timer >= damage_cooldown)
            {
                hitTaken = false;
                timer = 0.0f;

            }

        }
        Debug.Log(currenthp);
    }
}