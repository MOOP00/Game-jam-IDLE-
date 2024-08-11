using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class menucontroller : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextvalue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;
    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    public string _newGamelevel;





    public void NewGameDialogYes() {
        SceneManager.LoadScene(_newGamelevel);
    }
    public void ExitButton() {
        Application.Quit();
    }
    public void SetVolume(float volume) {
        AudioListener.volume = volume;
        volumeTextvalue.text = volume.ToString("0.0");

    }

    public void VolumeApply() {
        PlayerPrefs.SetFloat("mastervolume", AudioListener.volume);
        StartCoroutine(Confirmationbox());
    }
    public void ResetButton(string MenuType) {
        if (MenuType == "Audio") {
            volumeSlider.value = defaultVolume;
            volumeTextvalue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
    public IEnumerator Confirmationbox() {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}
