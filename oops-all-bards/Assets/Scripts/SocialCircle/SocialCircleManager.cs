using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialCircleManager : MonoBehaviour
{
    private static SocialCircleManager _instance;
    public static SocialCircleManager Instance => SocialCircleManager._instance;

    public GameObject socialCircleUI;
    public GameObject characterSocialCircleUI;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !DialogueManager.Instance.dialogueUI.activeInHierarchy) //test key
        {
            //if menu is already enabled disable it
            if (socialCircleUI.activeSelf || characterSocialCircleUI.activeSelf)
            {
                ToggleSocialCircleUIOff();
            }
            //otherwise enable the menu
            else
            {
                ToggleSocialCircleUIOn();
                RenderSocialCircleUI();
            }
        }
    }

    public void ToggleSocialCircleUIOn()
    {
       socialCircleUI.SetActive(true);
       characterSocialCircleUI.SetActive(true);

        //lock movement and camera while in menu
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.StartCoroutine(GameManager.togglePlayerPause());
        
    }

    public void ToggleSocialCircleUIOff()
    {
        socialCircleUI.SetActive(false);
        characterSocialCircleUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.TogglePlayerControls();
    }

        public void RenderSocialCircleUI()
    {
        //stuff goes here eventually
    }
}
