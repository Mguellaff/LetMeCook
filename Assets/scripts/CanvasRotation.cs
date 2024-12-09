using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    private Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + playerCamera.rotation * Vector3.forward, playerCamera.rotation * Vector3.up);
    }
}
