using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    // Set this to your pixels-per-unit
    public float pixelSize = 1f / 16f; 

    private void LateUpdate()
    {
        // Calculate target position with offset
        Vector3 targetPosition = target.position + offset;

        // Pixel-perfect rounding applied to the target position BEFORE smoothing
        targetPosition.x = Mathf.Round(targetPosition.x / pixelSize) * pixelSize;
        targetPosition.y = Mathf.Round(targetPosition.y / pixelSize) * pixelSize;

        // Smoothly move camera toward the rounded target position
        Vector3 smooth = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.position = smooth;
    }
}
