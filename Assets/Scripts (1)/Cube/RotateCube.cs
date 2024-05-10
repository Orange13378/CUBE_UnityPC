using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 10;
    float z = 0;
    // Start is called before the first frame update
    void Start()
    {
        //float z = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
