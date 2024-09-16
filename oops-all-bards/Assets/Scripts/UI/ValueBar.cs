using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    // References to the text and fill components of the value bar.
    private TMP_Text textComponent;
    private Image fillComponent;
    // References to max value.
    public int maxValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update value bar with new value to display
    public void UpdateValueBar(int newValue)
    {
        float ratio = (float)newValue / (float)maxValue;
        textComponent = gameObject.GetComponentInChildren<TMP_Text>();
        fillComponent = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //textComponent.text = newValue.ToString() + " / " + maxValue;
        textComponent.text = newValue.ToString();
        fillComponent.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
    }
}
