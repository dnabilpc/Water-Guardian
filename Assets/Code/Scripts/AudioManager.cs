using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider gameVolumeSlider;
    public static AudioManager Instance { get; private set; }
    [Header("-----Audio Sources-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clips-----")]
    public AudioClip bgmMainMenu;
    public AudioClip bgmGamePlay;
    public AudioClip bgmCutscene;
    public AudioClip voiceOver;
    public AudioClip hitSound;
    public AudioClip gameWonSound; // Audio untuk game won
    public AudioClip gameOverSound; // Audio untuk game over



    private bool isLoading = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume") || !PlayerPrefs.HasKey("gameVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            PlayerPrefs.SetFloat("gameVolume", 1f);
        }
        else
        {
            Load();
        }
        musicSource.clip = bgmMainMenu;
        musicSource.Play();

        StartCoroutine(LoadAfterFrame());
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private System.Collections.IEnumerator LoadAfterFrame()
    {
        yield return null;
        Load();
    }
    public void PlayVoiceOver()
    {
        SFXSource.clip = voiceOver;
        SFXSource.Play();
    }
    public void StopVoiceOver()
    {
        if (SFXSource.isPlaying && SFXSource.clip == voiceOver)
        {
            SFXSource.Stop();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
    public void PlayHitSound()
    {
        if (hitSound != null)
        {
            PlaySFX(hitSound);
        }
        else
        {
            Debug.LogWarning("Hit sound clip is not assigned!");
        }
    }

    public void ChangeMusicVolume()
    {
        if (!isLoading)
        {
            musicSource.volume = musicVolumeSlider.value;
            Save();
        }
    }
    public void ChangeGameVolume()
    {
        if (!isLoading)
        {
            SFXSource.volume = gameVolumeSlider.value;
            Save();
        }
    }

    public void PlayGameWonAudio()
    {
        if (gameWonSound != null)
        {
            PlaySFX(gameWonSound);
        }
        else
        {
            Debug.LogWarning("Game won sound clip is not assigned!");
        }
    }

    public void PlayGameOverAudio()
    {
        if (gameOverSound != null)
        {
            PlaySFX(gameOverSound);
        }
        else
        {
            Debug.LogWarning("Game over sound clip is not assigned!");
        }
    }



    private void Load()
    {
        isLoading = true;

        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        gameVolumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        SFXSource.volume = PlayerPrefs.GetFloat("gameVolume");

        isLoading = false;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("gameVolume", gameVolumeSlider.value);
    }


}
