using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlacardEtage
{
    public List<Image> kitchenItems = new List<Image>();
}

public class PlacardCanvas : MonoBehaviour
{
    [SerializeField] private Image leftHandImage;
    [SerializeField] private Image rightHandImage;
    [SerializeField] private Image leftHandSprite;
    [SerializeField] private Image rightHandSprite;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject etageButtonPrefab;
    [SerializeField] private Transform etageButtonContainer;
    private List<PlacardEtage> etages = new List<PlacardEtage>();
    private Sprite[] handSprites;
    private Image selectedImage;
    private int currentEtageIndex = 0;

    void Start()
    {
        leftHandImage.sprite = leftHandSprite.sprite;
        rightHandImage.sprite = rightHandSprite.sprite;
        handSprites = Resources.LoadAll<Sprite>("Hands");

        for (int i = 0; i < 3; i++)
        {
            PlacardEtage etage = new PlacardEtage();
            foreach (Transform child in panel2.transform)
            {
                etage.kitchenItems.Add(child.GetComponent<Image>());
            }
            etages.Add(etage);

            // Créer un bouton pour chaque étage
            GameObject button = Instantiate(etageButtonPrefab, etageButtonContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Étage " + (i + 1);
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(() => SelectEtage(index));
        }
    }

    void Update()
    {

    }

    public void CloseCanvas()
    {
        gameObject.SetActive(false);
    }

    public void HandButton(int index)
    {
        if (index == 0)
        {
            HandleHandButton(leftHandImage, handSprites[0]);
        }
        else if (index == 1)
        {
            HandleHandButton(rightHandImage, handSprites[1]);
        }
    }

    private void HandleHandButton(Image handImage, Sprite defaultSprite)
    {
        if (handImage.sprite == defaultSprite && selectedImage != null)
        {
            handImage.sprite = selectedImage.sprite;
        }
        else if (handImage.sprite != defaultSprite)
        {
            selectedImage = handImage;
            handImage.sprite = defaultSprite;
        }
    }

    public void CreateItemButton()
    {
        PlacardEtage currentEtage = etages[currentEtageIndex];

        if (currentEtage.kitchenItems.Count >= 28)
        {
            Debug.LogWarning("La liste kitchenItems contient déjà 28 éléments ou plus.");
            return;
        }

        int index = Random.Range(0, currentEtage.kitchenItems.Count);
        Sprite sprite = currentEtage.kitchenItems[index].sprite;
        CreateImage(panel2);
        int imageToChange = CheckKitchenItems(currentEtage);
        Debug.Log("imageToChange: " + imageToChange);
        if (imageToChange != -1)
        {
            currentEtage.kitchenItems[imageToChange].sprite = sprite;
        }
    }

    private void CreateImage(GameObject parentObject)
    {
        GameObject newImageObject = new GameObject("NewImage");
        newImageObject.transform.SetParent(parentObject.transform);

        Image newImage = newImageObject.AddComponent<Image>();

        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 100);

        etages[currentEtageIndex].kitchenItems.Add(newImage);
    }

    private int CheckKitchenItems(PlacardEtage etage)
    {
        for (int i = 0; i < etage.kitchenItems.Count; i++)
        {
            if (etage.kitchenItems[i].sprite == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void SelectEtage(int index)
    {
        currentEtageIndex = index;
        Debug.Log("Étage sélectionné : " + (index + 1));

        foreach (var etage in etages)
        {
            foreach (var item in etage.kitchenItems)
            {
                item.gameObject.SetActive(false);
            }
        }

        foreach (var item in etages[currentEtageIndex].kitchenItems)
        {
            item.gameObject.SetActive(true);
        }
    }

}
