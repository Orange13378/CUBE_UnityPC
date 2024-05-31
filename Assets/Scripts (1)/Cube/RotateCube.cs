using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 10;
    private float _z = 0;

    void Start()
    {
        //float z = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        _z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, _z);
    }
}
