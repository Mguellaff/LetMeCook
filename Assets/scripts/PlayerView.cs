using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    Camera cam;
    Vector2 mouseLook;
    private Interactable lastInteractable;

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        mouseLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(Vector3.up * mouseLook.x);
        cam.transform.Rotate(Vector3.right * -mouseLook.y);

    }

    private void FixedUpdate()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (lastInteractable != null && lastInteractable != interactable)
                    {
                        lastInteractable.OnRayExit();
                    }
                    interactable.OnRayEnter();
                    lastInteractable = interactable;
                }
            }
            else if (lastInteractable != null)
            {
                lastInteractable.OnRayExit();
                lastInteractable = null;
            }
        }
        else if (lastInteractable != null)
        {
            lastInteractable.OnRayExit();
            lastInteractable = null;
        }
    }
}
