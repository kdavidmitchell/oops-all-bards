using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class DialogueHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color savedColor;

    //Saved the original text color to switch back to later
    void Start()
    {
        savedColor = gameObject.GetComponentInChildren<TMP_Text>().color;

    }

    //Show highlighted text when player hovers over a response
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    //When not hovering over a response, text color should switch back to the original text color
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = savedColor;
    }


}