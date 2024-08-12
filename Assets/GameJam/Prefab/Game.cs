using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game _instance;
    public float Health;
    public float MaxHealth;
    public int AttackPower;
    public int Experience;
    public int Level;
    private int experienceForNextLevel; // ทำให้เป็น private

    [Header("UI")]
    public TextMeshProUGUI hp;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI exp;

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
        UpdateUI(); // อัปเดต UI ทั้งหมดเมื่อเริ่มต้น
    }

    private void UpdateUI()
    {
        UpdateHealthUI();
        UpdateUILevel();
        UpdateUIExp();
    }

    private void UpdateHealthUI()
    {
        if (hp != null)
        {
            hp.text = $"HP: {Health}/{MaxHealth}"; // อัปเดตข้อความให้แสดงค่า HP ปัจจุบัน
        }
    }

    private void UpdateUILevel()
    {
        if (lvl != null)
        {
            lvl.text = $"Level: {Level}"; // อัปเดตข้อความให้แสดงระดับปัจจุบัน
        }
    }

    private void UpdateUIExp()
    {
        if (exp != null)
        {
            exp.text = $"Experience: {Experience}/{experienceForNextLevel}"; // อัปเดตข้อความให้แสดงค่าประสบการณ์ปัจจุบัน
        }
    }

    private void InitializeStats()
    {
        // กำหนดค่าเริ่มต้นให้กับสถิติของผู้เล่น
        MaxHealth = 200;
        Health = MaxHealth;
        AttackPower = 10;
        Experience = 0;
        Level = 1;
        experienceForNextLevel = CalculateExperienceForNextLevel();
    }

    // คำนวณค่าประสบการณ์ที่จำเป็นสำหรับการเลื่อนระดับ
    private int CalculateExperienceForNextLevel()
    {
        return Level * 100;
    }

    // เพิ่มค่าประสบการณ์
    public void GainExperience(int amount)
    {
        Experience += amount;
        CheckLevelUp();
        UpdateUIExp(); // ตรวจสอบว่าเรียกเมธอดนี้หลังจากการเพิ่มประสบการณ์
    }

    // ฟังก์ชันสำหรับการตรวจสอบระดับที่เพิ่มขึ้น
    private void CheckLevelUp()
    {
        // ตรวจสอบว่าค่าประสบการณ์เกินกว่าที่กำหนดสำหรับระดับถัดไปหรือไม่
        while (Experience >= experienceForNextLevel)
        {
            Level++;
            Experience -= experienceForNextLevel;
            LevelUp();
        }
        UpdateUIExp(); // อัปเดต UI ประสบการณ์หลังจากตรวจสอบการเลื่อนระดับ
    }

    // จัดการการเลื่อนระดับ
    private void LevelUp()
    {
        MaxHealth += 50;
        Health = MaxHealth;
        AttackPower += 5;
        experienceForNextLevel = CalculateExperienceForNextLevel(); // อัปเดตค่าประสบการณ์ที่จำเป็นสำหรับระดับถัดไป
        UpdateUI(); // อัปเดต UI ทั้งหมดหลังจากเลื่อนระดับ
    }

    // ลด HP
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Max(Health, 0); // ตรวจสอบให้มั่นใจว่า HP ไม่ต่ำกว่า 0
        UpdateHealthUI();

        if (Health <= 0)
        {
            Die();
        }
    }

    // ฟื้นฟู HP
    public void Heal(float amount)
    {
        Health += amount;
        Health = Mathf.Min(Health, MaxHealth); // ตรวจสอบให้มั่นใจว่า HP ไม่เกิน MaxHealth
        UpdateHealthUI();
    }

    // จัดการเมื่อผู้เล่นตาย
    private void Die()
    {
        RestartGame();
    }

    // เริ่มเกมใหม่
    private void RestartGame()
    {
        SceneManager.LoadScene(3);
    }
}
