/*
 * Author: Mei Yifan
 * Date: 14/6/2025
 * Description: Updates and displays the player's current score using TextMeshPro UI text.
 */

using UnityEngine;
using TMPro;

/// <summary>
/// Displays the player's current score on the UI by fetching the score from PlayerBehaviour.
/// </summary>
public class Score : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshPro UI text component that shows the score.
    /// </summary>
    public TMP_Text scoreText;

    /// <summary>
    /// Reference to the PlayerBehaviour script to retrieve the current score.
    /// </summary>
    private PlayerBehaviour player;

    /// <summary>
    /// Finds the PlayerBehaviour component in the scene on start.
    /// </summary>
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    /// <summary>
    /// Updates the score text UI every frame to reflect the player's current score.
    /// </summary>
    void Update()
    {
        if (player != null)
        {
            scoreText.text = "Score: " + player.GetScore();
        }
    }
}
