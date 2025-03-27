using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum Team{
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
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        IShootable shootable = collision.GetComponent<IShootable>();
        if(shootable != null) { 
            if(shootable.GetTeam() != team) {
                animator.SetTrigger("Hit");
                shootable.OnShot(this);
            }           
            return;
        }
        speed = 0;
        animator.SetTrigger("Hit");
        //Destroy(this.gameObject);
    }
}
