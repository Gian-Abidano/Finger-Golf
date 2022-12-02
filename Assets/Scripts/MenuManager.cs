using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSFX;
    private int musicValue;
    private int sfxValue;
    
    void Start()
    {
        sliderMusic.value = (float) PlayerPrefs.GetInt("musicValue", 100) / 100;
        sliderSFX.value = (float) PlayerPrefs.GetInt("sfxValue", 100) / 100;
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Changing to Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }

    public void QuitSettings()
    {
        Debug.Log("Setting Preferences Saved");
        PlayerPrefs.Save();
    }

    public void MusicVolumeChanged(float sliderValue)
    {
        musicValue = Mathf.FloorToInt(sliderValue * 100);
        PlayerPrefs.SetInt("musicValue", musicValue);
        Debug.Log("Volume BGM : " + musicValue);
    }

    public void SFXVolumeChanged(float sliderValue)
    {
        sfxValue = Mathf.FloorToInt(sliderValue * 100);
        PlayerPrefs.SetInt("sfxValue", sfxValue);
        Debug.Log("Volume SFX : " + sfxValue);
    }
}
