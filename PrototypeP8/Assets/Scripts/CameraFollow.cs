using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cible;
    public float distance = 5.0f;
    public float hauteur = 2.0f;
    public float sensibilitéRotation = 5.0f;
    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void LateUpdate()
    {
        rotationY += Input.GetAxis("Mouse X") * sensibilitéRotation;
        rotationX -= Input.GetAxis("Mouse Y") * sensibilitéRotation;
        rotationX = Mathf.Clamp(rotationX, -20f, 80f); 

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 position = cible.position - (rotation * Vector3.forward * distance) + Vector3.up * hauteur;

        transform.rotation = rotation;
        transform.position = position;
    }
}
