using UnityEngine;

namespace CubeECS
{
    [CreateAssetMenu(fileName = "Cube", menuName = "Cube/Cube")]
    public class CubeItem : ScriptableObject
    {
        public Vector3 Position;
        public PedestalWorld NextWorld;
        [TextArea] public string DialogText;
    }
}