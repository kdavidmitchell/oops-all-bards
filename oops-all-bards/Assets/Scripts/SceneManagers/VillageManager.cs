using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : BaseSceneManager
{
    void Awake()
    {
        EntitySpawner.Instance.SpawnPlayer(playerSpawnPoint, playerCamera);
        base.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
