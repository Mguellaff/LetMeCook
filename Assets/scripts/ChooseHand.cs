using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseHand : MonoBehaviour
{
    [SerializeField] private Image leftHand;
    [SerializeField] private Image rightHand;
    private string parentName;
    private GameObject ingredient;
    private Container container;
    public void Choose(int hand)
    {
        parentName = transform.parent.name;
        string resourcePath = $"{parentName}";
        if (transform.parent.tag == "Grabable")
        {
            if (hand == 0)
            {
                rightHand.sprite = Resources.Load<Sprite>(resourcePath);
                transform.parent.position = new Vector3(0, -10, 0);
            }
            else if (hand == 1)
            {
                leftHand.sprite = Resources.Load<Sprite>(resourcePath);
                transform.parent.position = new Vector3(0, -10, 0);
            }
        }
        else if (transform.parent.tag == "Container")
        {
            Sprite[] handSprites = Resources.LoadAll<Sprite>("hands");
            container = transform.parent.GetComponent<Container>();

            if (hand == 0)
            {
                ingredient = GameObject.Find(leftHand.sprite.name);

                if (ingredient != null)
                {
                    container.AddIngredient(ingredient);
                }

                Transform child = transform.parent.GetComponentInChildren<Transform>();
                Debug.Log(child);
                if (child != null && child.tag == "Grabable")
                {
                    Debug.Log(child.name);
                    leftHand.sprite = Resources.Load<Sprite>(child.name);
                    container.EmptyContainer();
                }
                else
                {
                    leftHand.sprite = System.Array.Find(handSprites, sprite => sprite.name == "leftHand");
                }
            }
            else if (hand == 1)
            {
                ingredient = GameObject.Find(rightHand.sprite.name);

                if (ingredient != null)
                {
                    container.AddIngredient(ingredient);
                }

                Transform child = transform.parent.GetComponentInChildren<Transform>();
                Debug.Log(child);
                if (child != null && child.tag == "Grabable")
                {
                    Debug.Log(child.name);
                    rightHand.sprite = Resources.Load<Sprite>(child.name);
                    container.EmptyContainer();
                }
                else
                {
                    rightHand.sprite = System.Array.Find(handSprites, sprite => sprite.name == "rightHand");
                }
            }
        }
    }



}
