using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIData : MonoBehaviour
{
    public Image portrait;
    public TMP_Text speakerName;
    public TMP_Text nodeText;
    public GameObject nodeContentOrganizer;

    void Awake()
    {
        Debug.Log($"object name: {this.gameObject.transform.name}");


        portrait = transform.Find("PortraitPanel").transform.Find("Portrait").transform.GetComponent<Image>();
        speakerName = transform.Find("PortraitPanel").Find("Name").transform.GetComponent<TMP_Text>();
        nodeText = transform.Find("TextPanel").transform.Find("UsableSpace").Find("NodeText").transform.GetComponent<TMP_Text>();
        nodeContentOrganizer = transform.Find("TextPanel").transform.Find("UsableSpace").Find("NodeContentOrganizer").transform.gameObject;
       
    }

    public void DisableUI()
    {
        gameObject.SetActive(false);
    }
}
