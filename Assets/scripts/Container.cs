using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private bool isEmpty = true;

    void Start()
    {

    }

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

            foreach (Transform child in childrenToRemove)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
