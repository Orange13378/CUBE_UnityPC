using UnityEngine;

namespace CubeMVC
{
    public class FootstepsModel
    {
        public AudioClip[] FootSteps;
        public float Timer;

        public FootstepsModel(AudioClip[] footSteps)
        {
            FootSteps = footSteps;
        }
    }
}