using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundscene : MonoBehaviour
{
    public void tosoundscene() {
        SceneManager.LoadSceneAsync("soundscene1");
    }

    public void toaudiosettings() {
        SceneManager.LoadSceneAsync("audiosettings");
    }
    public void tomainmenu() {
        SceneManager.LoadSceneAsync("title");
    }

}
