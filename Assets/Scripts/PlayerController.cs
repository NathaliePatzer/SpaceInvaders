using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IShootable
{
    public Team team;
    public float speed = 10;
    float _speed;
    Vector2 movement;
    Rigidbody2D rb;
    Weapon weapon;
    Animator animator;
    public int lives = 3;
    Collider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        _speed = speed;
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime*speed);
    }

    public void Movement(InputAction.CallbackContext context) {
        //down
        if(context.performed) {
            movement = context.ReadValue<Vector2>();
        }
        //up
        else if(context.canceled) {
            movement = Vector2.zero;
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if(context.performed) {
            weapon.ShootBullet();
        }
    }

    public void OnShot(Bullet bullet) {
        bullet.speed = 0;
        lives--;
        animator.SetTrigger("Death");
        animator.SetInteger("Lives", lives);   
        StartCoroutine(Invencible());  
    }

    public Team GetTeam() {
        return team;
    }

    IEnumerator Invencible()
    {
        coll.enabled = false;
        speed = 0;
        yield return new WaitForSeconds(1.5f);
        coll.enabled = true;
        speed = _speed;
    }
}
