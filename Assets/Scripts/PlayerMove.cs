using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GlobalVariables vars;
    Rigidbody2D rb;
    Animator animator;
    Vector2 direction;
    bool talking;

    public float speedMultiplier;

    void Start()
    {
        vars = FindObjectOfType<GlobalVariables>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction = Vector2.zero;
    }

    void Update()
    {
        if (vars.talking || direction == Vector2.zero)
        {
            direction = Vector2.zero;
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("Moving", false);
        }
        else animator.SetBool("Moving", true);
        if (vars.talking) return;

        if (Input.GetKey("a")) direction.Set(-1, direction.y);
        else if (Input.GetKey("d")) direction.Set(1, direction.y);
        else direction.Set(0, direction.y);
        if (Input.GetKey("w")) direction.Set(direction.x, 1);
        else if (Input.GetKey("s")) direction.Set(direction.x, -1);
        else direction.Set(direction.x, 0);

        Move();
        Animate();

        if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f || Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
        {
            animator.SetFloat("LastX", Input.GetAxis("Horizontal"));
            animator.SetFloat("LastY", Input.GetAxis("Vertical"));
        }
    }

    void Move()
    {
        rb.velocity = direction.normalized * speedMultiplier;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}