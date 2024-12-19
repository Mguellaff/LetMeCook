using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private bool isEmpty = true; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(GameObject ingredient)
    {
        if (isEmpty)
        {
            ingredient.transform.SetParent(transform);
            ingredient.transform.localPosition = Vector3.zero;
            isEmpty = false;
        }
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }
    public void EmptyContainer()
    {
        if (!isEmpty)
        {
            isEmpty = true;
            List<Transform> childrenToRemove = new List<Transform>();

            foreach (Transform child in transform)
            {
                if (child.tag == "Grabable")
                {
                    childrenToRemove.Add(child);
                }
            }
            for (int i = 0; i < childrenToRemove.Count; i++)
            {

            }

            foreach (Transform child in childrenToRemove)
            {
                child.SetParent(null);
                child.position = new Vector3(0, -10, 0);
            }
        }
    }



}
