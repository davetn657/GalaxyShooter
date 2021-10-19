using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 00";
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }
    }

    public void NewScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
       _livesImage.sprite = _livesSprites[currentLives];
        if(currentLives < 1)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
