using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCreationManager : MonoBehaviour
{
    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    public GameObject errorMessage;
    public ModelViewer modelViewer;
    public ClassViewer classViewer;
    public TMP_InputField nameInputField;
    public BlackFade fader;
    private int currentStep = 0;
    private BasePlayer playerData = new BasePlayer();

    // Start is called before the first frame update
    void Start()
    {
        ContinueToNextStep();
    }

    // Continue to the next step on button click.
    public void ContinueToNextStep()
    {
        currentStep++;
        if (currentStep == 1)
        {
            ToggleGameObject(step2);
            ToggleGameObject(step3);
        } else if (currentStep == 2)
        {
            SetPlayerModel(modelViewer.GetModel());
            ToggleGameObject(step1);
            ToggleGameObject(step2);
        } else if (currentStep == 3)
        {
            SetPlayerClass(classViewer.GetClass());
            ToggleGameObject(step2);
            ToggleGameObject(step3);
        } else
        {
            //This if statement prevents player from skipping putting in a name
            if (string.IsNullOrEmpty(nameInputField.text))
            {
                //Display error message
                errorMessage.SetActive(true);
                ToggleGameObject(step3);
                ToggleGameObject(step3);

            }
            else
            {
                errorMessage.SetActive(false);
                SetPlayerName(nameInputField.text);
                ExitCharacterCreationScene();
            }
        }
    }

    // Toggle a gameobject's visibility.
    void ToggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    // Save all player data and exit character creation scene.
    void ExitCharacterCreationScene()
    {
        FinalizePlayerData(playerData.PlayerClass);
        DataManager.Instance.PlayerData = playerData;
        DataManager.Instance.SavePlayerData();
        //SceneManager.LoadScene("TavernDemo"); 

        fader.FadeToLevel("TavernDemo"); //TAVERNDEMO
    }

    void SetPlayerModel(GameObject model)
    {
        playerData.Model = model;
    }

    void SetPlayerClass(BaseClass playerClass)
    {
        playerData.PlayerClass = playerClass;
    }

    void SetPlayerName(string name)
    {
        playerData.Name = name;
    }

    void FinalizePlayerData(BaseClass playerClass)
    {
        playerData.Health = 15;
        playerData.Flourish = 10;
        BaseAbility influence = new BaseAbility("Influence", 99, "Suggest a plan of action for an Ally. Results may vary.", BaseAbility.AbilityTypes.COMBAT, BaseAbility.CombatAbilityTypes.SUPPORT, 0, 0, 1, 1);
        playerData.PlayerClass.Abilities.Add(influence);
    }
}
