/*
 * Author: Mei Yifan
 * Date: 14/6/2025
 * Description: Tracks elapsed time and displays it as a MM:SS formatted timer using TextMeshProUGUI.
 */

using UnityEngine;
using TMPro;

/// <summary>
/// Displays a running timer that shows minutes and seconds since the game started.
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshProUGUI component used to display the timer text.
    /// </summary>
    [SerializeField] private TextMeshProUGUI timertext;

    /// <summary>
    /// Accumulated elapsed time in seconds.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// Updates the timer each frame and displays the formatted time.
    /// </summary>
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);// Round down to get whole minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60);// Round down to get whole seconds
        
         // Format as two digits (e.g. 01:07) with leading zeros if needed
        timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
