using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour{

    public AudioSource slash;
    public AudioSource shoot;
    public AudioSource shield;
    enum playerAbilities { Melee, Shoot, Shield }
    [Header("Player Abilities")]
    [SerializeField] playerAbilities currentAbility;

    [Header("Melee variables")]
    [SerializeField] float meleePower = 10.0f;
    [SerializeField] float meleeDistance = 2.0f;
    [SerializeField] float meleeRadius = 3.0f;

    [Header("Projectile variables")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectilePower;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDuration;


//  i literally dont know what i'm doing but we gaming?
// I got you king - Adriel
    [Header("Shield variables")]
    [SerializeField] GameObject shieldObject;
    [SerializeField] private float shieldDuration = 5.0f;
    [SerializeField] private float shieldCooldown = 10.0f;

    [HideInInspector] public bool activeShield = false;
    private bool cooldown = false;

    private float shieldTimer = 0.0f;
    private float shieldCooldownTimer = 0.0f;

    [Header("Debugging")]
    [SerializeField] Vector2 mouseDir; //Prints the mouse dir on the inspector

    private void OnDrawGizmos(){
        DrawCircle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + mouseDir * meleeDistance, meleeRadius);
    }

    private void Start()
    {
        shieldObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        mouseDir = GetMouseDirection();
        switch (currentAbility){

             //Melee abilities
             case playerAbilities.Melee:
                 Melee();
                 break;
                 
             //Shooting abilities
             case playerAbilities.Shoot:
                 Shoot();
                 break;
            
            //Shield abilities
             case playerAbilities.Shield:
                 Shield();
                 break;
        }
    }

    //Function to get the direction of the mouse relative to the player
    Vector2 GetMouseDirection(){
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 playerPos = gameObject.transform.position;
        Vector2 mouseDirection = mouseWorldPosition - playerPos;
        return mouseDirection.normalized;
    }

    //Chat GPT code
    //Draws a circle 
    void DrawCircle(Vector3 center, float radius)
    {
        Gizmos.color = Color.green;

        int segments = 36; // You can adjust the number of segments for a smoother circle
        float angleIncrement = 360f / segments;

        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleIncrement);
            Vector3 point = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, point);
            }

            prevPoint = point;
        }
    }

    void Melee() {
        if (Input.GetMouseButtonDown(0)){
            
            Vector2 playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            RaycastHit2D[] hitObjects = Physics2D.CircleCastAll(playerPos + mouseDir * meleeDistance, meleeRadius, Vector2.zero);
            foreach (var Object in hitObjects)
            {
                if (Object.collider.gameObject.tag == "Enemy")
                {
                    slash.Play();
                    Debug.Log("Ouch");
                    Enemy enemyProperties = Object.collider.gameObject.GetComponent<Enemy>();
                    if (enemyProperties == null)
                    {
                        Debug.Log("Broke something");
                        break;
                    }
                    enemyProperties.TakeDamage(meleePower);
                }
            }
        }
    }
    void Shoot() {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            GameObject spawnedProjectile = Instantiate(projectile, playerPos + mouseDir * 2, Quaternion.identity);
            Projectile spawnedProperties = spawnedProjectile.GetComponent<Projectile>();
            if (spawnedProperties == null)
            {
                
                Debug.Log("Broke something");
                return;
            }

            shoot.Play();
            spawnedProperties.currentGroup = Projectile.group.Player;
            spawnedProperties.direction = mouseDir;
            spawnedProperties.speed = projectileSpeed;
            spawnedProperties.duration = projectileDuration;
            spawnedProperties.power = projectilePower;
        }
    }
    void Shield() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !cooldown)
        {
            if (!activeShield)
            {
                shield.Play();
                shieldObject.SetActive(true);
                activeShield = true;
                shieldTimer = 0;
            }
            else
            {
                shieldObject.SetActive(false);
                activeShield = false;
                shieldTimer = 0;
                cooldown = true;
            }
        }

        if (activeShield)
        {
            shieldTimer += Time.deltaTime;
            Debug.Log("shield active, (active for " + Mathf.Round((shieldDuration - shieldTimer)) + ")");

            if (shieldTimer >= shieldDuration)
            {
                shieldObject.SetActive(false);
                activeShield = false;
                shieldTimer = 0.0f;
                cooldown = true;
            }
        }

        if (cooldown)
        {
            shieldCooldownTimer += Time.deltaTime;
            Debug.Log("On cooldown, (active for " + Mathf.Round((shieldCooldown - shieldCooldownTimer)) + ")");

            if (shieldCooldownTimer >= shieldCooldown)
            {
                cooldown = false;
                shieldCooldownTimer = 0.0f;
            }
        }
    }
}
