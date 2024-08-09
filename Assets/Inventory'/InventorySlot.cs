using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static WeightedRandomList;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [Header("INVENTORY DETAIL")]
    public Inventory Inventory;

    [Header("Slot Detail")]
    public SO_Item item;
    public int stacklvl;

    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI stacklvltext;

    [Header("Drag and Drop")]
    public int siblingIndex;
    public RectTransform draggable;
    Canvas Canvas;
    CanvasGroup CanvasGroup;

    [Header("WeaponSlot")]
    public Weapon WeaponSlot;

    [System.Serializable]
    public struct ItemStat
    {
        public SO_Item item;
        public int stacklvl;

        public ItemStat(SO_Item item, int stacklvl)
        {
            this.item = item;
            this.stacklvl = stacklvl;
        }
    }
    public List<ItemStat> This_Item = new List<ItemStat>();
    void Start()
    {
        WeaponSlot = FindAnyObjectByType<Weapon>();
        Canvas = GetComponentInParent<Canvas>();
        CanvasGroup = GetComponent<CanvasGroup>();
        siblingIndex = transform.GetSiblingIndex();
    }
    #region Drag And Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        CanvasGroup.alpha = 0.6f;
        CanvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
        Inventory.SetLayoutControlChild(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggable.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.blocksRaycasts = true;
        draggable.anchoredPosition = Vector2.zero;
        transform.SetSiblingIndex(siblingIndex);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();
            if (slot != null)
            {
                if (slot.item == item && item != Inventory.Empty_Item)
                {
                    MergeSlot(slot);
                }
                else
                {
                    SwapSlot(slot);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item == Inventory.Empty_Item)
                return;

            ItemStat stat = new ItemStat(item, stacklvl);
            Inventory.SetRightClickSlot(this);
            Inventory.SetCurrentItemStat(stat);
            Inventory.OpenMiniCanvas(eventData.position);
        }
    }


    #endregion

    public void SwapSlot(InventorySlot newSlot)
    {
        SO_Item keepItem = item;
        int keepstack = stacklvl;

        SetSwap(newSlot.item, newSlot.stacklvl);
        newSlot.SetSwap(keepItem, keepstack);
    }

    public void SetSwap(SO_Item swapItem, int amount)
    {
        item = swapItem;
        stacklvl = amount;
        icon.sprite = swapItem.icon;

        CheckShowText();
    }

    public void MergeSlot(InventorySlot mergeSlot)
    {
        int totalAmount = stacklvl + mergeSlot.stacklvl;
        int maxLevel = item.MaxLevel;

        stacklvl = Mathf.Clamp(totalAmount, 0, maxLevel);
        mergeSlot.stacklvl = totalAmount - stacklvl;

        CheckShowText();
        mergeSlot.CheckShowText();

        if (mergeSlot.stacklvl == 0)
        {
            Inventory.RemoveItem(mergeSlot);
        }
    }
    public void UseItem_Main()
    {
        WeaponSlot.EquipWeapon(item, stacklvl);
    }
    public ItemStat ReturnStat()
    {
        return new ItemStat(item, stacklvl);
    }

    public void SetThisSlot(SO_Item newitem, int amount)
    {
        item = newitem;
        icon.sprite = newitem.icon;
        stacklvl = Mathf.Clamp(amount, 0, newitem.MaxLevel);
        CheckShowText();
    }

    public void CheckShowText()
    {
        stacklvltext.text = stacklvl.ToString();
        if (item.MaxLevel < 2)
        {
            stacklvltext.gameObject.SetActive(false);
        }
        else
        {
            stacklvltext.gameObject.SetActive(stacklvl > 1);
        }
    }
}