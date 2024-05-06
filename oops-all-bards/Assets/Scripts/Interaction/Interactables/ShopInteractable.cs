using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShopInteractable : MonoBehaviour, IInteractable
{
    public bool triggering;

    [SerializeField]
    public ShopItem[] shop;

    ShopManager shopManager;

    // Start is called before the first frame update
    void Start()
    {
        shopManager = GetComponentInChildren<ShopManager>();
    }

    void Update()
    {
        if (triggering)
        {
            if (Input.GetKeyDown(KeyCode.F) && !DialogueManager.Instance.dialogueUI.activeInHierarchy)
            {
                Execute();
                triggering = false;
            }
        }
    }

    public void Execute()
    {
        // open a shop manager, populate it with items to sell
        //shopManager.PopulateShop(shop);
        shopManager.DisplayShop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
        }
    }
}

[System.Serializable]
public struct ShopItem
{
    public string name;
    public int cost;
}
