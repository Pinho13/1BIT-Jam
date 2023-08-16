using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using EZCameraShake;

public class MeleeEnemyAI : MonoBehaviour
{
    public bool following;

    [Header("Enemy Stats")]
    [SerializeField]float Damage;
    [SerializeField]float knockback;

    [Header("References")]
    GameObject playerPos;
    NavMeshAgent agent;
    Animator anim;

    [Header("CameraShake")]
    [SerializeField]Vector4 cameraShake;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
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
            agent.SetDestination(playerPos.transform.position);
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
        var lookPos = playerPos.transform.position - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    void HitOpponent()
    {
        playerPos.GetComponent<PlayerHealth>().currentHealth -= Damage;
        playerPos.GetComponent<Rigidbody2D>().AddForce((playerPos.transform.position - transform.position).normalized * knockback, ForceMode2D.Impulse);
        CameraShaker.Instance.ShakeOnce(cameraShake.x, cameraShake.y, cameraShake.z, cameraShake.w);
    }

}
