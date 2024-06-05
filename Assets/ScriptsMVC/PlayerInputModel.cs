using UniRx;
using UnityEngine;

namespace CubeMVC
{
    public class PlayerInputModel
    {
        public ReactiveProperty<float> X { get; } = new(0);
        public ReactiveProperty<float> Y { get; } = new(0);
        public ReactiveProperty<bool> PressedX { get; } = new(false);
        public ReactiveProperty<Vector2> CurrentPosition { get; } = new(new (-29.9699993f, -4.44000006f));

        public ReactiveProperty<bool> IsPlayerActive { get; } = new(true);

        public void UpdateInput(float x, float y)
        {
            X.Value = x;
            Y.Value = y;
        }
    }

    public class PlayerAnimationModel
    {
        public Animator Animator { get; set; }

        public PlayerAnimationModel(Animator animator)
        {
            Animator = animator;
        }
    }
}