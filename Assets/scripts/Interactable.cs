using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isFocused;   
    private Outline outline;
    [SerializeField] private GameObject leftOrRightCanvas;
    private PlayerView playerView;
    [SerializeField] private Vector3 createButtonOffset;
    [SerializeField] private float removeCanvasDelay = 4.0f;
    private Animator animator;
    [SerializeField] private Canvas doorCanvas; 
    void Start()
    {
        outline = GetComponent<Outline>(); 
        if (leftOrRightCanvas == null)
        {
            leftOrRightCanvas = GameObject.Find("WhichHandCanvas");
        }
        playerView = GameObject.Find("player").GetComponent<PlayerView>();
        animator = leftOrRightCanvas.GetComponent<Animator>();
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
        isFocused = false;
        outline.enabled = false;
        StartCoroutine(RemoveCanvasAfterDelay());
    }

    private IEnumerator RemoveCanvasAfterDelay()
    {
        yield return new WaitForSeconds(removeCanvasDelay);
        RemoveCanvas();
    }

    public void PlaceCanvas()
    {
        if (gameObject.tag == "Door")
        {
            OpenDoor();
            return;
        }

        leftOrRightCanvas.SetActive(true);
        if (animator != null)
        {
            animator.SetBool("IsGrowing", true);
            StartCoroutine(ResetIsGrowingAfterAnimation());
        }
        leftOrRightCanvas.transform.SetParent(transform);
        if (leftOrRightCanvas == GameObject.Find("WhichHandCanvas") && gameObject.tag != null)
        {
            leftOrRightCanvas.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        else if (gameObject.tag == null)
        {
            leftOrRightCanvas.transform.localPosition = new Vector3(0, 5, 0);
        }
        else
        {
            leftOrRightCanvas.transform.localPosition = createButtonOffset;
            Debug.Log("Place create recipe canvas");
        }
    }

    private IEnumerator ResetIsGrowingAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("IsGrowing", false);
    }



    public void RemoveCanvas()
    {
        if (animator != null)
        {
            animator.SetBool("IsReducing", true);
        }
        StartCoroutine(DeactivateCanvasAfterAnimation(animator));
    }

    private IEnumerator DeactivateCanvasAfterAnimation(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("IsReducing", false);
        leftOrRightCanvas.SetActive(false);
        leftOrRightCanvas.transform.SetParent(null);
    }

    public void OpenDoor()
    {
       if(Input.GetMouseButtonDown(0))
        {
            doorCanvas.gameObject.SetActive(true);
        }
    }

}
