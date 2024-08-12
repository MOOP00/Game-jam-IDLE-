using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin Instance;
    public int Coins; // จำนวนเหรียญที่ผู้เล่นมี

    public GameObject Pannel;
    public GameObject Inv;

    public TextMeshProUGUI coinText;
    public bool isPaused;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateCoinUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
        UpdateCoinUI();
    }

    // ลบเหรียญ
    public void SpendCoins(int amount)
    {
        Coins -= amount;
        UpdateCoinUI();

    }
    public void UpdateCoinUI()
    {
        coinText.text = Coins.ToString();
    }
    public void PauseGame()
    {
        Inv.gameObject.SetActive(false);
        Pannel.SetActive(true);
        Time.timeScale = 0f;  // หยุดเวลา
        isPaused = true;
    }
    public void ResumeGame()
    {
        Inv.gameObject.SetActive(true);
        Pannel.SetActive(false);
        Time.timeScale = 1f;  // เริ่มเวลาใหม่
        isPaused = false;
    }
}
