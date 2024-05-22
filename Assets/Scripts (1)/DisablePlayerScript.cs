using UnityEngine;

public class DisablePlayerScript : MonoBehaviour
{
    public GameObject player, footsteps;

    public static bool on = false, off = false, antiMouse = false;
    public AnimationClip a;

    void Update()
    {
        if(on)
        {
            on = false;
            On();
        }

        if(off)
        {
            off = false;
            Off();
        }
    }

    public void Off()
    {
        if(!antiMouse)
        {
            player.gameObject.GetComponent<Player1>().enabled = false;
            Animator animator = player.gameObject.GetComponent<Animator>();
            animator.SetBool("Stoped", true);
            footsteps.gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void On()
    {
        if(!antiMouse)
        {
            player.gameObject.GetComponent<Player1>().enabled = true;
            Animator animator = player.gameObject.GetComponent<Animator>();
            animator.SetBool("Stoped", false);
            footsteps.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}

namespace CubeECS
{
}