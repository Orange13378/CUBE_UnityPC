using System.Collections;
using UnityEngine;
using DG.Tweening;
using CubeMVC;

public class MoveBlocks : MonoBehaviour
{
    [SerializeField] Vector3 nextPos = new ();

    private bool moved, pressedE, entered, once;
    Vector3 startPos;
    [SerializeField] private GameObject player, underPlite1, underPlite2;
    [SerializeField] private Sprite sprite_goPlite, sprite_backPlite, spriteN , spriteS;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _underPliteSpriteRenderer;
    private SpriteRenderer _underPlite2SpriteRenderer;
    private EdgeCollider2D _edgeCollider;

    [SerializeField]
    private ContextProvider _contextProvider;
    private PlayerInputModel _inputModel;

    public static bool sprite;
    
    private void Start()
    {
        startPos = gameObject.transform.position;
        moved = true;
        pressedE = false;
        entered = false;
        once = true;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _underPliteSpriteRenderer = underPlite1.GetComponent<SpriteRenderer>();
        _underPlite2SpriteRenderer = underPlite2.GetComponent<SpriteRenderer>();
        _edgeCollider = gameObject.GetComponent<EdgeCollider2D>();

        _inputModel = _contextProvider.GetContext().PlayerInputModel;
    }

    private void Update()
    {
        if (!_inputModel.PressedX.Value)
            return;

        if (entered && once)
        {
            StartCoroutine(Plite());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
        }
    }

    private IEnumerator Plite()
    {
        once = false;
        pressedE = !pressedE;
        if(moved)
        {
            if (pressedE)
            {
                _spriteRenderer.sprite = sprite_goPlite;
                _edgeCollider.enabled = true;
                gameObject.transform.DOLocalMove(startPos + Vector3.up, 1f);
                player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 1f);
                yield return new WaitForSeconds(1f);
                once = true;
                sprite = true;
                yield return new WaitUntil((() => MagnitMech.magnit));
                _underPliteSpriteRenderer.sprite = spriteN;
                _underPlite2SpriteRenderer.sprite = spriteS;
                once = false;
                if (pressedE)
                {
                    sprite = false;
                    player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 5f);
                    gameObject.transform.DOLocalMove(nextPos + Vector3.up, 5f);
                    yield return new WaitForSeconds(5f);
                    gameObject.transform.DOLocalMove(nextPos, 1f);
                    player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position), 1f);
                    yield return new WaitForSeconds(1f);
                    _spriteRenderer.sprite = sprite_backPlite;
                    _edgeCollider.enabled = false;
                    moved = !moved;
                    pressedE = !pressedE;
                }
                once = true;
            }
            else 
            {
                player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position), 1f);
                gameObject.transform.DOLocalMove(startPos, 1f);
                yield return new WaitForSeconds(1f);
                once = true;
                _spriteRenderer.sprite = sprite_backPlite;
                _edgeCollider.enabled = false;
            }
        }
        else
        {
            if (pressedE)
            {
                sprite = true;
                _spriteRenderer.sprite = sprite_goPlite;
                _edgeCollider.enabled = true;
                gameObject.transform.DOLocalMove(nextPos + Vector3.up, 1f);
                player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 1f);
                yield return new WaitForSeconds(1f);
                once = true;
                yield return new WaitUntil((() => !MagnitMech.magnit));
                _underPliteSpriteRenderer.sprite = spriteS;
                _underPlite2SpriteRenderer.sprite = spriteN;
                sprite = false;
                once = false;
                if (pressedE)
                {
                    player.transform.DOLocalMove(startPos + (player.transform.position  - gameObject.transform.position + Vector3.up), 5f);
                    gameObject.transform.DOLocalMove(startPos + Vector3.up, 5f);
                    yield return new WaitForSeconds(5f);
                    gameObject.transform.DOLocalMove(startPos, 1f);
                    player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position), 1f);
                    yield return new WaitForSeconds(1f);
                    _spriteRenderer.sprite = sprite_backPlite;
                    _edgeCollider.enabled = false;
                    moved = !moved;
                    pressedE = !pressedE;
                    
                }
                once = true;
            }
            else 
            {
                player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position), 1f);
                gameObject.transform.DOLocalMove(nextPos, 1f);
                yield return new WaitForSeconds(1f);
                once = true;
                _spriteRenderer.sprite = sprite_backPlite;
                _edgeCollider.enabled = false;
            }
        }
        
    }

}
