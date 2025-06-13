/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Controls the mob health bar UI to face the camera and update health values.
 */

using UnityEngine;
using UnityEngine.UI;

public class MobHealthBar : MonoBehaviour
{
    /// <summary>
    /// The UI slider showing the health bar.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// The main camera in the scene.
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// Set the main camera if not assigned.
    /// </summary>
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    /// <summary>
    /// Rotate the health bar to always face the camera.
    /// </summary>
    void Update()
    {
        if (mainCamera != null)
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }

    /// <summary>
    /// Set the max value of the health bar and initialize its value.
    /// </summary>
    /// <param name="health">Maximum health value.</param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    /// <summary>
    /// Update the health bar to show current health.
    /// </summary>
    /// <param name="health">Current health value.</param>
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
