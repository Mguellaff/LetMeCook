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
    }

    void OnEnable()
    {
        GenerateRandomEtages();
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

    public void HandleHandButton(Image handImage, Sprite defaultSprite)
    {
        if (selectedImage != null)
        {
            handImage.sprite = selectedImage.sprite;
            selectedImage = null;
        }
        else if (handImage.sprite != defaultSprite)
        {
            Sprite spriteToAssign = handImage.sprite;
            handImage.sprite = defaultSprite;
            CreateItemButton();
            AssignSpriteToNewItem(spriteToAssign);
        }
    }

    private void SelectImage(Image image)
    {
        selectedImage = image;
    }

    private void AssignSpriteToNewItem(Sprite sprite)
    {
        PlacardEtage currentEtage = etages[currentEtageIndex];
        int imageToChange = CheckKitchenItems(currentEtage);
        if (imageToChange != -1)
        {
            currentEtage.kitchenItems[imageToChange].sprite = sprite;
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

    private void GenerateRandomEtages()
    {
        // Clear existing etages and buttons
        etages.Clear();
        foreach (Transform child in etageButtonContainer)
        {
            Destroy(child.gameObject);
        }

        int numberOfEtages = Random.Range(2, 8); // Génère un nombre aléatoire entre 2 et 7 (8 est exclus)
        Debug.Log("Nombre d'étages généré : " + numberOfEtages);

        for (int i = 0; i < numberOfEtages; i++)
        {
            PlacardEtage etage = new PlacardEtage();
            foreach (Transform child in panel2.transform)
            {
                Image image = child.GetComponent<Image>();
                etage.kitchenItems.Add(image);
            }
            etages.Add(etage);
            Debug.Log("count:" + etages.Count);
            GameObject button = Instantiate(etageButtonPrefab, etageButtonContainer);
            if (button != null)
            {
                Debug.Log($"Button for Étage {i + 1} instantiated successfully.");
            }
            else
            {
                Debug.LogError($"Failed to instantiate button for Étage {i + 1}.");
            }

            if (etageButtonContainer != null)
            {
                Debug.Log("etageButtonContainer is assigned.");
            }
            else
            {
                Debug.LogError("etageButtonContainer is not assigned.");
            }

            button.GetComponentInChildren<TextMeshProUGUI>().text = "Étage " + (i + 1);
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(() => SelectEtage(index));
        }
    }
}
