/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Controls the mob's health bar UI to face the camera and update health values.
*/

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the health bar slider for mobs, keeping it facing the camera and updating its value.
/// </summary>
public class MobHealthBar : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI slider showing health.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Reference to the main camera to face the health bar toward.
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// Initialize the main camera reference if not assigned.
    /// </summary>
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    /// <summary>
    /// Make the health bar always face the camera.
    /// </summary>
    void Update()
    {
        if (mainCamera != null)
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }

    /// <summary>
    /// Set the slider's maximum health value and current value.
    /// </summary>
    /// <param name="health">Maximum health value.</param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    /// <summary>
    /// Update the slider's current health value.
    /// </summary>
    /// <param name="health">Current health value.</param>
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
