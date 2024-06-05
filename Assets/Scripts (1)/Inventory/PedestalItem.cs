using UnityEngine;

[CreateAssetMenu(fileName = "Pedestal", menuName = "Pedestal/Item")]
public class PedestalItem : ScriptableObject
{
    public int Id = 0;
    public Sprite Sprite = null;
    public Sprite CubeSprite = null;
    [TextArea] public string Text;
}
