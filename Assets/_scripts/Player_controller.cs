using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    float MvSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove > 0.1 || horizontalMove < -0.1 || verticalMove > 0.1 || verticalMove < -0.1) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        if (horizontalMove < -0.2f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (horizontalMove > 0.2f) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        rb.velocity = new Vector2(horizontalMove, verticalMove).normalized * MvSpeed;
    }
}
