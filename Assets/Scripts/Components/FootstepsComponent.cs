using UnityEngine;

namespace CubeECS
{
    public struct FootstepsComponent
    {
        public AudioSource AudioSource;
        public AudioClip[] FootSteps;
        public float Timer;
    }
}