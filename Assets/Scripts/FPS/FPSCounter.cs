using UnityEngine;

namespace CubeECS
{
    public class FPSCounter : MonoBehaviour
    {
        private int _frameCount;
        private float _elapsedTime;
        private float _updateInterval;
        private float _fps;

        private void Update()
        {
            _frameCount++;
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _updateInterval)
            {
                _fps = _frameCount / _elapsedTime;
                _frameCount = 0;
                _elapsedTime = 0f;
            }
        }

        public float GetFPS()
        {
            return _fps;
        }
    }
}