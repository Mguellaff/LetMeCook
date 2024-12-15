using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerView : MonoBehaviour
{
    Camera cam;
    Vector2 mouseLook;
    private Interactable lastInteractable;

    [SerializeField] public float maxDistance;
    void Start()
    {
        cam = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        mouseLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.Rotate(Vector3.up * mouseLook.x);
        cam.transform.Rotate(Vector3.right * -mouseLook.y);

        Vector3 centerGame = cam.WorldToScreenPoint(transform.position);
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;

        Input.mousePosition.Set(centerGame.x, centerGame.y, 0);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MouseLock();
        }
    }



    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int interactableLayer = LayerMask.NameToLayer("Interactable");

            if (hit.collider.gameObject.layer == interactableLayer)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (lastInteractable != null && lastInteractable != interactable)
                    {
                        lastInteractable.OnRayExit(ray.origin);
                    }
                    interactable.OnRayEnter();
                    lastInteractable = interactable;
                }
            }
            else if (lastInteractable != null)
            {
                lastInteractable.OnRayExit(ray.origin);
                lastInteractable = null;
            }
        }
        else if (lastInteractable != null)
        {
            lastInteractable.OnRayExit(ray.origin);
            lastInteractable = null;
        }
    }

    private void MouseLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



}
