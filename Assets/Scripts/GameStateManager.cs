using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameManager gameManager;

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
        if (Input.GetKeyDown(KeyCode.M) && currentState == GameState.Gameplay_State)
        {
            ChangeStateToGameplay();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Gameplay_State)
            {
                Pause();
            }
            else if (currentState == GameState.Paused_State)
            {
                Resume();
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && currentState == GameState.MainMenu_State)
        {
            ChangeStateToGameplay();
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
                pauseMenuUI.SetActive(true);
                Debug.Log("Switched to Pause screen");
                break;

        }
    }

    public void ChangeStateToMainMenu()
    {
        ChangeState(GameState.MainMenu_State);
        gameManager.uiManager.EnableMainMenu();
    }

    public void Resume()
    {
        ChangeState(GameState.Gameplay_State);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        ChangeState(GameState.Paused_State);
        Time.timeScale = 0f;
    }

    public void ChangeStateToGameplay()
    {
        ChangeState(GameState.Gameplay_State);
    }
}