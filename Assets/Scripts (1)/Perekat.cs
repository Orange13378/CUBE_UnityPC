using UnityEngine;

public class Perekat : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVelocity;

    public float x = 100f, y = 100f;
    private Vector3 beginPos = new Vector3(0, 0, 0); //начальная координата где стоит наш объект(поменяй на свои)
    public float moveSpeed = 1f;
    public float reloadTime = 5f, goingTime = 5f; // reloadTime это через сколько он опять появится, goingTime это сколько по времени он будет двигаться
    private bool breakTime = false, goTime = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        beginPos = new Vector3(x,y,0);
    }

    void Update()
    {
        if (goTime)
        {
            moveVelocity.x = 0.1f;
            moveVelocity.y = -0.1f;
            Vector2 pos = new Vector2(moveVelocity.x, moveVelocity.y);
            moveVelocity = pos.normalized * moveSpeed;
        }

        if (breakTime) 
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime < 0)
            {
                goTime = true;
                breakTime = false;
                reloadTime = 5f; //здесь поменять на свои значения по времени
                transform.position = beginPos; // перемщение на определенную позицию
            }
        }
        else
        {
            goingTime -= Time.deltaTime;
            if (goingTime < 0) 
            {
                breakTime = true;
                goTime = false;
                goingTime = 5f; //здесь поменять на свои значения по времени
            }
        }
        
    }

    void FixedUpdate()
    {
        if (goTime)
            rb.MovePosition(rb.position + moveVelocity * moveSpeed * Time.fixedDeltaTime);
    }
}