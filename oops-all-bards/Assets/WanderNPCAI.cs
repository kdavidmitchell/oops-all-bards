using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNPCAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float rotationSpeed = 75f;

    private bool isWandering = false;

    private const string idleAnimation = "Idle";
    private const string walkAnimation = "Walk";

    private int rotationTimeMin = 1;
    private int rotationTimeMax = 4;

    private int rotationWaitMin = 1;
    private int rotationWaitMax = 4;

    private int walkWaitMin = 1;
    private int walkWaitMax = 5;

    private int walkTimeMin = 1;
    private int walkTimeMax = 5;

    private enum State { Wandering, RotatingLeft, RotatingRight, Walking, Idle };
    private State state;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        if (!isWandering)
        {
            StartCoroutine(Wander());
        }

        if (state == State.RotatingRight)
        {
            animator.Play(idleAnimation);
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        else if (state == State.RotatingLeft)
        {
            animator.Play(idleAnimation);
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }
        else if (state == State.Walking)
        {
            animator.Play(walkAnimation);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (state == State.Idle)
        {
            animator.Play(idleAnimation);
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(rotationTimeMin, rotationTimeMax);
        int rotateWait = Random.Range(rotationWaitMin, rotationWaitMax);
        int walkWait = Random.Range(walkWaitMin, walkWaitMax);
        int walkTime = Random.Range(walkTimeMin, walkTimeMax);
        int rotateLorR = Random.Range(1, 2);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        state = State.Walking;
        yield return new WaitForSeconds(walkTime);
        state = State.Idle;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            state = State.RotatingRight;
            yield return new WaitForSeconds(rotTime);
            state = State.Idle;
        }
        if (rotateLorR == 2)
        {
            state= State.RotatingLeft;
            yield return new WaitForSeconds(rotTime);
            state = State.Idle;
        }
        isWandering = false;
    }
}