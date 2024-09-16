using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    private bool hasMovingTarget = false;
    private PositionTracker pt;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pt = new PositionTracker(gameObject.name);
    }

    void Update()
    {
        if (agent.hasPath)
        {
            if (agent.remainingDistance < 0.5)
            {
                agent.isStopped = true;
                animator.SetBool("isMoving", false);
                hasMovingTarget = false;
            }
            // Update NPC's position in NPC data
            pt.updatePosition(transform.position);
        }
    }

    void FixedUpdate()
    {
        if (hasMovingTarget)
        {
            SendQuintonToPlayer();
        }
    }

    void SetGoalDestination(Transform goal)
    {
        animator.SetBool("isMoving", true);
        agent.destination = goal.position;
    }

    public void SendQuintonToBackroom()
    {
        Transform goal = GameObject.Find("QuintonQuestTrigger").transform;
        SetGoalDestination(goal);
    }

    public void SendQuintonToPlayer()
    {
        hasMovingTarget = true;
        Transform goal = GameObject.FindGameObjectWithTag("Player").transform;
        SetGoalDestination(goal);
    }

    public bool HasStopped()
    {
        return agent.velocity == Vector3.zero;
    }
}
