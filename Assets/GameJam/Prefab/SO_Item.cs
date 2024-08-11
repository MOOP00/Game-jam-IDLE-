using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/SO_Item")]
public class SO_Item : ScriptableObject
{
    public Sprite icon;
    public string id;
    public string itemName;
    public string description;
    public float Damgae;
    public int MaxLevel;
    public Rarity rarity;
    public TypeWeapon type;

    [Header("Prefab")]
    public GameObject gamePrefab;

    public SO_Item Clone()
    {
        return Instantiate(this);
    }
}

public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
public enum TypeWeapon { Main, Support }
