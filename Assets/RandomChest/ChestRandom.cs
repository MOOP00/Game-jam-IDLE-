using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Chest : MonoBehaviour
{
    public WeightedRandomList lootTable;

    public GameObject This_item;
    public Transform itemHolder;
    public SO_Item itemData;
    public Inventory inventory;

    [Header("Animator")]
    public Animator anim;

    [Header("UI")]
    public Button TapOpen_Button;
    public Button CollectButton;
    public Button DropButton;

    [Header("Text_Rarity")]
    public TextMeshProUGUI text;

    [Header("Text_Stat")]
    public GameObject panel;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI Dmg;
    public TextMeshProUGUI Multi;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI Type;

    [Header("Req")]
    public bool Pressed;
    public bool invFull;

    public int price;
    public TextMeshProUGUI NoMoney;

    private int itemImageCounter = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        TapOpen_Button = GetComponent<Button>();
        CollectButton.gameObject.SetActive(false);
        DropButton.gameObject.SetActive(false);

        if (TapOpen_Button != null)
        {
            TapOpen_Button.onClick.AddListener(ToggleChest);
        }
    }

    void UpdateText(SO_Item data)
    {
        text.color = GetRarityColor(data.rarity);
        text.gameObject.SetActive(true);
        text.text = data.itemName;
    }

    void UpdateStat(SO_Item data)
    {
        itemname.text = data.itemName;
        Dmg.text = data.Damgae.ToString();
        rarity.text = GetRarityName(data.rarity);
        rarity.color = GetRarityColor(data.rarity);
        Type.text = GetItemTypeName(data.type);
        Multi.text = CalculateValue(data.rarity);
    }

    string GetItemTypeName(TypeWeapon type)
    {
        switch (type)
        {
            case TypeWeapon.Main:
                return "Main Weapon";
            case TypeWeapon.Support:
                return "Support Weapon";
            default:
                return "Unknown";
        }
    }

    Color GetRarityColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Color.white;
            case Rarity.Uncommon:
                return Color.green;
            case Rarity.Rare:
                return Color.blue;
            case Rarity.Epic:
                return Color.magenta;
            case Rarity.Legendary:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    string GetRarityName(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return "Common";
            case Rarity.Uncommon:
                return "Uncommon";
            case Rarity.Rare:
                return "Rare";
            case Rarity.Epic:
                return "Epic";
            case Rarity.Legendary:
                return "Legendary";
            default:
                return "Unknown";
        }
    }

    string CalculateValue(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return "x1.25 + 0.25:lvl";
            case Rarity.Uncommon:
                return "x2 + 0.5:lvl";
            case Rarity.Rare:
                return "x5 + 1.25:lvl";
            case Rarity.Epic:
                return "x10 + 2.5:lvl";
            case Rarity.Legendary:
                return "x50 + 10:lvl";
            default:
                return "Unknown";
        }
    }

    public void ToggleChest()
    {
        if (Pressed && !invFull && Coin.Instance.Coins >= price)
        {
            Coin.Instance.SpendCoins(price);
            ShowItem();
        }
        else
        {
            NoMoney.gameObject.SetActive(true) ;
            StartCoroutine(HideNoMoneyAfterDelay(2.0f));
        }
    }
    IEnumerator HideNoMoneyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NoMoney.gameObject.SetActive(false);
    }

    void HideItem()
    {
        text.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        CollectButton.gameObject.SetActive(false);
        DropButton.gameObject.SetActive(false);
        for (int i = itemHolder.childCount - 1; i >= 0; i--)
        {
            Destroy(itemHolder.GetChild(i).gameObject);
        }
        itemHolder.gameObject.SetActive(false);
    }

    public void Collect()
    {
        inventory.AddItem(itemData, 1);
        Pressed = true;
        HideItem();
    }

    public void Drop()
    {
        Pressed = true;
        HideItem();
    }

    void ShowItem()
    {
        Pressed = false;
        CollectButton.gameObject.SetActive(true);
        DropButton.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);

        // Get a new random item from lootTable
        SO_Item randomItem = lootTable.GetRandom();

        // Clone the random item
        itemData = randomItem.Clone();

        // Assign a unique string ID
        itemData.id = $"{itemData.itemName}_{++itemImageCounter}";

        UpdateText(itemData);
        UpdateStat(itemData);

        if (itemData != null)
        {
            if (itemData.icon != null)
            {
                GameObject imageObject = new GameObject("ItemImage");
                imageObject.transform.SetParent(itemHolder, false);

                Image itemImage = imageObject.AddComponent<Image>();
                itemImage.sprite = itemData.icon;
                itemImage.rectTransform.sizeDelta = new Vector2(50, 50);

                itemHolder.gameObject.SetActive(true);
            }
        }
    }
}
