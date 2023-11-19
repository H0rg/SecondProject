using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _panelPause;

    private bool gamePaused = false;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(Resume);
        _menuButton.onClick.AddListener(BackToMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        _panelPause.SetActive(true);
        _player.GetComponent<Movement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        _panelPause.SetActive(false);
        _player.GetComponent<Movement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void BackToMenu()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene("Menu");
    }
}
