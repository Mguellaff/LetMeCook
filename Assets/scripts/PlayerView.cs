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

        Debug.Log(Input.mousePosition);
        //Cursor.visible = false;
    }

    void Update()
    {
        // Obtenir les mouvements de la souris
        mouseLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Appliquer la rotation en fonction des mouvements de la souris
        transform.Rotate(Vector3.up * mouseLook.x);
        cam.transform.Rotate(Vector3.right * -mouseLook.y);

        // Déplacer la souris au centre du jeu
        Vector3 centerGame = cam.WorldToScreenPoint(transform.position);
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;

        // Déplacer la souris au centre de l'écran
        Input.mousePosition.Set(centerGame.x, centerGame.y, 0);
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

}
