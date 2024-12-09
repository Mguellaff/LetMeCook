using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isFocused;   
    private Outline outline;
    private GameObject leftOrRightCanvas;

    void Start()
    {
        outline = GetComponent<Outline>();
        leftOrRightCanvas = GameObject.Find("test");
    }

    void Update()
    {
        
    }

    public void OnRayEnter()
    {
        isFocused = true;
        outline.enabled = true;
        PlaceCanvas();
    }
    public void OnRayExit()
    {
        isFocused = false;
        outline.enabled = false;
        //leftOrRightCanvas.SetActive(false);
    }
    public void PlaceCanvas()
    {
        leftOrRightCanvas.SetActive(true);
        leftOrRightCanvas.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
    }
}
