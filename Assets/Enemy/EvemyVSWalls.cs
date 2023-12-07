using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyVSWalls : AgentBehaviour
{
    static Vector3 up = new Vector3(0, 0, 1);
    static Vector3 up_right = new Vector3(1, 0, 1);
    static Vector3 right = new Vector3(1, 0, 0);
    static Vector3 down_right = new Vector3(1, 0, -1);
    static Vector3 down = new Vector3(0, 0, -1);
    static Vector3 down_left = new Vector3(-1, 0, -1);
    static Vector3 left = new Vector3(-1, 0, 0);
    static Vector3 up_left = new Vector3(-1, 0, 1);

    Vector3[] directions = new Vector3[] { up, up_right, right, down_right, down, down_left, left, up_left };

    float[] interest = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] danger = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] result = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };


    Vector3 playersLastPos = new Vector3();
    public bool drawARay = false;

    Vector3 CalculateLastSeenDir()
    {
        Collider[] detectionRange = Physics.OverlapSphere(transform.position, 4.0f);
        for (int i = 0; i < detectionRange.Length; i++)
        {
            if (detectionRange[i].tag == "Obstacle")
            {
                if (drawARay == true)
                    Debug.DrawRay(transform.position, detectionRange[i].transform.position - transform.position, Color.red);

                Vector3 fromEnemyToWall = (detectionRange[i].transform.position - transform.position).normalized;
                for (int j = 0; j < directions.Length; j++)
                {
                    danger[j] = Vector3.Dot(fromEnemyToWall, directions[j].normalized);
                }
            }
            if (detectionRange[i].tag == "Enemy")
            {
                if (drawARay == true)
                    Debug.DrawRay(transform.position, detectionRange[i].transform.position - transform.position, Color.magenta);

                Vector3 fromEnemyToEnemy = (detectionRange[i].transform.position - transform.position).normalized;
                for (int j = 0; j < directions.Length; j++)
                {
                    danger[j] += Vector3.Dot(fromEnemyToEnemy, directions[j].normalized);
                }

            }
        }
        for (int j = 0; j < directions.Length; j++)
        {
            interest[j] = Vector3.Dot((playersLastPos - transform.position).normalized, directions[j].normalized);
        }

        Vector3 resultDir = new Vector3();
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


    public void UpdatePlayersLastPos()
    {
        Vector3 playerDir = target.transform.position - transform.position;
        RaycastHit rayOnThePlayer;

        if (Physics.Raycast(transform.position, playerDir, out rayOnThePlayer))
        {
            if (rayOnThePlayer.collider.tag == "Player")
            {
                playersLastPos = rayOnThePlayer.transform.position;
            }
        }
    }
    public override Steering GetSteering()
    {

        Steering accelToThePoint = new Steering();
        UpdatePlayersLastPos();
        Vector3 validMovementDir = CalculateLastSeenDir();
        // Vector3 normilisedDirToPoint = (playersLastPos - transform.position).normalized;
        accelToThePoint.linear = (validMovementDir * agent.maxSpeed) - agent.velocity;

        //Debug stuff
        if (drawARay == true)
            Debug.DrawRay(transform.position, playersLastPos - transform.position, Color.green);

        return accelToThePoint;


    }

    // Start is called before the first frame update

}


