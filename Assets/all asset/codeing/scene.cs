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
        // ��駤�� VideoPlayer ����ʴ���� RenderTexture
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        // ŧ����¹ event handler ������Դ����������
        videoPlayer.loopPointReached += OnVideoFinished;

        // ���������Դ���
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp) {
        // ����¹��ѧ�ҡ�Ѵ�������Դ����������
        SceneManager.LoadScene(nextSceneName);
    }
}
