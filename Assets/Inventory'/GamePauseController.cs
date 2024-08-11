using UnityEngine;
using UnityEngine.UI;

public class GamePauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;  // ตัวแปรเก็บ UI ที่จะปรากฏเมื่อหยุดเกม
    public GameObject Inv;
    private bool isPaused = false;  // ตัวแปรเพื่อเช็คสถานะการหยุดเกม

    void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);  // ซ่อน pause menu เมื่อเริ่มต้นเกม
        }
    }
    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);  
        }
        Time.timeScale = 1f;  
        isPaused = false;
    }

    public void PauseGame()
    {
        if (pauseMenuUI != null)
        {
            Inv.SetActive(false);
            pauseMenuUI.SetActive(true);   // แสดง pause menu
        }
        Time.timeScale = 0f;  // หยุดเวลา
        isPaused = true;
    }
}
