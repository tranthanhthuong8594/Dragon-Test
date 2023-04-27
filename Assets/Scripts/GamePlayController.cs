using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoSingleton<GamePlayController>
{
    public bool endGame;

    int Score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Text endScoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] GameObject pnlEndGame;

    [SerializeField] Button btnRestart;
    protected override void Awake()
    {
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        endGame = false;
        pnlEndGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(endGame == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
            }
        }
    }

    public void GetScore()
    {
        Score++;
        scoreText.text = "Score: " + Score.ToString();
    }

    public void EndGame()
    {
        pnlEndGame.SetActive(true);
        endGame = true;
        Time.timeScale = 1;
        AudioController.Instance.SetGameoverClip();
        AudioController.Instance.AudioSource.Play();
        
        
        endScoreText.text = "Score: " + Score.ToString();

        if (Score > GameManager.Instance._GetHighScore())
        {
            GameManager.Instance._SetHighScore(Score);
        }
        highScoreText.text = "High Score: " + GameManager.Instance._GetHighScore().ToString();
    }

    public void Restart()
    {
        GameManager.Instance._SetAudio(GameManager.Instance._audio);
        SceneManager.LoadScene("GamePlay");
    }

    public void _Quit()
    {
        Application.Quit();
    }

    public void _Rate()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}
