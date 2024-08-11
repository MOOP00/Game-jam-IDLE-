using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int Coins;  // จำนวนเหรียญที่ผู้เล่นมี
    public TextMeshProUGUI coinText;
    private void Awake()
    {
        UpdateCoinUI();
        // ตรวจสอบว่ามี instance ที่มีอยู่แล้วหรือไม่
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); // ทำลาย instance ซ้ำ
        }
    }
    // เพิ่มเหรียญ
    public void AddCoins(int amount)
    {
        if (amount < 0)
        {
            return;
        }
        Coins += amount;
        UpdateCoinUI();
    }
    public void SpendCoins(int amount)
    {
        Coins -= amount;
    }
    public int GetCoinBalance()
    {
        return Coins;
    }
    public void UpdateCoinUI()
    {
        // อัปเดตข้อความใน UI ด้วยจำนวนเงินปัจจุบันจาก CoinManager
        coinText.text = "Coins: " + CoinManager.Instance.Coins.ToString();
    }
}
