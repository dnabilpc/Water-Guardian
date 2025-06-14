using UnityEngine;

public class AudioManager : MonoBehaviour
{
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


    private void Start()
    {
        musicSource.clip = bgmMainMenu;
        musicSource.Play();
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
}
