using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip overworldMusicClip;
    [SerializeField] private AudioClip mainMenuMusicClip;

    private AudioSource audioSource;

    public static MusicManager Instance;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    private void Start() {

        if (SavingSystem.Instance.GetSceneIsDungeon()) {
            audioSource.clip = DungeonManager.Instance.GetDungeonSO().dungonAudioClip;
            audioSource.Play();
            Debug.Log(DungeonManager.Instance.GetDungeonSO().dungonAudioClip);
        }

        if (SavingSystem.Instance.GetSceneIsOverworld()) {
            audioSource.clip = overworldMusicClip;
            audioSource.Play();
        }

        if (SavingSystem.Instance.GetSceneIsMainMenu()) {
            audioSource.clip = mainMenuMusicClip;
            audioSource.Play();
        }

        SetVolume(SoundManager.Instance.GetMusicVolume());
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
