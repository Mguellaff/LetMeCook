using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isFocused;   
    private Outline outline;
    [SerializeField] private GameObject leftOrRightCanvas;
    private PlayerView playerView;
    [SerializeField] private Vector3 createButtonOffset;
    void Start()
    {
        outline = GetComponent<Outline>(); 
        if (leftOrRightCanvas == null)
        {
            leftOrRightCanvas = GameObject.Find("WhichHandCanvas");
        }
        playerView = GameObject.Find("player").GetComponent<PlayerView>();
    }

    public void OnRayEnter()
    {
        isFocused = true;
        outline.enabled = true;
        Debug.Log("OnRayEnter");
        PlaceCanvas();
    }

    public void OnRayExit(Vector3 rayPosition)
    {
        float distance = Vector3.Distance(transform.position, rayPosition);
        if (distance > playerView.maxDistance)
        {
            isFocused = false;
            outline.enabled = false;
            RemoveCanvas();
        }
    }

    public void PlaceCanvas()
    {
        if (gameObject.tag == "Door")
        {
            OpenDoor();
            return; 
        }

        leftOrRightCanvas.SetActive(true);
        leftOrRightCanvas.transform.SetParent(transform);
        if (leftOrRightCanvas == GameObject.Find("WhichHandCanvas") && gameObject.tag!=null)
        {
            leftOrRightCanvas.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        else if(gameObject.tag == null)
        {
            leftOrRightCanvas.transform.localPosition = new Vector3(0, 5, 0);
        }
        else 
        {
            leftOrRightCanvas.transform.localPosition = createButtonOffset;
            Debug.Log("Place create recipe canvas");
        }
    }


    public void RemoveCanvas()
    {
        leftOrRightCanvas.SetActive(false);
        leftOrRightCanvas.transform.SetParent(null);
    }

    public void OpenDoor()
    {
        if (isFocused)
        {
            //Animation.start();
        }
    }

}
