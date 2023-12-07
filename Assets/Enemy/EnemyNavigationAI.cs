using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyNavigationAI : MonoBehaviour{
    Enemy enemy;
    [SerializeField] GameObject Target;
    [SerializeField] GameObject Obstacle;
    [SerializeField] float obstacleDetectionRadius = 4.0f;

    static Vector2 up = new Vector2(0, 1);
    static Vector2 up_right = new Vector2(1, 1);
    static Vector2 right = new Vector2(1, 0);
    static Vector2 down_right = new Vector2(1,-1);
    static Vector2 down = new Vector2(0, -1);
    static Vector2 down_left = new Vector2(-1, -1);
    static Vector2 left = new Vector2(-1, 0);
    static Vector2 up_left = new Vector2(-1, 1);

    Vector2[] directions = new Vector2[] { up, up_right, right, down_right, down, down_left, left, up_left };

    float[] interest = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] danger = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] result = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };

    Vector2 targetLastSeenPos = new Vector2();
    private Rigidbody2D rb;
    private CircleCollider2D enemyCollider;

    //Ray related debug methods
    public bool drawObstacleRays = false;
    public bool drawLastTargetPos = false;
    public bool drawSphere = false;
    public bool drawTargetPos = true;

    private void Awake(){
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CircleCollider2D>();

        if(Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }

        if(Obstacle == null)
        {
            Obstacle = GameObject.FindGameObjectWithTag("Wall");
        }
    }
    Vector2 CalculateMoveDir()
    {
        //Converting all the vec3 positions to vec2
        Vector2 transformPos = transform.position;

        //Calculate the interest array
        for (int i = 0; i < directions.Length; i++)
        {
            interest[i] = Vector2.Dot((targetLastSeenPos - transformPos).normalized, directions[i].normalized);
        }

        //Cast a sphere around the object to detect obstacles
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transformPos, obstacleDetectionRadius);
        foreach(Collider2D obstacle in obstacles) { 
            //If what we're reading is the enemy itself, skip it
            if (obstacle.tag == gameObject.tag) { continue; }

            if (obstacle.tag == Obstacle.tag)
            {
                //Casting the vec3 position to vec2
                Vector2 obstaclePos = obstacle.transform.position;

                //Draws a ray from the object to the detected obstacle
                if (drawObstacleRays == true)
                    Debug.DrawRay(transformPos, obstaclePos - transformPos, Color.yellow);

                Vector2 obstacleDirection = (obstaclePos - transformPos).normalized;
                for (int i = 0; i < directions.Length; i++)
                {
                    danger[i] = Vector2.Dot(obstacleDirection, directions[i].normalized);
                }
            }
        }

        Vector2 resultDir = new Vector2();
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Mathf.Clamp01(interest[i] - danger[i]);

        }

        for (int i = 0; i < result.Length; i++)
        {
            resultDir += directions[i] * result[i];
        }

        return resultDir.normalized;
    }

    public void UpdateTargetLastScenePos()
    {
        //Casting some variables to vector2 
        //Because vector3 and vector2 is totally different
        Vector2 transformPos = transform.position;
        Vector2 targetDir = Target.transform.position - transform.position;

        //Offsetting the ray starting position so it doesn't hit the object casting the ray
        float buffer = 0.2f;
        Vector2 rayStartingPos = transformPos + (targetDir.normalized * (enemyCollider.radius + buffer));
        float maxRayDistance = targetDir.magnitude - (enemyCollider.radius + buffer);

        RaycastHit2D firstHit = Physics2D.Raycast(rayStartingPos, targetDir, maxRayDistance);

        if (firstHit){
            //Debug.Log(firstHit.collider.gameObject.name);
            if (firstHit.collider.tag == Target.tag)
            {
                targetLastSeenPos = firstHit.transform.position;
            }
        }

        if (drawTargetPos)
        {
            //Draw a ray from the enemy's position to the target's position
            Debug.DrawRay(rayStartingPos, targetDir.normalized * maxRayDistance, Color.green);
        }
        if (drawLastTargetPos)
        {
            //Draw a ray to the last seen position of the target
            Debug.DrawRay(rayStartingPos, targetLastSeenPos - rayStartingPos, Color.red);
        }
    }

    //Modified ChatGPT code
    void DrawCircle(Vector3 center, float radius, int segments)
    {
        Gizmos.color = Color.green;

        // You can adjust the number of segments for a smoother circle
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

    private void OnDrawGizmos()
    {
        if (drawSphere) DrawCircle(transform.position, obstacleDetectionRadius, 10);
    }

    void Update()
    {
        UpdateTargetLastScenePos();
        Vector2 validMovementDir = CalculateMoveDir();
        Vector2 totalVel = validMovementDir * enemy.maxSpeed;
        totalVel = Vector2.ClampMagnitude(totalVel, enemy.maxSpeed);

        rb.velocity += totalVel * Time.deltaTime;
       
    }
}
