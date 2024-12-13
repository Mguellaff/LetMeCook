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

    private void ContainerSprite(Image hand, string name)
    {
        Debug.Log($"ContainerSprite called with hand: {hand.name}, name: {name}");

        // si le container a un ingrédient et que la main est vide
        if (hand.sprite.name == name && !container.IsEmpty())
        {
            Transform ingredient = null;
            foreach (Transform child in transform.parent)
            {
                if (child.tag == "Grabable")
                {
                    ingredient = child;
                    break;
                }
            }
            Debug.Log($"ingredient found: {ingredient?.name}, tag: {ingredient?.tag}");

            if (ingredient != null && ingredient.tag == "Grabable")
            {
                Debug.Log($"ingredient is Grabable, name: {ingredient.name}");
                hand.sprite = Resources.Load<Sprite>(ingredient.name);
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
            hand.sprite = System.Array.Find(handSprites, sprite => sprite.name == name);
        }
    }

}


