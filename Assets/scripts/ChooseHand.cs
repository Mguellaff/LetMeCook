using UnityEngine;
using UnityEngine.UI;

public class ChooseHand : MonoBehaviour
{
    [SerializeField] private Image leftHand;
    [SerializeField] private Image rightHand;
    private GameObject ingredient;
    private Container container;
    private Sprite[] handSprites;

    private void Start()
    {
        handSprites = Resources.LoadAll<Sprite>("Hands");
        Debug.Log("Hand sprites loaded: " + handSprites.Length);
    }

    public void Choose(int hand)
    {
        if (transform.parent.tag == "Grabable")
        {
            MeshFilter ingredientMeshFilter = transform.parent.GetComponent<MeshFilter>();
            if (ingredientMeshFilter != null)
            {
                string ingredientMeshName = ingredientMeshFilter.mesh.name.Replace(" Instance", "");
                Debug.Log("Ingredient mesh name: " + ingredientMeshName);

                if (hand == 0)
                {
                    leftHand.sprite = Resources.Load<Sprite>(ingredientMeshName);
                    Debug.Log("Left hand sprite set to: " + leftHand.sprite.name);
                    transform.parent.position = new Vector3(0, -10, 0);
                }
                else if (hand == 1)
                {
                    rightHand.sprite = Resources.Load<Sprite>(ingredientMeshName);
                    Debug.Log("Right hand sprite set to: " + rightHand.sprite.name);
                    transform.parent.position = new Vector3(0, -10, 0);
                }
            }
            else
            {
                Debug.LogError("Ingredient does not have a MeshFilter component.");
            }
        }
        else if (transform.parent.tag == "Container")
        {
            container = transform.parent.GetComponent<Container>();

            if (container == null)
            {
                Debug.LogError("Container component not found on parent.");
                return;
            }

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
        if (hand.sprite == null)
        {
            Debug.LogError("Hand sprite is null.");
            return;
        }

        Debug.Log("Hand sprite name: " + hand.sprite.name);

        // si le container a un ingrédient et que la main est vide
        if (hand.sprite.name == name && !container.IsEmpty())
        {
            Transform ingredientTransform = null;
            foreach (Transform child in transform.parent)
            {
                if (child.tag == "Grabable")
                {
                    ingredientTransform = child;
                    break;
                }
            }

            if (ingredientTransform != null)
            {
                MeshFilter ingredientMeshFilter = ingredientTransform.GetComponent<MeshFilter>();
                if (ingredientMeshFilter != null)
                {
                    string ingredientMeshName = ingredientMeshFilter.mesh.name.Replace(" Instance", "");
                    hand.sprite = Resources.Load<Sprite>(ingredientMeshName);
                    Debug.Log("Hand sprite set to ingredient: " + hand.sprite.name);
                    container.EmptyContainer();
                }
            }
        }
        else
        {
            foreach (Transform child in transform.parent)
            {
                MeshFilter meshFilter = child.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.mesh.name.Replace(" Instance", "") == hand.sprite.name)
                {
                    ingredient = child.gameObject;
                    break;
                }
            }

            if (ingredient != null)
            {
                container.AddIngredient(ingredient);
                Debug.Log("Ingredient added to container: " + ingredient.name);
                ingredient.transform.SetParent(container.transform);
                ingredient.transform.localPosition = Vector3.zero; // Ajustez la position selon vos besoins
            }

            hand.sprite = System.Array.Find(handSprites, sprite => sprite.name == name);
            Debug.Log("Hand sprite set to: " + hand.sprite.name);
        }
    }
}
