using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassViewer : MonoBehaviour
{
    public GameObject className;
    public GameObject classDescription;
    public GameObject[] classStats;
    public GameObject[] classAbilites;
    public GameObject[] abilityDescriptions;
    
    //A reference to the tooltip UI
    public CanvasGroup currentToolTip;
    public JSONReader jsonReader;
    private int index = 0;
    private int max = 3;
    private BaseClass classToRender;

    void Awake()
    {
        UpdateClassViewer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Increment index
    public void IncrementIndex()
    {
        this.index++;
        if (index > max)
        {
            index = 0;
        }
        UpdateClassViewer();
    }

    // Decrement index
    public void DecrementIndex()
    {
        this.index--;
        if (index < 0)
        {
            index = max;
        }
        UpdateClassViewer();
    }

    // Update the class viewer
    private void UpdateClassViewer()
    {
        classToRender = jsonReader.baseClasses.baseClasses[index];
        RenderClass(classToRender);
    }

    // Render the current class's name, description, stats, and abilities to appropriate GameObject
    private void RenderClass(BaseClass c)
    {
        className.GetComponent<TMP_Text>().text = c.Name;
        classDescription.GetComponent<TMP_Text>().text = c.Description;
        for (int i = 0; i < c.Stats.Count; i++)
        {
            classStats[i].GetComponent<TMP_Text>().text = c.Stats[i].Name + ": " + c.Stats[i].BaseValue; 
        }
        for (int i = 0; i < c.Abilities.Count; i++)
        {
            classAbilites[i].GetComponent<TMP_Text>().text = c.Abilities[i].Name;
            abilityDescriptions[i].GetComponent<TMP_Text>().text = c.Abilities[i].Description;
            abilityDescriptions[i].AddComponent<ToolTips>(); //Add comment later when not dead
        }
    }

    public BaseClass GetClass()
    {
        return classToRender;
    }
}
