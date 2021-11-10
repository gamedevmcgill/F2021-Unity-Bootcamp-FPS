using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private InputActions controlsMap;

    [Header("Player Component References")]
    public PlayerController playerControllerComponent;

    [Header("UI References")]
    public GameObject pauseMenuCanvas;
    public GameObject inGameUICanvas;

    /// <summary>
    /// Learn more about Unity Script Initialization order
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Creation of an input map
    /// </summary>
    private void Awake()
    {
        controlsMap = new InputActions();
    }

    /// <summary>
    /// Subscribing to Input Action events and enabling the behavior when the script is on
    /// </summary>
    private void OnEnable()
    {
        controlsMap.UI_Game_On.Menu_Toggle.performed += eventCtx => ToggleMenu();
        controlsMap.UI_Game_On.Menu_Toggle.Enable();
    }

    /// <summary>
    /// Unsubscribing to Input Action events and disabling the behavior when the script is off
    /// </summary>
    private void OnDisable()
    {
        controlsMap.UI_Game_On.Menu_Toggle.performed -= eventCtx => ToggleMenu();
        controlsMap.UI_Game_On.Menu_Toggle.Disable();
    }

    /// <summary>
    /// Opens and closes the menu after escape or resume button press. 
    /// Additionally manages extra game parameters to resume the experience. Learn more:
    /// https://docs.unity3d.com/ScriptReference/Time-timeScale.html
    /// https://docs.unity3d.com/ScriptReference/Cursor-lockState.html
    /// </summary>
    public void ToggleMenu()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
        inGameUICanvas.SetActive(!inGameUICanvas.activeSelf);

        if (pauseMenuCanvas.activeSelf == true)
        {
            Time.timeScale = 0;
            playerControllerComponent.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerControllerComponent.enabled = true;
            Time.timeScale = 1;
        }
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
