using UnityEngine;
using UnityEngine.UI;

public class WeaponSupport : MonoBehaviour
{
    [System.Serializable]
    public struct Data_Item
    {
        public SO_Item itemData;
        public int lvl;

        public Data_Item(int lvl, SO_Item itemData)
        {
            this.lvl = lvl;
            this.itemData = itemData;
        }
    }

    [Header("Support Slots")]
    public Data_Item[] supportSlots = new Data_Item[4];

    [Header("Support Slot Icons")]
    public Image supportSlotIcon1;
    public Image supportSlotIcon2;
    public Image supportSlotIcon3;
    public Image supportSlotIcon4;

    private void Start()
    {
        foreach (Image image in new Image[] { supportSlotIcon1, supportSlotIcon2, supportSlotIcon3, supportSlotIcon4 })
        {
            if (image != null)
            {
                image.enabled = false;
            }
        }
    }

    public void EquipSupportItem(SO_Item item, int stacklvl, int slotIndex)
    {
        if (item == null)
        {
            Debug.LogError("Item is null!");
            return;
        }

        // ลบไอเท็มที่มี id ตรงกันจากช่องทั้งหมดก่อน
        for (int i = 0; i < supportSlots.Length; i++)
        {
            if (supportSlots[i].itemData != null && supportSlots[i].itemData.id == item.id)
            {
                supportSlots[i] = new Data_Item(0, null); // เคลียร์ข้อมูลในช่องนี้
                ClearSlotIcon(i); // เคลียร์ไอคอน
            }
        }

        // ตั้งค่าไอเท็มใหม่ในช่องที่ระบุ
        if (slotIndex >= 0 && slotIndex < supportSlots.Length)
        {
            supportSlots[slotIndex] = new Data_Item(stacklvl, item);
        }
        else
        {
            // หาก slotIndex ไม่ถูกต้อง, หาช่องว่างที่ว่าง
            for (int i = 0; i < supportSlots.Length; i++)
            {
                if (supportSlots[i].itemData == null)
                {
                    supportSlots[i] = new Data_Item(stacklvl, item);
                    break;
                }
            }
        }

        UpdateSupportSlots(); // อัปเดตไอคอน
    }


    public void UpdateSupportSlots()
    {
        UpdateSupportSlotIcon(supportSlotIcon1, 0);
        UpdateSupportSlotIcon(supportSlotIcon2, 1);
        UpdateSupportSlotIcon(supportSlotIcon3, 2);
        UpdateSupportSlotIcon(supportSlotIcon4, 3);
    }

    private void UpdateSupportSlotIcon(Image icon, int index)
    {
        if (supportSlots[index].itemData != null)
        {
            icon.sprite = supportSlots[index].itemData.icon;
            icon.enabled = true;
        }
        else
        {
            ClearSlotIcon(index); // Clear image only, not the entire component
        }
    }

    public void ClearSupportSlot(SO_Item item)
    {
        for (int i = 0; i < supportSlots.Length; i++)
        {
            if (supportSlots[i].itemData != null && supportSlots[i].itemData.id == item.id)
            {
                supportSlots[i] = new Data_Item(0, null); // Clear slot data
                ClearSlotIcon(i); // Clear image only
                break;
            }
        }
    }


    public void ClearSlotIcon(int index)
    {
        switch (index)
        {
            case 0:
                ClearIcon(supportSlotIcon1);
                break;
            case 1:
                ClearIcon(supportSlotIcon2);
                break;
            case 2:
                ClearIcon(supportSlotIcon3);
                break;
            case 3:
                ClearIcon(supportSlotIcon4);
                break;
        }
    }

    public void ClearIcon(Image icon)
    {
        if (icon != null)
        {
            icon.sprite = null; // Remove image
            icon.enabled = false; // Optionally, hide the icon
        }
    }
}
