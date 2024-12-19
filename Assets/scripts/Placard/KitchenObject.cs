using UnityEngine;

[CreateAssetMenu(fileName = "NewKitchenObject", menuName = "KitchenObject")]
public class KitchenObject : ScriptableObject
{
    public GameObject prefab;
    public string description;
    public Sprite image;
}
