using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 3f;

    Rigidbody2D rb;
    public Animator animator;
    public PedestalUI pedestalUI;
    private Vector2 moveVelocity;
    //public GameObject minimap;
    public GameObject EscPanel;

    //bool check_M = false;
    //bool check_Esc = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PedestalUI pedestalUI = GetComponent<PedestalUI>();
    }

    void Update()
    {
        moveVelocity.x = Input.GetAxisRaw("Horizontal");
        moveVelocity.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", moveVelocity.x);
        animator.SetFloat("Vertical", moveVelocity.y);
        animator.SetFloat("Speed", moveVelocity.sqrMagnitude);

        Vector2 pos = new Vector2(moveVelocity.x, moveVelocity.y);
        moveVelocity = pos.normalized * moveSpeed;

        rb.MovePosition(rb.position + moveVelocity * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanel.SetActive(true);
            pedestalUI.Stop();
        }
    }
    
}
