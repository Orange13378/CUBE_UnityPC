using UnityEngine;

namespace CubeMVC
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private float playerSpeed = 5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        public Rigidbody2D GetRigidBody2D()
        {
            return _rb;
        }

        public void Move(Vector2 moveInput)
        {
            var newPosition = _rb.position + moveInput * playerSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }
    }
}