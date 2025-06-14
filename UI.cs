/*
* Author: Mei Yifan
* Date: 14/6/2025
* Description: handles restart, quit, and resume actions in the end game UI
*/

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// controls what happens when player chooses to restart, quit, or resume from end game screen
/// </summary>
public class EndGameUI : MonoBehaviour
{
    /// <summary>
    /// restart the current scene
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// quit the game (or stop play mode in editor)
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop play mode in editor
#else
        Application.Quit(); // quit the game when built
#endif
    }

    /// <summary>
    /// unpause the game and hide this UI
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
