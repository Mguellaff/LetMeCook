using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseHand : MonoBehaviour
{
    [SerializeField] private Image leftHand;
    [SerializeField] private Image rightHand;
    private string parentName;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Choose(int hand)
    {
        parentName = transform.parent.name; 
        string resourcePath = $"{parentName}";
        if (hand == 0)
        {
            rightHand.sprite = Resources.Load<Sprite>(resourcePath);
        }
        else if (hand == 1)
        {
            leftHand.sprite = Resources.Load<Sprite>(resourcePath);

        }
    }
}
