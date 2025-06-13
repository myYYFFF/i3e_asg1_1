using UnityEngine;
using TMPro;

/// <summary>
/// Updates and displays the player's current score on the UI.
/// </summary>
public class Score : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshPro UI component to display the score.
    /// </summary>
    public TMP_Text scoreText;

    /// <summary>
    /// Reference to the PlayerBehaviour script to get the current score.
    /// </summary>
    private PlayerBehaviour player;

    /// <summary>
    /// Called before the first frame update.
    /// Finds the PlayerBehaviour instance in the scene.
    /// </summary>
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    /// <summary>
    /// Called once per frame.
    /// Updates the score UI text with the player's current score.
    /// </summary>
    void Update()
    {
        if (player != null)
        {
            scoreText.text = "Score: " + player.GetScore();
        }
    }
}
