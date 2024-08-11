using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class scene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public string nextSceneName;

    void Start() {
        // ตั้งค่า VideoPlayer ให้แสดงผลใน RenderTexture
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        // ลงทะเบียน event handler เมื่อวิดีโอเล่นเสร็จ
        videoPlayer.loopPointReached += OnVideoFinished;

        // เริ่มเล่นวิดีโอ
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp) {
        // เปลี่ยนไปยังฉากถัดไปเมื่อวิดีโอเล่นเสร็จ
        SceneManager.LoadScene(nextSceneName);
    }
}
