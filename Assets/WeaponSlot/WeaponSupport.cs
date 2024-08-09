using UnityEngine;

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

    public Data_Item[] supportSlots = new Data_Item[4];

    public void EquipSupportItem(SO_Item supportItem, int lvl, int slotIndex)
    {
        for (int i = 0; i < supportSlots.Length; i++)
        {
            if (supportSlots[i].itemData == supportItem)
            {
                supportSlots[i] = new Data_Item(0, null); // ลบ itemData ที่ซ้ำกันออก
                break;
            }
        }

        // ลบ GameObject ที่มีชื่อเดียวกันออกจาก WeaponSupport
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == supportItem.gamePrefab.name)
            {
                Destroy(child.gameObject); // ลบ GameObject ที่มีชื่อซ้ำ
                break;
            }
        }

        // เพิ่ม itemData ใหม่
        supportSlots[slotIndex] = new Data_Item(lvl, supportItem);

        GameObject supportInstance = Instantiate(supportItem.gamePrefab, transform.position, transform.rotation);
        supportInstance.transform.parent = transform;
    }
}
