using UnityEngine;
using Cinemachine;

/// <summary>
/// Controls the zoom functionality for a Cinemachine virtual camera
/// based on mouse scroll input. Allows setting of customizable
/// zoom speed, minimum, and maximum zoom levels.
/// </summary>
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class ZoomController : MonoBehaviour
{
    [Header("Zoom Configuration")]
    [Tooltip("The Cinemachine virtual camera to control the zoom level.")]
    public CinemachineVirtualCamera cinemachineCamera;

    [Tooltip("Speed at which the camera zooms in/out.")]
    [Range(1f, 20f)]
    public float zoomSpeed = 5f;

    [Tooltip("Minimum allowed field of view (zoomed in).")]
    [Range(5f, 30f)]
    public float minZoom = 10f;

    [Tooltip("Maximum allowed field of view (zoomed out).")]
    [Range(40f, 90f)]
    public float maxZoom = 60f;

    /// <summary>
    /// Called every frame to handle zooming input and adjust the camera field of view.
    /// </summary>
    void Update()
    {
        // Capture scroll input from the mouse
        float scrollData = Input.mouseScrollDelta.y;

        // Only adjust FOV if there is scroll input and the camera is assigned
        if (Mathf.Abs(scrollData) > 0.01f)
        {
            AdjustZoom(scrollData);
        }
        else if (cinemachineCamera == null)
        {
            Debug.LogWarning("Cinemachine Camera is not assigned to the ZoomController.");
        }
    }

    /// <summary>
    /// Adjusts the camera's field of view (FOV) based on scroll input.
    /// </summary>
    /// <param name="scrollInput">Scroll input from the mouse wheel.</param>
    private void AdjustZoom(float scrollInput)
    {
        if (cinemachineCamera == null) return;

        // Calculate the new field of view based on scroll input and speed
        float currentFOV = cinemachineCamera.m_Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(currentFOV - scrollInput * zoomSpeed, minZoom, maxZoom);

        // Set the camera's field of view to the clamped target value
        cinemachineCamera.m_Lens.FieldOfView = targetFOV;

        // Log the updated FOV value for debugging
        Debug.Log($"Updated Field of View: {targetFOV}");
    }
}
