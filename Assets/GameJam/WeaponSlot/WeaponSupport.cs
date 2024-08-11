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

    [Header("Transform Points")]
    public Transform transformPoint1;
    public Transform transformPoint2;
    public Transform transformPoint3;
    public Transform transformPoint4;

    [Header("Prefabs")]
    public GameObject[] itemPrefabs; // Array of prefabs corresponding to each slot

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

        // Remove existing item if it matches the current item ID
        for (int i = 0; i < supportSlots.Length; i++)
        {
            if (supportSlots[i].itemData != null && supportSlots[i].itemData.id == item.id)
            {
                supportSlots[i] = new Data_Item(0, null);
                ClearSlotIcon(i);
                RemoveExistingPrefab(i);
            }
        }

        // Assign new item or find an empty slot
        if (slotIndex >= 0 && slotIndex < supportSlots.Length)
        {
            supportSlots[slotIndex] = new Data_Item(stacklvl, item);
            PlaceItemPrefab(item, slotIndex);
        }
        else
        {
            for (int i = 0; i < supportSlots.Length; i++)
            {
                if (supportSlots[i].itemData == null)
                {
                    supportSlots[i] = new Data_Item(stacklvl, item);
                    PlaceItemPrefab(item, i);
                    break;
                }
            }
        }

        UpdateSupportSlots();
    }

    public void RemoveExistingPrefab(int slotIndex)
    {
        Transform targetTransform = GetTransformForSlot(slotIndex);
        if (targetTransform != null && targetTransform.childCount > 0)
        {
            // Remove the last child as an example; modify as needed to match your use case
            GameObject lastChild = targetTransform.GetChild(targetTransform.childCount - 1).gameObject;
            Destroy(lastChild);
        }
    }

    public void PlaceItemPrefab(SO_Item item, int slotIndex)
    {
        if (item.gamePrefab != null)
        {
            Transform targetTransform = GetTransformForSlot(slotIndex);
            if (targetTransform != null)
            {
                GameObject prefabInstance = Instantiate(item.gamePrefab, targetTransform.position, targetTransform.rotation);
                prefabInstance.transform.SetParent(targetTransform, false); // Set as child without using parent's local scale
                prefabInstance.transform.localPosition = Vector3.zero;

                if (targetTransform.childCount > 1)
                {
                    Destroy(targetTransform.GetChild(0).gameObject);
                }
            }
        }
    }

    public Transform GetTransformForSlot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0: return transformPoint1;
            case 1: return transformPoint2;
            case 2: return transformPoint3;
            case 3: return transformPoint4;
            default: return null;
        }
    }

    private void ClearItemPrefab(int slotIndex)
    {
        Transform targetTransform = GetTransformForSlot(slotIndex);
        if (targetTransform != null)
        {
            foreach (Transform child in targetTransform)
            {
                Destroy(child.gameObject);
            }
        }
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
            ClearSlotIcon(index);
        }
    }

    public void ClearSupportSlot(SO_Item item)
    {
        for (int i = 0; i < supportSlots.Length; i++)
        {
            if (supportSlots[i].itemData != null && supportSlots[i].itemData.id == item.id)
            {
                supportSlots[i] = new Data_Item(0, null);
                ClearSlotIcon(i);
                ClearItemPrefab(i);
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
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
