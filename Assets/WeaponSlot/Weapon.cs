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

    [Header("Main_Weapon_Transform")]
    public Transform mainSlotTransform;

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
            Transform oldWeaponTransform = mainSlotTransform.Find(weaponSlots[slotIndex].itemData.itemName);
            if (oldWeaponTransform != null)
            {
                Destroy(oldWeaponTransform.gameObject);
            }
        }

        // Equip the new weapon
        weaponSlots[slotIndex] = new Data_Item(lvl, weaponItem);

        // Clear the old weapon GameObject
        ClearOldWeaponGameObject();

        if (weaponItem.gamePrefab != null)
        {
            // Instantiate the new weapon prefab
            GameObject weaponInstance = Instantiate(weaponItem.gamePrefab, mainSlotTransform.position, mainSlotTransform.rotation);

            // Set the new weapon as a child of mainSlotTransform
            weaponInstance.transform.SetParent(mainSlotTransform, false);

            // Ensure the weapon is positioned correctly within the parent
            weaponInstance.transform.localPosition = Vector3.zero;

            // Set the name of the weapon instance to match the item name
            weaponInstance.name = weaponItem.itemName;
        }

        // Update the UI icon
        UpdateMainSlotIcon();
    }

    private void ClearOldWeaponGameObject()
    {
        // Destroy all children of the mainSlotTransform
        foreach (Transform child in mainSlotTransform)
        {
            Destroy(child.gameObject);
        }
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
