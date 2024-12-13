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
        if (leftOrRightCanvas == GameObject.Find("WhichHandCanvas"))
        { 
        leftOrRightCanvas.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        else
        {
            leftOrRightCanvas.transform.localPosition = createButtonOffset;
        }
    }

    public void RemoveCanvas()
    {
        leftOrRightCanvas.SetActive(false);
        leftOrRightCanvas.transform.SetParent(null);
    }


}
