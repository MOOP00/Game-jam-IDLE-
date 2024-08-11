using UnityEngine;

public class GameStart : MonoBehaviour
{
    public AudioClip themeMusic;  // เสียงธีมที่ต้องการเล่น

    void Start()
    {
        SoundManager.instance.PlayTheme(themeMusic);
    }
}