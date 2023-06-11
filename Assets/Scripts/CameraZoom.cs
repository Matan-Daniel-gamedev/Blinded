using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float startSize = 10f;
    public float targetSize = 50f;
    public float stayDuration = 5f;
    public float zoomSpeedMultiplier = 1.01f;

    private Camera mainCamera;
    private float currentTime = 0f;
    private float currentSize;
    private bool isZoomingOut = true;
    private bool isZoomingIn = false;
    private bool isStaying = false;
    private float zoomDirection = 1f;
    private float zoomSpeed = 1f;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        currentSize = startSize;
        mainCamera.orthographicSize = currentSize;
    }

    private void Update()
    {
        if (isZoomingOut)
        {
            currentSize += zoomSpeed * Time.deltaTime;
            mainCamera.orthographicSize = currentSize;

            if (currentSize >= targetSize)
            {
                currentSize = targetSize;
                mainCamera.orthographicSize = currentSize;
                isZoomingOut = false;
                isStaying = true;
                currentTime = 0f;
            }
        }
        else if (isStaying)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= stayDuration)
            {
                isStaying = false;
                isZoomingIn = true;
                currentTime = 0f;
                zoomDirection = -1f;
                zoomSpeed = 1f;
            }
        }
        else if (isZoomingIn)
        {
            currentSize += zoomSpeed * Time.deltaTime * zoomDirection;
            mainCamera.orthographicSize = currentSize;

            if (currentSize <= startSize)
            {
                currentSize = startSize;
                mainCamera.orthographicSize = currentSize;
                isZoomingIn = false;
            }
        }

        // Increase zoom speed gradually
        if (isZoomingOut || isZoomingIn)
        {
            zoomSpeed *= zoomSpeedMultiplier;
        }
    }

    public void TriggerZoom()
    {
        if (!isZoomingOut && !isStaying && !isZoomingIn)
        {
            isZoomingOut = true;
        }
    }
}
