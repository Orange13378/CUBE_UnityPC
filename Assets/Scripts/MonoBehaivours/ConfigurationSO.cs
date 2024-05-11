using UnityEngine;

namespace CubeECS
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class ConfigurationSO : ScriptableObject
    {
        public float PlayerSpeed;
        public float CameraFollowSmoothness;
    }
}