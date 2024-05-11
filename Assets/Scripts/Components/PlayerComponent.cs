using UnityEngine;

namespace CubeECS
{
    public struct PlayerComponent
    {
        public Transform PlayerTransform;
        public Rigidbody2D PlayerRB;
        public BoxCollider2D PlayerCollider;
        public AudioSource PlayerAudioSource;
        public Animator PlayerAnimator;
        public float PlayerSpeed;
        public bool IsPlayerActive;
    }
}