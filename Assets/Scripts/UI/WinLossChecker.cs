using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Author: Denis
/// </summary>
public class WinLossChecker : MonoBehaviour
{
    private List<GameObject> activeEnemies;
    private GameObject playerPrefab;

    private PlayerController playerControllerComponent;

    [Header("UI References")]
    public GameObject gameOverMenuCanvas;
    public GameObject inGameUICanvas;
    public PauseMenu pauseMenuComponent;
    public TMP_Text gameStateTextField;

    /// <summary>
    /// Cache enemy and player references as well as subscring to their death update delegate
    /// </summary>
    void Start()
    {
        activeEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerControllerComponent = playerPrefab.GetComponent<PlayerController>();

        foreach (GameObject g in activeEnemies)
        {
            g.GetComponent<HealthComponent>().OnCharacterDied += LogEnemyDeath;
        }
        playerPrefab.GetComponent<HealthComponent>().OnCharacterDied += LogPlayerDeath;
    }

    /// <summary>
    /// Registers an enemy death and checks if they were the last one standing.
    /// If the latter is true then the player won the game
    /// </summary>
    /// <param name="g"></param>
    public void LogEnemyDeath(GameObject g)
    {
        activeEnemies.Remove(g);
        if (activeEnemies.Count == 0)
        {
            gameStateTextField.text = "Victory";
            StartCoroutine(GameEndDelay());
        }
    }

    /// <summary>
    /// Registers a player's death. The player lost.
    /// </summary>
    /// <param name="g"></param>
    public void LogPlayerDeath(GameObject g)
    {
        gameStateTextField.text = "Game Over";
        StartCoroutine(GameEndDelay());
    }

    /// <summary>
    /// Coroutine to delay the end of the game after a game ending condition has been met.
    /// Learnm more about coroutines here: https://docs.unity3d.com/ScriptReference/Coroutine.html
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameEndDelay()
    {
        yield return new WaitForSecondsRealtime(1F);

        playerControllerComponent.enabled = false;
        pauseMenuComponent.enabled = false;
        inGameUICanvas.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverMenuCanvas.SetActive(true);
    }
}
