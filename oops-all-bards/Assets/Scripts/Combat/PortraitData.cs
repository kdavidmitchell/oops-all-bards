using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PortraitData : MonoBehaviour
{
    public TMP_Text nameText;
    public Image icon;
    public ValueBar healthBar;
    public ValueBar flourishBar;
    public Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("1");
        nameText = transform.Find("Frame").transform.Find("Background").transform.Find("NameText").GetComponent<TMP_Text>();
        icon = transform.Find("Frame").transform.Find("Background").transform.Find("Icon").GetComponent<Image>();
        healthBar = transform.Find("Frame").transform.Find("Background").transform.Find("HealthBar").GetComponent<ValueBar>();
        flourishBar = transform.Find("Frame").transform.Find("Background").transform.Find("FlourishBar").GetComponent<ValueBar>();
        anim = GetComponent<Animator>();
    }
}
