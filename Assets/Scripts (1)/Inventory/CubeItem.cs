using CubeMVC;
using UnityEngine;

[CreateAssetMenu(fileName = "Cube", menuName = "Cube/Cube")]
public class CubeItem : ScriptableObject
{
    public int Id = 0;
    public Vector3 Position;
    public PedestalWorld NextWorld;
    [TextArea] public string DialogText;
}
