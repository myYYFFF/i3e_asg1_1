using UnityEngine;
using TMPro;

/// <summary>
/// Shows a running timer on the UI in minutes and seconds.
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// Text component to display the timer.
    /// </summary>
    [SerializeField] 
    private TextMeshProUGUI timertext;

    /// <summary>
    /// Time passed since the timer started.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// Called every frame to update the timer.
    /// </summary>
    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        //takes the resulting floating-point number of minutes and rounds it down to the nearest whole integer
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);//min:sec
    }
}
