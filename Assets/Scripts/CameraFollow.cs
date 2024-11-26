using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform; // Reference to the car's transform
    public Vector3 offset = new Vector3(-14.29f, 5.53f, 13.3f); // Default camera offset
    public float followSpeed = 5f; // Speed at which the camera adjusts position
    public float rotateSpeed = 5f; // Speed at which the camera adjusts rotation

    void LateUpdate()
    {
        if (carTransform == null) return;

        // Smoothly follow the car's position
        Vector3 targetPosition = carTransform.position + carTransform.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate to match the car's rotation
        Quaternion targetRotation = Quaternion.LookRotation(carTransform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
