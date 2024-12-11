using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isFocused;   
    private Outline outline;
    private GameObject leftOrRightCanvas;
    private PlayerView playerView;
    void Start()
    {
        outline = GetComponent<Outline>();
        leftOrRightCanvas = GameObject.Find("WhichHandCanvas");
        playerView = GameObject.Find("player").GetComponent<PlayerView>();
    }

    public void OnRayEnter()
    {
        isFocused = true;
        outline.enabled = true;
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
        leftOrRightCanvas.SetActive(true);
        leftOrRightCanvas.transform.SetParent(transform);
        leftOrRightCanvas.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    public void RemoveCanvas()
    {
        leftOrRightCanvas.SetActive(false);
        leftOrRightCanvas.transform.SetParent(null);
    }


}
