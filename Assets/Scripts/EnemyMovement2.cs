using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement2 : MonoBehaviour
{
   private Transform target;
   private int wavepointIndex = 0;
   private Enemy enemy;
   void Start ()
   {
    enemy = GetComponent<Enemy>();
    target = Waypoints2.points[0];
   }
   void Update ()
   {
    Vector3 dir = target.position - transform.position;
    transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

    if(Vector3.Distance(transform.position, target.position) <= 0.4f)
    {
        GetNextWayPoint();
    }  
    enemy.speed = enemy.startSpeed;
   }
   void GetNextWayPoint()
   {
    if(wavepointIndex >= Waypoints2.points.Length - 1)
    {
        EndPath();
        return;
    }
    wavepointIndex++;
    target = Waypoints2.points[wavepointIndex];
   }
   void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
