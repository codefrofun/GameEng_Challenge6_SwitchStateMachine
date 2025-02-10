using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Gameobject with pause UI message
    public GameObject pauseMenuUI;
    // Gameobject with main menu UI message
    public GameObject mainMenuUI;
    // Gameobject with gamplay UI message
    public GameObject gameplayUI;

    // Bool to freeze gameplay
    private bool isPaused = false;

    public enum GameState
    { 
        MainMenu_State, Gameplay_State, Paused_State
    }

    public GameState currentState { get; private set; }

    [SerializeField] private string currentStateDebug;
    [SerializeField] private string lastStateDebug;

    private void Start()
    {
        ChangeState(GameState.MainMenu_State);
    }

    public void ChangeState(GameState newState)
    {
        lastStateDebug = currentState.ToString();

        currentState = newState;

        HandleStateChange(newState);

        currentStateDebug = currentState.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeState(GameState.MainMenu_State);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Gameplay_State || currentState == GameState.Paused_State)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    ChangeState(GameState.Paused_State);
                    Pause();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeState(GameState.Gameplay_State);
        }
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu_State:

                mainMenuUI.SetActive(true);
                gameplayUI.SetActive(false);
                pauseMenuUI.SetActive(false);
                Debug.Log("Switched to Main Menu screen");


                break;

            case GameState.Gameplay_State:

                mainMenuUI.SetActive(false);
                gameplayUI.SetActive(true);
                pauseMenuUI.SetActive(false);
                Debug.Log("Switched to Gameplay screen");


                break;

            case GameState.Paused_State:

                mainMenuUI.SetActive(false);
                gameplayUI.SetActive(false);
                Debug.Log("Switched to Pause screen");


                break;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
