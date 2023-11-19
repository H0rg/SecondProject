using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    void Awake()
    {
        _playButton.onClick.AddListener(PlayGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level1");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
