using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameManager gameManager;

    // Bool to freeze gameplay
    private bool isPaused = false;

    public enum GameState
    { 
        // Different gamestates 
        MainMenu_State, Gameplay_State, Paused_State, Credit_State
    }

    public GameState currentState { get; private set; }

    [SerializeField] private string currentStateDebug;
    [SerializeField] private string lastStateDebug;

    private void Start()
    {
        // On start, show main menu
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

        if (Input.GetKeyDown(KeyCode.C) && currentState == GameState.MainMenu_State)
        {
            ChangeStateToCredit();
        }
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu_State:
                Time.timeScale = 0f;
                gameManager.uiManager.EnableMainMenu();
                Debug.Log("Switched to Main Menu screen");
                break;

            case GameState.Gameplay_State:
                Time.timeScale = 1f;
                gameManager.uiManager.EnableGameplay();
                Debug.Log("Switched to Gameplay screen");
                break;

            case GameState.Paused_State:
                Time.timeScale = 0f;
                gameManager.uiManager.EnablePause();
                Debug.Log("Switched to Pause screen");
                break;

            case GameState.Credit_State:
                Time.timeScale = 0f;
                gameManager.uiManager.EnableCredits();
                Debug.Log("Switched to Main Menu screen");
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
    }

    public void ChangeStateToGameplay()
    {
        ChangeState(GameState.Gameplay_State);
    }

    public void ChangeStateToCredit()
    {
        ChangeState(GameState.Credit_State);
    }

    // Referenced quit game method from previous project, closes game to desktop.
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}