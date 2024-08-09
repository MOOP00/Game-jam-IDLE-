﻿using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using static InventorySlot;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public SO_Item Empty_Item;
    public Transform slotPrefab;
    public Transform inventoryPannel;
    protected GridLayoutGroup gridLayoutGroup;
    [Space(5)]
    public int SlotAmount = 12;
    public InventorySlot[] inventorySlots;

    [Header("Mini Canvas")]
    public RectTransform mimiCanvas;
    [SerializeField] protected InventorySlot rightClickSlot;
    public RectTransform Use_Main;
    public RectTransform Use_Support;

    [Header("Chest")]
    public Chest chest_Main;
    public Chest chest_Support;
    public TextMeshProUGUI text;

    [Header("Support 4 Button")]
    public WeaponSupport weaponSupport;
    public GameObject Support_Pannel;
    public Button use_1;
    public Button use_2;
    public Button use_3;
    public Button use_4;

    private ItemStat currentItemStat;
    void Start()
    {
        gridLayoutGroup = inventoryPannel.GetComponent<GridLayoutGroup>();
        CreateInventorySlot();
    }
    public void SetCurrentItemStat(ItemStat stat)
    {
        currentItemStat = stat;  
    }

    #region Inventory Methods

    public void AddItem(SO_Item item, int amount)
    {
        InventorySlot slot = IsEmptyLeft();
        if (slot == null)
        {
            ShowMessage();
            return;
        }
        slot.SetThisSlot(item, amount);
    }

    public void ShowMessage()
    {
        chest_Main.invFull = true;
        chest_Support.invFull = true;
        text.gameObject.SetActive(true);
        text.text = "Inventory Full: Please Remove Item";
        Invoke("WaitForTextDown", 4.0f);
    }

    public void WaitForTextDown()
    {
        text.gameObject.SetActive(false);
    }

    public void CreateInventorySlot()
    {
        inventorySlots = new InventorySlot[SlotAmount];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Transform slot = Instantiate(slotPrefab, inventoryPannel);
            InventorySlot invSlot = slot.GetComponent<InventorySlot>();

            inventorySlots[i] = invSlot;
            invSlot.Inventory = this;
            invSlot.SetThisSlot(Empty_Item, 0);
        }
    }

    public void RemoveItem(InventorySlot slot)
    {
        slot.SetThisSlot(Empty_Item, 0);
        OnFinishMiniCanvas();
    }

    public void UseItemMain() // OnClick Event
    {
        rightClickSlot.UseItem_Main();
        OnFinishMiniCanvas();
    }

    private void SupportButton()
    {
        Support_Pannel.SetActive(true);
    }

    public void UseItemSupport() // OnClick Event
    {
        SupportButton();
        OnFinishMiniCanvas();
    }
    public void UseItem_Support_1()
    {
        weaponSupport.EquipSupportItem(currentItemStat.item, currentItemStat.stacklvl, 0);
        Support_Pannel.SetActive(false);
    }
    public void UseItem_Support_2()
    {
        weaponSupport.EquipSupportItem(currentItemStat.item, currentItemStat.stacklvl, 1);
        Support_Pannel.SetActive(false);
    }
    public void UseItem_Support_3()
    {
        weaponSupport.EquipSupportItem(currentItemStat.item, currentItemStat.stacklvl, 2);
        Support_Pannel.SetActive(false);
    }
    public void UseItem_Support_4()
    {
        weaponSupport.EquipSupportItem(currentItemStat.item, currentItemStat.stacklvl, 3);
        Support_Pannel.SetActive(false);
    }

    public void RemoveItem() // OnClick Event
    {
        rightClickSlot.SetThisSlot(Empty_Item, 0);
        OnFinishMiniCanvas();
    }

    public void SetLayoutControlChild(bool isControlled)
    {
        gridLayoutGroup.enabled = isControlled;
    }

    public InventorySlot IsEmptyLeft(SO_Item itemChecker = null, InventorySlot itemslot = null)
    {
        InventorySlot firstEmptySlot = null;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot == itemslot)
                continue;
            if (slot.item == itemChecker && slot.stacklvl < slot.item.MaxLevel)
            {
                return slot;
            }
            else if (slot.item == Empty_Item && firstEmptySlot == null)
                firstEmptySlot = slot;
        }
        return firstEmptySlot;
    }
    #endregion

    #region Mini Canvas
    public void SetRightClickSlot(InventorySlot slot)
    {
        rightClickSlot = slot;
    }

    public void OpenMiniCanvas(Vector2 clickPos)
    {
        mimiCanvas.position = clickPos;
        if (rightClickSlot != null)
        {
            if (rightClickSlot.item.type == TypeWeapon.Main)
            {
                Use_Support.gameObject.SetActive(false);
                Use_Main.gameObject.SetActive(true);
            }
            else if (rightClickSlot.item.type == TypeWeapon.Support)
            {
                Use_Support.gameObject.SetActive(true);
                Use_Main.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("rightClickSlot is not set.");
        }

        mimiCanvas.gameObject.SetActive(true);
    }

    public void OnFinishMiniCanvas()
    {
        rightClickSlot = null;
        chest_Main.invFull = false;
        chest_Support.invFull = false;
        mimiCanvas.gameObject.SetActive(false);
    }
    #endregion
}