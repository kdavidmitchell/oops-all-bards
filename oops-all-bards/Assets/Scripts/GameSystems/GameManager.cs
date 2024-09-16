using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// A class that manages the features of the combat demo.
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => GameManager._instance;

    public int tavernVisits = 1;
    public bool completedGame = false;

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

    // Start is called before the first frame update
    void Start()
    {

        // Subscribe to events that the demo manager should be aware of.
        SubscribeToEvents();

        // Add player to the party.
        PartyManager.Instance.AddCharacterToParty(DataManager.Instance.PlayerData);
        TCPTestClient.Instance.RefreshWMEs();
    }

    // Update is called once per frame
    void Update()
    {

        if (completedGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }

    // A function used to debug the player object.
    private void DebugPlayer(BasePlayer player)
    {
        Debug.Log(player.Name);
        Debug.Log("FAME: " + player.Fame);
        Debug.Log("GOLD: " + player.Gold);
        Debug.Log(player.PlayerClass.Name);
        foreach (BaseAbility ability in player.PlayerClass.Abilities)
        {
            Debug.Log(ability.Name + " " + ability.Damage + " " + ability.Cost);
        }
    }

    // A function that uses the event management system to subscribe to events used in this manager.
    private void SubscribeToEvents()
    {
        EventManager.Instance.SubscribeToEvent(EventType.CheckQueue, CheckQueue);
        EventManager.Instance.SubscribeToEvent(EventType.AwaitPlayerInput, AwaitPlayerInput);
        EventManager.Instance.SubscribeToEvent(EventType.CombatLoss, CombatLoss);
        EventManager.Instance.SubscribeToEvent(EventType.CombatWin, CombatWin);
    }

    public void CheckQueue()
    {
        // Refresh WMEs every time we check the queue.
        TCPTestClient.Instance.RefreshWMEs();
        if (!CombatManager.Instance.combatQueue.IsEmpty())
        {
            ICombatQueueable cq = CombatManager.Instance.combatQueue.Pop();
            cq.Execute();
            if (CombatManager.Instance.combatQueue.queue.Count < CombatUI.Instance.queueDisplay.Count)
            {
                CombatUI.Instance.RenderPop();
            }
        }
        else
        {
            Debug.Log("Combat round has ended. Resetting queue.");
            CombatManager.Instance.rounds += 1;
            CombatManager.Instance.InitCombatQueue(CombatManager.Instance.party, CombatManager.Instance.enemies);
        }
    }

    // A function that enables player input when it is the player's turn in the combat queue.
    private void AwaitPlayerInput()
    {
        Debug.Log("Awaiting player input...");
    }

    public void CombatWin()
    {
        PartyManager.Instance.ToggleInCombat(false);
        DemoManager.Instance.LoadScene("CombatWin");
    }

    public void CombatLoss()
    {
        PartyManager.Instance.ToggleInCombat(false);
        DemoManager.Instance.LoadScene("CombatLoss");
    }

    public void TogglePlayerControls()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera camera = Camera.main;
        player.GetComponent<PlayerController>().ToggleControls();
        camera.GetComponent<CameraController>().ToggleControls();
    }

    public static IEnumerator togglePlayerPause()
    {
        //Delay necessary to prevent cursor from being locked too soon between start of level and 1st help message 
        yield return new WaitForSeconds(0.003f);
        //Code enables cursor to be used to close the signpost message
        Cursor.lockState = CursorLockMode.Confined;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraController>().enabled = false;
    }


    public void IncrementTavernVisits()
    {
        tavernVisits++;
    }



}
