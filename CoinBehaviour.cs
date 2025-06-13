/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script controls how the coin behaves when collected and when it gets highlighted.
 */

using UnityEngine;

/// <summary>
/// Handles coin behavior like being collected and highlighted.
/// </summary>
public class CoinBehaviour : MonoBehaviour
{
    /// <summary>
    /// The renderer of the coin.
    /// </summary>
    MeshRenderer MyMeshRenderer;

    /// <summary>
    /// Material used when the coin is highlighted.
    /// </summary>
    [SerializeField] Material hightlightMat;

    /// <summary>
    /// The original material before highlight.
    /// </summary>
    Material originalMat;

    /// <summary>
    /// How much the coin is worth in score.
    /// </summary>
    int value = 10;

    /// <summary>
    /// Gives the score to the player and removes the coin.
    /// </summary>
    /// <param name="player">The player who collects the coin.</param>
    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(value);
        Destroy(gameObject); // destroy when collected
    }

    /// <summary>
    /// Called when the coin appears. Sets up the material.
    /// </summary>
    void Start() 
    {
        MyMeshRenderer = GetComponent<MeshRenderer>(); // get renderer
        originalMat = MyMeshRenderer.material; // store original material
    }

    /// <summary>
    /// Makes the coin look highlighted.
    /// </summary>
    public void Highlight()
    {
        MyMeshRenderer.material = hightlightMat; 
    }

    /// <summary>
    /// Returns the coin to its original look.
    /// </summary>
    public void UnHighlight()
    {
        MyMeshRenderer.material = originalMat; 
    }
}
