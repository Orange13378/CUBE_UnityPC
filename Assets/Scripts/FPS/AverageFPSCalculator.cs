using UnityEngine;

namespace CubeECS
{
    public class AverageFPSCalculator : MonoBehaviour
    {
        [SerializeField]
        private FPSCounter _fpsCounter;

        private float _totalFPS;
        private int _samplesCount;
        private float _currentFPS;

        private void Update()
        {
            _currentFPS = _fpsCounter.GetFPS();
            if (_currentFPS > 0)
            {
                _totalFPS += _currentFPS;
                _samplesCount++;
            }
        }

        public float GetAverageFPS()
        {
            return _samplesCount > 0 ? _totalFPS / _samplesCount : 0f;
        }
    }
}