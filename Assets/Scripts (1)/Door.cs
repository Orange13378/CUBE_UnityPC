using CubeECS;
using Leopotam.EcsLite;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject door, codePanel, panel;
    private bool entered, close;

    private EcsFilter _playerInputFilter;
    private EcsPool<PlayerInputComponent> _playerInputPool;

    [System.NonSerialized] public static bool correct;
    void Start()
    {
        correct = false;
        entered = false;
        close = true;

        var world = EcsWorldManager.GetEcsWorld();
        _playerInputFilter = world.Filter<PlayerInputComponent>().End();
        _playerInputPool = world.GetPool<PlayerInputComponent>();
    }

    private void Update()
    {
        if (entered)
        {
            foreach (var entity in _playerInputFilter)
            {
                ref var playerInputComponent = ref _playerInputPool.Get(entity);
                if (playerInputComponent.PressedX && !correct)
                    codePanel.SetActive(true);
            }
        }

        if (correct & close) 
        {
            doorAnim.SetBool("open", true);
            panel.SetActive(false);
            StartCoroutine(Open());
            close = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            entered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            entered = false;
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(4f);
        door.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}
