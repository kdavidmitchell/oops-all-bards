using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private static EntitySpawner _instance;
    public static EntitySpawner Instance => EntitySpawner._instance;

    // Singleton pattern
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayer(GameObject spawnPoint, CameraController playerCamera)
    {
        // Instantiate chosen player model at spawn point.
        BasePlayer playerData = DataManager.Instance.PlayerData;
        GameObject toInstantiate = Instantiate(playerData.Model, spawnPoint.transform.position, Quaternion.identity);
        // Set up rigidbody and collider for physics.
        Rigidbody rb = toInstantiate.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        // Set up player controller script and camera follow.
        PlayerController playerController = toInstantiate.AddComponent<PlayerController>();
        playerCamera.Player = toInstantiate;
        // Set up player animator controller.
        Animator a = toInstantiate.GetComponent<Animator>();
        a.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Controllers/Player");
        // Add player tag.
        toInstantiate.tag = "Player";
        Cursor.lockState = CursorLockMode.Locked; //testing
    }
}
