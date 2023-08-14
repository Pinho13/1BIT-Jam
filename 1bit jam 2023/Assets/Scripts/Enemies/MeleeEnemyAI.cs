using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;

public class MeleeEnemyAI : MonoBehaviour
{
    public bool following;

    [Header("Enemy Stats")]
    [SerializeField] float MaxHealth;
    [SerializeField] float currentHealth;

    [Header("References")]
    Transform playerPos;
    NavMeshAgent agent;
    Animator anim;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        FollowPlayer();
        LookWhereItsGoing();
    }

    void FollowPlayer()
    {
        if(following)
        {
            anim.SetBool("Atacking", false);
            agent.SetDestination(playerPos.position);
            anim.SetBool("Walking", true);
        }
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    following = false;
                    anim.SetBool("Walking", false);
                    anim.SetBool("Atacking", true);
                }
            }   
        }

        if (agent.hasPath || agent.velocity.sqrMagnitude != 0f)
        {
            following = true;
        }  
    }

    void LookWhereItsGoing()
    {
        var lookPos = playerPos.position - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
