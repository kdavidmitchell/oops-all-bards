using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopButton;
    ScrollRect scrollRect;

    ShopInteractable shop;
    List<ShopItemButtonData> shopButtons = new List<ShopItemButtonData>();

    Transform panel;
    public TextMeshProUGUI tooltip;
    TextMeshProUGUI goldTooltip;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        shop = GetComponentInParent<ShopInteractable>();
        anim = GetComponent<Animator>();
        panel = GameObject.Find("Canvas").transform.Find("Panel");
        tooltip = transform.Find("Tooltip Panel").transform.Find("Tooltip").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        goldTooltip = transform.Find("Gold Panel").transform.Find("Tooltip").transform.Find("Text").GetComponent<TextMeshProUGUI>();

        PopulateShop();
    }

    public void PopulateShop()
    {
        foreach (ShopItem item in shop.shop)
        {
            ShopItemButtonData obj = Instantiate(shopButton, scrollRect.content).GetComponent<ShopItemButtonData>();

            obj.player = DataManager.Instance.PlayerData;
            obj.shop = this;
            BaseItem itemData = BaseItem.GetItem(item.name);
            obj.text.text = $"{itemData.DisplayName} - {item.cost}";
            if (itemData.Icon != null)
            {
                obj.icon.sprite = itemData.Icon;
            }
            obj.item = itemData;
            obj.cost = item.cost;
            shopButtons.Add(obj);
            obj.UpdateAvailability();
        }
        UpdateShop();
    }

    public void DisplayShop()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.StartCoroutine(GameManager.togglePlayerPause());
        transform.SetParent(panel, false);
    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.TogglePlayerControls();
        transform.SetParent(shop.transform, false);
        shop.triggering = true;
    }

    public void UpdateShop()
    {
        goldTooltip.text = DataManager.Instance.PlayerData.Gold.ToString();
        foreach(ShopItemButtonData button in shopButtons)
        {
            button.UpdateAvailability();   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
