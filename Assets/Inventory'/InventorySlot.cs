using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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
    public TextMeshProUGUI Type;

    [Header("Drag and Drop")]
    public int siblingIndex;
    public RectTransform draggable;
    Canvas Canvas;
    CanvasGroup CanvasGroup;

    [Header("WeaponSlot")]
    public Weapon WeaponSlot;
    public WeaponSupport weaponSupport;

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
        weaponSupport = FindObjectOfType<WeaponSupport>();
        WeaponSlot = FindObjectOfType<Weapon>();
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
            InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
            if (draggedSlot != null)
            {
                bool handled = false; // ตัวบ่งชี้ว่าไอเท็มถูกจัดการหรือไม่

                if (item != null && draggedSlot.item != null && item.itemName == draggedSlot.item.itemName)
                {
                    if (item.type == TypeWeapon.Main)
                    {
                        // จัดการอาวุธหลัก
                        if (WeaponSlot != null && WeaponSlot.HasWeaponWithName(item.itemName))
                        {
                            MergeSlot(draggedSlot);
                            UseItem_Main();
                            handled = true;
                        }
                        else
                        {
                            // ทำให้ไอเท็มอยู่ที่ตำแหน่งที่ต้องการ
                            if (eventData.pointerCurrentRaycast.gameObject != null)
                            {
                                InventorySlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
                                if (targetSlot != null)
                                {
                                    // ตั้งค่าตำแหน่งของไอเท็มที่ลากให้ตรงกับตำแหน่งของช่องเป้าหมาย
                                    draggedSlot.transform.position = targetSlot.transform.position;
                                    draggedSlot.transform.SetSiblingIndex(targetSlot.transform.GetSiblingIndex());
                                }
                            }

                            MergeSlot(draggedSlot);
                            handled = true;
                        }
                    }
                    else if (item.type == TypeWeapon.Support)
                    {
                        // จัดการอาวุธสนับสนุน
                        if (weaponSupport != null)
                        {
                            int slotIndex = -1;
                            bool draggedItemInSupport = false;
                            bool currentItemInSupport = false;

                            // ตรวจสอบว่าไอเท็มที่ถูกลากอยู่ในช่องสนับสนุนหรือไม่
                            for (int i = 0; i < weaponSupport.supportSlots.Length; i++)
                            {
                                if (weaponSupport.supportSlots[i].itemData != null)
                                {
                                    if (weaponSupport.supportSlots[i].itemData.itemName == draggedSlot.item.itemName)
                                    {
                                        slotIndex = i;
                                        draggedItemInSupport = true;
                                    }

                                    if (weaponSupport.supportSlots[i].itemData.itemName == item.itemName)
                                    {
                                        currentItemInSupport = true;
                                    }
                                }
                            }

                            if (slotIndex != -1)
                            {
                                // ทั้งสองไอเท็มอยู่ในช่องสนับสนุน
                                if (draggedItemInSupport && currentItemInSupport)
                                {
                                    MergeSlot(draggedSlot);
                                    weaponSupport.EquipSupportItem(item, stacklvl, slotIndex);
                                    Debug.Log("After merge: " + stacklvl);
                                    Debug.Log("EquipSupportItem called with stacklvl: " + stacklvl);

                                    if (draggedSlot.stacklvl == 0)
                                    {
                                        weaponSupport.ClearSupportSlot(draggedSlot.item);
                                    }

                                    handled = true;
                                }
                                else
                                {
                                    MergeSlot(draggedSlot);
                                    Debug.Log("After merge: " + stacklvl);
                                    weaponSupport.EquipSupportItem(item, stacklvl, slotIndex);
                                    Debug.Log("EquipSupportItem called with stacklvl: " + stacklvl);
                                    handled = true;
                                }
                            }
                            else
                            {
                                MergeSlot(draggedSlot);
                                Debug.Log("After merge: " + stacklvl);
                                weaponSupport.EquipSupportItem(item, stacklvl, -1);
                                Debug.Log("EquipSupportItem called with stacklvl: " + stacklvl);
                                handled = true;
                            }
                        }
                    }
                }

                if (!handled)
                {
                    // ถ้าไม่ได้จัดการตามเงื่อนไขด้านบน ทำการสลับตำแหน่ง
                    SwapSlot(draggedSlot);
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
        if (item == null || mergeSlot.item == null)
        {
            Debug.LogError("Item or mergeSlot item is null!");
            return;
        }

        SO_Item overwrittenItem = mergeSlot.item;
        int overwrittenStackLevel = mergeSlot.stacklvl;

        int totalAmount = stacklvl + mergeSlot.stacklvl;
        int maxLevel = item.MaxLevel;

        stacklvl = Mathf.Clamp(totalAmount, 0, maxLevel);
        mergeSlot.stacklvl = totalAmount - stacklvl;

        CheckShowText();
        mergeSlot.CheckShowText();

        if (mergeSlot.stacklvl == 0)
        {
            Inventory.RemoveItem(mergeSlot);

            if (weaponSupport != null)
            {
                for (int i = 0; i < weaponSupport.supportSlots.Length; i++)
                {
                    if (weaponSupport.supportSlots[i].itemData != null && weaponSupport.supportSlots[i].itemData.id == overwrittenItem.id)
                    {
                        weaponSupport.supportSlots[i] = new WeaponSupport.Data_Item(0, null); // เคลียร์ข้อมูลในช่อง
                        weaponSupport.ClearSupportSlot(overwrittenItem); // เคลียร์ไอคอนในช่อง
                        break;
                    }
                }

                weaponSupport.UpdateSupportSlots();
            }
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
    string GetItemTypeName(TypeWeapon type)
    {
        switch (type)
        {
            case TypeWeapon.Main:
                return "M";
            case TypeWeapon.Support:
                return "S";
            default:
                return "U";
        }
    }

    public void CheckShowText()
    {
        Type.text = GetItemTypeName(item.type);
        stacklvltext.text = stacklvl.ToString();
        if (item.MaxLevel < 2)
        {
            stacklvltext.gameObject.SetActive(false);
            Type.gameObject.SetActive(false);
        }
        else
        {
            stacklvltext.gameObject.SetActive(stacklvl > 1);
            Type.gameObject.SetActive(true);
        }
    }
}
