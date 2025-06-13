using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the UI for ending the game, including restart, quit, and resume actions.
/// </summary>
public class EndGameUI : MonoBehaviour
{
    /// <summary>
    /// Restarts the current game by reloading the active scene.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the game or stops play mode if running inside the Unity editor.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        Application.Quit(); // Quit in a built game
#endif
    }

    /// <summary>
    /// Resumes the game by unpausing and hiding the UI panel.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
