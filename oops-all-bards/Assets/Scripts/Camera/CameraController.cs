using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 left = new Vector3(0, -1, 0);
    private Vector3 right = new Vector3(0, 1, 0);
    private GameObject player;
    public float speed = 100;

    public float sensitivity = 100f; //test
    float xRotation = 0f;// test
    public Transform playerTransform;

    //test start
    private void Start()
    {
        //test
        transform.position = player.transform.Find("CameraTarget").position;

        playerTransform = player.transform;
        if (!DialogueManager.Instance.dialogueUI.activeSelf)
        {
            cursorLockPause();
        }

    }

    // Update is called after Update each frame
    void Update () 
    {
        // Align to camera target position
        transform.position = player.transform.Find("CameraTarget").position;

        //test
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localEulerAngles = Vector3.right * xRotation;

        playerTransform.Rotate(Vector3.up * mouseX);

        /*
        // Rotate left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(left * Time.deltaTime * speed, Space.World);
            player.transform.Rotate(left * Time.deltaTime * speed, Space.World);
        }
        
        // Rotate right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(right * Time.deltaTime * speed, Space.World);
            player.transform.Rotate(right * Time.deltaTime * speed, Space.World);
        }
        */
    }

    //Used to lock cursor at beginning of the level
    public void cursorLockPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    public void ToggleControls()
    {
        this.enabled = !this.enabled;
    }
}
