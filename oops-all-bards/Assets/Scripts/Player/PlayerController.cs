using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private Vector3 forward;
    private Vector3 right;
    public float speed = 5;

    void Start()
    {
        //Sets camera to be a child of the player
        GameObject.FindWithTag("MainCamera").transform.parent = gameObject.transform;
        player = gameObject;
        animator = player.GetComponent<Animator>();
    }
 
    void Update()
    {
        // Update forward/right for rotation of player
        forward = player.transform.forward;
        right = player.transform.right;

        //Handle animation parameters
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        // Move Forwards
        if (Input.GetKey(KeyCode.W)) 
        {
            player.transform.position += forward * speed * Time.deltaTime;
        }

        // Move Backwards
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position += -forward * speed * Time.deltaTime;
        }
        
        // Move Rightwards
        if (Input.GetKey(KeyCode.D))
        {
           player.transform.position += right * speed * Time.deltaTime;
        }
        
        // Move Leftwards
        if (Input.GetKey(KeyCode.A))
        {
           player.transform.position += -right * speed * Time.deltaTime;
        }
    }

    public void ToggleControls()
    {
        this.enabled = !this.enabled;
    }
}
