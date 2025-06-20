using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Aliens,
}
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Team team;
    public Vector2 direction;
    public float speed;
    Rigidbody2D rb;
    Animator animator;
    Collider2D coll;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    void OnEnable()
    {
        coll.enabled = true;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InvadedTrigger>())
            return;

        IShootable shootable = collision.GetComponent<IShootable>();
        if (shootable != null)
        {
            if (shootable.GetTeam() != team)
            {
                shootable.OnShot(this);
            }
            else
            {
                return;
            }
        }
        speed = 0;
        animator.SetTrigger("Hit");
        coll.enabled = false;
    }
}
