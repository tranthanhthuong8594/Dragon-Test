using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private const string HIGH_SCORE = "High Score";
    private const string AUDIO = "Audio";

    public int _audio;

    protected override void Awake()
    {
        base.Awake();
        IsGameStartedForTheFirstTime();
        _audio = _GetAudio();
    }

    void IsGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }
    }

    public void _StartGame()
    {
        _SetAudio(_audio);
        SceneManager.LoadScene("GamePlay");
    }

    public void _SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int _GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void _SetAudio(int audio)
    {
        PlayerPrefs.SetInt(AUDIO, audio);
    }

    public int _GetAudio()
    {
        return PlayerPrefs.GetInt(AUDIO);
    }
}
