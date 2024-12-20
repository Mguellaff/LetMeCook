using UnityEngine;

public class PlayerView : MonoBehaviour
{
    Camera cam;
    Vector2 mouseLook;
    private Interactable lastInteractable;
    private bool rotate = true;
    private bool isCursorVisible = false;
    [SerializeField] public float maxDistance;
    private GameObject player;
    private PlayerMovement playerMovement;

    void Start()
    {
        cam = Camera.main;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = CursorLockMode.Confined;
        player = GameObject.Find("player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        mouseLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        RotateCamera(rotate);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.SwitchMovement();
            isCursorVisible = !isCursorVisible;
            Cursor.visible = isCursorVisible;
            Debug.Log("Cursor visible: " + Cursor.visible);
            rotate = !rotate;
            RotateCamera(rotate);
            if(Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }




    private void FixedUpdate()
    {
        if(Cursor.lockState == CursorLockMode.None)
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
    }

    private void RotateCamera(bool rotate)
    {
        if(rotate)
        { 
        transform.Rotate(Vector3.up * mouseLook.x);
        cam.transform.Rotate(Vector3.right * -mouseLook.y);
        }
        else {
            transform.Rotate(Vector3.up * 0);
            cam.transform.Rotate(Vector3.right * 0);
        }
    }

}
