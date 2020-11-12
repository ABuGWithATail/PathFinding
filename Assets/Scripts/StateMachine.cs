using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class StateMachine : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Player Statistics.")]
    public float speed = 5.0f;
    public GameObject aiSprite;
    public GameObject playerObject;
    public Rigidbody2D playerRigidbody;
    public float enemyHealth = 5;

    [Header("Waypoint Statistics.")]
    public GameObject[] Waypoint;
    public float MinDistanceToWaypoint;
    public float ChasePlayerDistance;
    public int CurrentWaypoint = 0;

    void Update()
    {
        if (Vector2.Distance(playerObject.transform.position, aiSprite.transform.position) < ChasePlayerDistance)
        {
            MoveAi(playerObject.transform.position);
        }
        if(enemyHealth <= 1)
        {
            Flee(playerObject.transform.position);
        }
        else
        {
            Patrol();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void Patrol()
    {
        float distance = Vector2.Distance(aiSprite.transform.position, Waypoint[CurrentWaypoint].transform.position);
        //Are we at the target location
        if (distance < MinDistanceToWaypoint)
        {
            CurrentWaypoint++;
        }

        //if we reached the last waypoint, start again
        if (CurrentWaypoint >= Waypoint.Length)
        {
            CurrentWaypoint = 0;
        }
        MoveAi(Waypoint[CurrentWaypoint].transform.position);

    }

    private void Flee(Vector2 targetPosition)
    {

            aiSprite.transform.position = Vector2.MoveTowards(aiSprite.transform.position, -targetPosition, speed * Time.deltaTime);
    }

    private void MoveAi(Vector2 targetPosition)
    {
        //move ai
        aiSprite.transform.position = Vector2.MoveTowards(aiSprite.transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void DealDamage()
    {
        Destroy(playerObject);
        Time.timeScale = 0;
    }

    public void TakeDamage()
    {
        enemyHealth -= 4;
    }

}

