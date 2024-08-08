using UnityEngine;

public class ChestRandom : MonoBehaviour
{
    public WeightedRandomList<GameObject> lootTable;

    public Transform itemHolder;
    public bool CanOpen = true;
    public GameObject GetButton;
    public GameObject This_Item;

    private void Start()
    {
        itemHolder = transform.Find("Item_Holder");
        GetButton = transform.Find("Get_Button").gameObject;
    }

    private void Update()
    {
       
    }
    public void OpenChest()
    {
        if (CanOpen)
        {
            Debug.Log("open");
            CanOpen = false;
            GetButton.SetActive(false);
            ShowItem();
            this.enabled = false;
        }
    }  
    void ShowItem()
    {
        itemHolder.localScale = Vector3.one;
        GameObject item = lootTable.GetRandom();
        This_Item = Instantiate(item, itemHolder);
    }
}


