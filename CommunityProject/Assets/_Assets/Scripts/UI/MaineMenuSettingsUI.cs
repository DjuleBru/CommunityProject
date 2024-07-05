using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaineMenuSettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsGameObject;
    [SerializeField] private Button backButton;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float musicVolume;
    private float sfxVolume;

    private void Awake() {
        backButton.onClick.AddListener(() => {
            settingsGameObject.gameObject.SetActive(false);
        });
    }

    private void Start() {
        musicVolume = SoundManager.Instance.GetMusicVolume();
        sfxVolume = SoundManager.Instance.GetSFXVolume();
        SoundManager.Instance.SetSFXVolume(sfxVolume);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        musicSlider.onValueChanged.AddListener((v) => {
            musicVolume = v;
            SoundManager.Instance.SetMusicVolume(musicVolume);
        });

        sfxSlider.onValueChanged.AddListener((v) => {
            sfxVolume = v;

            SoundManager.Instance.SetSFXVolume(sfxVolume);
        });
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            settingsGameObject.gameObject.SetActive(false);
        }
    }

}
