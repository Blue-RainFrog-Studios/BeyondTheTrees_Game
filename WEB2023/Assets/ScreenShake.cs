using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Intensity of the screen shake
    public float shakeIntensity = 0.00005f;
    public float shakeFrequency = 10f;

    // Duration of the screen shake
    public float shakeDuration = 2f;

    // Reference to the main camera
    private Camera mainCamera;

    // Initial position of the camera
    private Vector3 initialPosition;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Store the initial position of the camera
        initialPosition = mainCamera.transform.position;
    }

    public IEnumerator ShakeScreen()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Calculate the next position based on Perlin noise for a smoother effect
            float offsetX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0) * 2 - 1;
            float offsetY = Mathf.PerlinNoise(0, Time.time * shakeFrequency) * 2 - 1;

            Vector3 randomOffset = new Vector3(offsetX, offsetY, 0) * shakeIntensity;

            // Interpolate between initial and final positions for smooth movement
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initialPosition + randomOffset, Time.deltaTime * 10f);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera's position to its initial position
        mainCamera.transform.position = initialPosition;
    }
}