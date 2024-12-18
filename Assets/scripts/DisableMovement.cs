using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerView view;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        movement.SwitchMovement();
        view.enabled = false;
    }

    private void OnDisable()
    {
        movement.SwitchMovement();
        view.enabled = true;
    }
}
