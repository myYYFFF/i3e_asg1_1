/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Handles coin behavior, including highlighting and player collection.
*/

using UnityEngine;

/// <summary>
/// Controls coin interaction, including highlight and score collection.
/// </summary>
public class CoinBehaviour : MonoBehaviour
{
    /// <summary>
    /// The coin's MeshRenderer for changing materials.
    /// </summary>
    MeshRenderer MyMeshRenderer;

    /// <summary>
    /// Material used when the coin is highlighted.
    /// </summary>
    [SerializeField]
    Material hightlightMat;

    /// <summary>
    /// Stores the original material of the coin.
    /// </summary>
    Material originalMat;

    /// <summary>
    /// Value the coin adds to the player's score.
    /// </summary>
    int value = 10;

    /// <summary>
    /// Called when player collects the coin.
    /// </summary>
    /// <param name="player">The player collecting the coin.</param>
    public void Collect(PlayerBehaviour player)
    {
        // Add coin value to player score
        player.ModifyScore(value);
        
        // Remove coin from the scene
        Destroy(gameObject);
    }

    /// <summary>
    /// Initializes material references on start.
    /// </summary>
    void Start() 
    {
        // Get the MeshRenderer component
        MyMeshRenderer = GetComponent<MeshRenderer>(); 

        // Store the original material
        originalMat = MyMeshRenderer.material;
    }

    /// <summary>
    /// Changes material to highlight material.
    /// </summary>
    public void Highlight() 
    {
        // Apply highlight material
        MyMeshRenderer.material = hightlightMat; 
    }

    /// <summary>
    /// Reverts material to original.
    /// </summary>
    public void UnHighlight() 
    {
        // Restore original material
        MyMeshRenderer.material = originalMat; 
    }
}
