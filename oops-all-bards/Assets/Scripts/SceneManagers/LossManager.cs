using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LossManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RedirectToTavern()
    {
        DemoManager.Instance.LoadScene("TavernDemo");
    }
}
