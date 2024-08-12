using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game _instance;
    public float Health;
    public float MaxHealth;
    public int AttackPower;
    public int Defense;
    public int Experience;
    public int Level;

    [Header("UI")]
    public TextMeshProUGUI hp;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializeStats();
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        if (hp != null)
        {
            hp.text = $"HP: {Health}/{MaxHealth}"; // Update the text with the current health
        }
    }
    private void InitializeStats()
    {
        // กำหนดค่าเริ่มต้นให้กับสถิติของผู้เล่น
        MaxHealth = 300;
        Health = MaxHealth;
        AttackPower = 10;
        Defense = 5;
        Experience = 0;
        Level = 1;
    }

    // ฟังก์ชันสำหรับการเพิ่มค่าประสบการณ์
    public void GainExperience(int amount)
    {
        Experience += amount;
        CheckLevelUp();
    }

    // ฟังก์ชันสำหรับการตรวจสอบระดับที่เพิ่มขึ้น
    private void CheckLevelUp()
    {
        // กำหนดค่าประสบการณ์ที่จำเป็นในการเพิ่มระดับ
        int experienceForNextLevel = Level * 100;

        // ตรวจสอบว่าค่าประสบการณ์เกินกว่าที่กำหนด
        if (Experience >= experienceForNextLevel)
        {
            Level++;
            Experience -= experienceForNextLevel;
            LevelUp();
        }
    }

    // ฟังก์ชันสำหรับการเพิ่มระดับ
    private void LevelUp()
    {
        MaxHealth += 50;
        Health = MaxHealth;
        AttackPower += 5;
        Defense += 3;
        UpdateHealthUI();

        Debug.Log("Level Up! Current Level: " + Level);
    }

    // ฟังก์ชันสำหรับการลดค่า HP
    public void TakeDamage(float damage)
    {
        float actualDamage = Mathf.Max(damage - Defense, 0); // คำนวณความเสียหายที่ลดลงจาก Defense
        Debug.Log(actualDamage);
        Health -= actualDamage;
        Health = Mathf.Max(Health, 0); // ไม่ให้ค่าต่ำกว่า 0
        UpdateHealthUI();

        if (Health <= 0)
        {
            UpdateHealthUI();
            Die();
        }
    }

    // ฟังก์ชันสำหรับการรักษา HP
    public void Heal(float amount)
    {
        Health += amount;
        Health = Mathf.Min(Health, MaxHealth); // ไม่ให้ค่าเกิน MaxHealth
        UpdateHealthUI();
    }

    // ฟังก์ชันเมื่อผู้เล่นตาย
    private void Die()
    {
        Debug.Log("Player Died!");
        // เพิ่มเติม: ใส่การจัดการเมื่อผู้เล่นตาย เช่น การโหลดฉากใหม่หรือรีเซ็ตเกม
    }
}
