using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;  // AudioSource สำหรับเล่นเสียงเอฟเฟกต์
    public AudioSource themeSource;  // AudioSource สำหรับเล่นเสียงธีม

    private void Awake()
    {
        // ทำให้ SoundManager เป็น Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayTheme(AudioClip themeClip)
    {
        if (themeSource.isPlaying) return;  // ตรวจสอบว่าเพลงธีมกำลังเล่นอยู่หรือไม่
        themeSource.clip = themeClip;
        themeSource.loop = true;  // ให้เสียงธีมเล่นวนลูป
        themeSource.Play();
    }

    public void StopTheme()
    {
        themeSource.Stop();
    }
}