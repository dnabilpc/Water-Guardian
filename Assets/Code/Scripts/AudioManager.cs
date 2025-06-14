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
    public void StopVoiceOver(){
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
}
