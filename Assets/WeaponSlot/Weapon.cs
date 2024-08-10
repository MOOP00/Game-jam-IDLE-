using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
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

    public Data_Item[] weaponSlots = new Data_Item[1]; // Ensure this array size fits your use case

    [Header("Main_Slot UI")]
    public Image mainSlotIcon; // Reference to the Image component in the UI

    private void Start()
    {
        // Initialize UI icon visibility
        UpdateMainSlotIcon();
    }

    public bool HasWeaponWithName(string weaponName)
    {
        foreach (var weapon in weaponSlots)
        {
            if (weapon.itemData != null && weapon.itemData.itemName == weaponName)
            {
                return true;
            }
        }
        return false;
    }

    public void EquipWeapon(SO_Item weaponItem, int lvl)
    {
        int slotIndex = 0; // Assuming single slot for simplicity, adjust as needed

        // Clear the existing weapon if any
        if (weaponSlots[slotIndex].itemData != null)
        {
            GameObject oldWeaponInstance = transform.GetChild(slotIndex).gameObject;
            if (oldWeaponInstance != null)
            {
                Destroy(oldWeaponInstance);
            }
        }

        // Equip the new weapon
        weaponSlots[slotIndex] = new Data_Item(lvl, weaponItem);

        if (weaponItem.gamePrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponItem.gamePrefab, transform.position, transform.rotation);
            weaponInstance.transform.parent = transform;
            weaponInstance.transform.localPosition = Vector3.zero; // Ensure it is properly positioned
        }

        // Update the UI icon
        UpdateMainSlotIcon();
    }

    private void UpdateMainSlotIcon()
    {
        if (weaponSlots.Length > 0)
        {
            SO_Item currentItem = weaponSlots[0].itemData;

            if (currentItem != null)
            {
                mainSlotIcon.sprite = currentItem.icon; // Update the icon with the currently equipped weapon's icon
                mainSlotIcon.enabled = true; // Ensure the icon is visible
            }
            else
            {
                mainSlotIcon.enabled = false; // Hide the icon if no weapon is equipped
            }
        }
    }
}
