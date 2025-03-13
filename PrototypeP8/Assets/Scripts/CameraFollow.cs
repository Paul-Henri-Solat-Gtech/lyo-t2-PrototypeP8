using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cible;
    public float distanceBase = 2.0f;
    public float hauteur = 2.0f;
    public float sensibiliteRotation = 5.0f;
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
        rotationY += Input.GetAxis("Mouse X") * sensibiliteRotation;
        rotationX -= Input.GetAxis("Mouse Y") * sensibiliteRotation;
        rotationX = Mathf.Clamp(rotationX, -20f, 80f); 

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);


        float tailleCube = cible.localScale.x;
        float distanceAjustee = distanceBase + tailleCube;

        Vector3 position = cible.position - (rotation * Vector3.forward * distanceAjustee) + Vector3.up * hauteur;

        transform.rotation = rotation;
        transform.position = position;
    }
}
