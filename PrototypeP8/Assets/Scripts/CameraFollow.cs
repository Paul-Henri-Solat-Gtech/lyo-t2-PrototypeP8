using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cible;
    public float normalFOV = 60f;
    public Vector3 offset = new Vector3(0, 3, -3); 

    public float sprintFOV = 90f;
    private Camera cam;
    private CubeGlouton cubeGlouton;

    void Start()
    {
        cam = GetComponent<Camera>();
        cubeGlouton = cible.GetComponent<CubeGlouton>();
    }

    void LateUpdate()
    {
        transform.position = cible.position + offset;
        transform.LookAt(cible);

        if (cubeGlouton != null && cubeGlouton.IsSprinting)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, Time.deltaTime * 2);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * 2);
        }
    }
}
