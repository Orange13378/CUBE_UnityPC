using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Inventory/ChestItem")]
public class ChestItem : ScriptableObject
{
    public int Id = 0;
    public int KeyId = 0;
    public Sprite Sprite;
    public string Success = "";
    public string Bad = "";
    public bool IsOpened = false;
    public bool IsUsed = false;
}
