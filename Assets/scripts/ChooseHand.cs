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
    private Sprite[] handSprites;

    private void Start()
    {
        handSprites = Resources.LoadAll<Sprite>("Hands");
    }
    public void Choose(int hand)
    {
        parentName = transform.parent.name;
        string resourcePath = $"{parentName}";
        if (transform.parent.tag == "Grabable")
        {
            if (hand == 0)
            {
                leftHand.sprite = Resources.Load<Sprite>(resourcePath);
                transform.parent.position = new Vector3(0, -10, 0);
            }
            else if (hand == 1)
            {
                rightHand.sprite = Resources.Load<Sprite>(resourcePath);
                transform.parent.position = new Vector3(0, -10, 0);
            }
        }
        else if (transform.parent.tag == "Container")
        {
            container = transform.parent.GetComponent<Container>();

            if (hand == 0)
            {
                ContainerSprite(leftHand, "leftHand");
            }
            else if (hand == 1)
            {
                ContainerSprite(rightHand, "rightHand");
            }
        }
    }

    private void ContainerSprite(Image hand,string name)
    {
        if (hand.sprite.name == name && !container.IsEmpty())
        {
            Transform child = transform.parent.GetComponentInChildren<Transform>();
            if (child != null && child.tag == "Grabable")
            {
                Debug.Log($"Child is Grabable, name: {child.name}");
                hand.sprite = Resources.Load<Sprite>(child.name);
                container.EmptyContainer();
            }
        }
        else
        {
            ingredient = GameObject.Find(hand.sprite.name);

            if (ingredient != null)
            {
                container.AddIngredient(ingredient);
            }

            Transform child = transform.parent.GetComponentInChildren<Transform>();

            if (child != null && child.tag == "Grabable")
            {
                hand.sprite = Resources.Load<Sprite>(child.name);
                container.EmptyContainer();
            }
            else
            {
                hand.sprite = System.Array.Find(handSprites, sprite => sprite.name == name);
            }
        }
    }

}
