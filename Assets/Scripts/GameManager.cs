using FlipSprite;
using UI;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    
    public IScore Score { get; set; }
    [SerializeField] 
    private MainUI _mainUI;

    public MainUI GetMainUI => _mainUI;

    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private AudioMixerGroup _audioMixerGroup;
    private AudioSource _audioSource;
    public GameManager()
    {
        Score = new Score();
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _audioMixerGroup;
    }
    public void PlayCoinSound()
    {
        _audioSource.PlayOneShot(_audioClip);

    }
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        

    }
}