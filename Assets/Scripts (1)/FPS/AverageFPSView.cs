using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class AverageFPSView : MonoBehaviour
    {
        [SerializeField]
        private AverageFPSCalculator _averageFPSCalculator;

        [SerializeField]
        private Text _fpsDisplayText;

        private void Update()
        {
            if (_averageFPSCalculator != null && _fpsDisplayText != null)
            {
                _fpsDisplayText.text = "FPS: " + _averageFPSCalculator.GetAverageFPS().ToString("F2");
            }
        }
    }
}