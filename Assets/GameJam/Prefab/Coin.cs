using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin Instance;
    public int Coins; // จำนวนเหรียญที่ผู้เล่นมี
    public int StartCoin;

    public GameObject Pannel;
    public GameObject Inv;

    public TextMeshProUGUI main;
    public TextMeshProUGUI support;
    public TextMeshProUGUI T;
    public GameObject UI_Stat_Pannel;

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
        InitializeStats();
        UpdateCoinUI();
    }
    private void InitializeStats()
    {
        Coins = StartCoin;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            T.gameObject.SetActive(false);
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
        UI_Stat_Pannel.SetActive(false);
        main.gameObject.SetActive(true);
        support.gameObject.SetActive(true);
        Inv.gameObject.SetActive(false);
        Pannel.SetActive(true);
        Time.timeScale = 0f;  // หยุดเวลา
        isPaused = true;
    }
    public void ResumeGame()
    {
        UI_Stat_Pannel.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        support.gameObject.SetActive(false);
        Inv.gameObject.SetActive(true);
        Pannel.SetActive(false);
        Time.timeScale = 1f;  // เริ่มเวลาใหม่
        isPaused = false;
    }
}
