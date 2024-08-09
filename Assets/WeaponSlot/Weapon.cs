using System.Collections.Generic;
using UnityEngine;

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

    public void EquipWeapon(SO_Item weaponItem, int lvl)
    {
        int slotIndex = 0;

        if (weaponSlots[slotIndex].itemData != null)
        {
            GameObject oldWeaponInstance = transform.GetChild(slotIndex).gameObject;
            if (oldWeaponInstance != null)
            {
                Destroy(oldWeaponInstance);
            }
        }
        weaponSlots[slotIndex] = new Data_Item(lvl, weaponItem);

        if (weaponItem.gamePrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponItem.gamePrefab, transform.position, transform.rotation);
            weaponInstance.transform.parent = transform;
        }
    }
}