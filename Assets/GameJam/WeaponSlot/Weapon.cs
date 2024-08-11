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

    public Data_Item[] weaponSlots = new Data_Item[1];

    [Header("Main_Slot UI")]
    public Image mainSlotIcon;

    [Header("Main_Weapon_Transform")]
    public Transform mainSlotTransform;

    private void Start()
    {
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
        int slotIndex = 0;

        if (weaponSlots[slotIndex].itemData != null)
        {
            Transform oldWeaponTransform = mainSlotTransform.Find(weaponSlots[slotIndex].itemData.itemName);
            if (oldWeaponTransform != null)
            {
                Destroy(oldWeaponTransform.gameObject);
            }
        }

        weaponSlots[slotIndex] = new Data_Item(lvl, weaponItem);
        ClearOldWeaponGameObject();

        if (weaponItem.gamePrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponItem.gamePrefab, mainSlotTransform.position, mainSlotTransform.rotation);
            weaponInstance.transform.SetParent(mainSlotTransform, false);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.name = weaponItem.itemName;
        }

        UpdateMainSlotIcon();
    }

    private void ClearOldWeaponGameObject()
    {
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
                mainSlotIcon.sprite = currentItem.icon;
                mainSlotIcon.enabled = true;
            }
            else
            {
                mainSlotIcon.enabled = false;
            }
        }
    }

    public float GetDamage()
    {
        if (weaponSlots.Length > 0)
        {
            SO_Item item = weaponSlots[0].itemData;
            int stackLevel = weaponSlots[0].lvl;

            if (item != null)
            {
                float baseDamage = item.Damgae;
                float value = CalculateValue(item.rarity, stackLevel);
                float valueRarity = CalculateValueRarity(item.rarity, stackLevel);
                return Mathf.RoundToInt(baseDamage * (value + valueRarity * stackLevel));
            }
        }
        return 0;
    }

    private float CalculateValue(Rarity rarity, int stacklvl)
    {
        switch (rarity)
        {
            case Rarity.Common: return 1.25f;
            case Rarity.Uncommon: return 2f;
            case Rarity.Rare: return 5f;
            case Rarity.Epic: return 10f;
            case Rarity.Legendary: return 50f;
            default: return 0f;
        }
    }

    private float CalculateValueRarity(Rarity rarity, int stacklvl)
    {
        switch (rarity)
        {
            case Rarity.Common: return 0.25f;
            case Rarity.Uncommon: return 0.5f;
            case Rarity.Rare: return 1.25f;
            case Rarity.Epic: return 2.5f;
            case Rarity.Legendary: return 2f;
            default: return 0f;
        }
    }
}
