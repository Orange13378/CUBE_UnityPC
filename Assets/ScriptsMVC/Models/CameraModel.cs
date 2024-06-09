using UniRx;
using UnityEngine;

namespace CubeMVC
{
    public class CameraModel
    {
        public ReactiveProperty<Vector2> TargetPosition { get; private set; }
        public ReactiveProperty<Vector2> CurrentVelocity { get; private set; }

        public CameraModel(Vector2 initialPosition)
        {
            TargetPosition = new ReactiveProperty<Vector2>(initialPosition);
            CurrentVelocity = new ReactiveProperty<Vector2>(Vector2.zero);
        }

        public void UpdateTargetPosition(Vector2 newTargetPosition)
        {
            TargetPosition.Value = newTargetPosition;
        }
    }
}