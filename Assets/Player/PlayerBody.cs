using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] GAME GameManager;
    public float maxhp = 150;
    public float currenthp;
    float timer = 0;
    float damage_cooldown = 2.0f;
    bool hitTaken = false;


    //  im making the shield an invicibility thing for now. no damage allowed when shielding.
    private PlayerAbilities shield;



    [SerializeField] UpdateUI HUD; //Reference to the HUD

    private void Start(){
        HUD.SetMaxHP(maxhp);

        shield = GetComponent<PlayerAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        //When the player dies
        if (currenthp <= 0) {
            currenthp = 0;
            GameManager.ChangeScene("Result");
            Debug.Log("Ow lmao you are dead af");
            
        }

        //If the player took a hit, activate invincibility frames
        if (hitTaken)
        {

            timer += Time.deltaTime;

            if (timer >= damage_cooldown)
            {
                hitTaken = false;
                timer = 0.0f;

            }

        }

        HUD.SetHP(currenthp);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!shield.activeShield){
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (!hitTaken)
                {
                    hitTaken = true;
                    Enemy enemyStuff = collision.gameObject.GetComponent<Enemy>();
                    currenthp -= enemyStuff.power;
                    HUD.SetHP(currenthp);
                }
            }
        }
    }

}
