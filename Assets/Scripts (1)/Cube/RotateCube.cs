using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 10;
    private float _z;

    private void Update()
    {
        _z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, _z);
    }
}
