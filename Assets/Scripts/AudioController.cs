using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] AudioSource audioSource;
    public AudioSource AudioSource => audioSource;

    [SerializeField] AudioClip flyClip;
    [SerializeField] AudioClip gameOverClip;

    [SerializeField] Button btnAudio;
    [SerializeField] Sprite onAudio;
    [SerializeField] Sprite offAudio;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = flyClip;
        if (GameManager.Instance._audio == 0)
        {
            audioSource.mute = false;
            btnAudio.GetComponent<Image>().sprite = onAudio;
        }
        else
        {
            audioSource.mute = true;
            btnAudio.GetComponent<Image>().sprite = offAudio;
        }
    }

    public void SetGameoverClip()
    {
        audioSource.clip = gameOverClip;
    }

    public void _AudioButton()
    {
        audioSource.mute = !audioSource.mute;
        if (audioSource.mute)
        {
            GameManager.Instance._audio = 1;
            btnAudio.GetComponent<Image>().sprite = offAudio;
        }
        else
        {
            GameManager.Instance._audio = 0;
            btnAudio.GetComponent<Image>().sprite = onAudio;
        }
    }
}
