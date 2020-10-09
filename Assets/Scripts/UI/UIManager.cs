﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject setting;
    public GameObject settingForMouse;

    private Button[] buttons;

    private void Start()
    {
        

        if (GameHelper.isFirstStartMenu)
        {
            if (PlayerPrefs.HasKey("Save"))
                if (PlayerPrefs.GetInt("Save") == 0)
                    GameHelper.isChangeSetting = true;
                else
                    GameHelper.isChangeSetting = false;
            Pause();
            buttons = FindObjectsOfType<Button>();
            changeButton("Resume", false);
        }
    }

    void Update()
    {
        buttons = FindObjectsOfType<Button>();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameHelper.isPausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameHelper.isChangeSetting)
        {
            changeButton("Setting", false);
            changeButton("SettingForMouse", true);
        }
        else
        {
            changeButton("SettingForMouse", false);
            changeButton("Setting", true);
        }
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameHelper.isPausedGame = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameHelper.isPausedGame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameHelper.isPausedGame = false;
        GameHelper.isFirstStartMenu = false;
        GameHelper.isFirstStartPlayer = true;
        SceneManager.LoadScene("Game");
    }

    public void Setting()
    {
        GameHelper.isChangeSetting = !GameHelper.isChangeSetting;
        SetQuality();
    }

    private void changeButton(string name, bool change)
    {
        foreach (var item in buttons)
        {
            if (item.gameObject.name == name)
            {
                item.interactable = change;
            }
        }
    }
    public void SetQuality()
    {
        int save;
        if (GameHelper.isChangeSetting)
            save = 0;
        else
            save = 1;
        PlayerPrefs.SetInt("Save", save);
    }
}
