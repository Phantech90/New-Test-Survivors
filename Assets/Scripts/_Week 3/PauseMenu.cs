using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    public bool isPaused = false;

    private void Start()
    {
        // Hide the pause menu when the game starts.
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        // Check whether Escape was pressed.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else PauseGame();

        }
    }

    public void PauseGame()
    {
        isPaused = true;



        // Show the pause menu.
        pauseMenu.SetActive(true);



        // Pause the game.
        Time.timeScale = 0f;

        // Print "Game Paused" to the Console.
        Debug.Log("Game Paused");


    }

    // Hint: Don't forget to hook this function up to the UI Button.
    public void ResumeGame()
    {
        isPaused = false;



        // Hide the pause menu.
        pauseMenu.SetActive(false);



        // Resume the game.
        Time.timeScale = 1f;

        // Print "Game Resumed" to the Console.
        Debug.Log("Game Resumed");


    }

    public void RestartGame()
    {
        // Make sure time is running again.
        Time.timeScale = 1f;

        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}