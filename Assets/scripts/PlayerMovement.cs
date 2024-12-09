using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Camera cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = cam.transform.right * moveHorizontal + cam.transform.forward * moveVertical;
        move.y = 0; 
        controller.Move(move * Time.deltaTime * speed);
    }
}
