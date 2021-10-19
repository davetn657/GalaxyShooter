using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameover;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _isGameover = true;
    }
}
