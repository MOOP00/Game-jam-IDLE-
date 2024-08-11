using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SupportSlot : MonoBehaviour
{
    public SO_Item itemData;
    public int stacklvl;
    public Image icon;
    public TextMeshProUGUI stacklvlText;

    // อัปเดต UI ของช่อง Support Slot
    public void UpdateUI()
    {
        if (itemData != null)
        {
            icon.sprite = itemData.icon;
            stacklvlText.text = stacklvl.ToString();
            stacklvlText.gameObject.SetActive(stacklvl > 1);
        }
    }

    // ล้างช่องให้ว่าง
    public void ClearSlot()
    {
        itemData = null;
        stacklvl = 0;
        icon.sprite = null;
        stacklvlText.text = "";
        stacklvlText.gameObject.SetActive(false);
    }
}
