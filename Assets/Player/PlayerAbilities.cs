using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour{

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

    [Header("Debugging")]
    [SerializeField] Vector2 mouseDir;
    // Start is called before the first frame update
    private void OnDrawGizmos(){
        DrawCircle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + mouseDir * meleeDistance, meleeRadius);
    }

    // Update is called once per frame
    void Update(){
        mouseDir = GetMouseDirection();
        if (Input.GetMouseButtonDown(0)){
            Vector2 playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            switch (currentAbility){
                case playerAbilities.Melee:
                    {
                        RaycastHit2D[] hitObjects = Physics2D.CircleCastAll(playerPos + mouseDir * meleeDistance, meleeRadius, Vector2.zero);
                        foreach (var Object in hitObjects)
                        {
                            if (Object.collider.gameObject.tag == "Enemy")
                            {
                                Enemy enemyProperties = Object.collider.gameObject.GetComponent<Enemy>();
                                if (enemyProperties == null)
                                {
                                    Debug.Log("Broke something");
                                    break;
                                }
                                enemyProperties.TakeDamage(meleePower);
                            }
                        }
                        break;
                    }
                
                case playerAbilities.Shoot:
                    {
                        GameObject spawnedProjectile = Instantiate(projectile, playerPos + mouseDir * 2, Quaternion.identity);
                        Projectile spawnedProperties = spawnedProjectile.GetComponent<Projectile>();
                        if (spawnedProperties == null)
                        {
                            Debug.Log("Broke something");
                            break;
                        }

                        spawnedProperties.currentGroup = Projectile.group.Player;
                        spawnedProperties.direction = mouseDir;
                        spawnedProperties.speed = projectileSpeed;
                        spawnedProperties.duration = projectileDuration;
                        spawnedProperties.power = projectilePower;
                        break;
                    }

                case playerAbilities.Shield: 
                    break;
            }
        }
    }

    Vector2 GetMouseDirection(){
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 playerPos = gameObject.transform.position;
        Vector2 mouseDirection = mouseWorldPosition - playerPos;
        return mouseDirection.normalized;
    }

    //Chat GPT code
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
}
