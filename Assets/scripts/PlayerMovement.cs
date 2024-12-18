using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Camera cam;
    private bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update()
    {
        if (canMove) 
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = cam.transform.right * moveHorizontal + cam.transform.forward * moveVertical;
            move.y = 0;
            controller.Move(move * Time.deltaTime * speed);
        }
        else
        {
            controller.Move(Vector3.zero);
        }
    }

    public void SwitchMovement()
    {
        canMove = !canMove; 
    }

}

