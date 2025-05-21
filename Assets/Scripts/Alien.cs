using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, IShootable
{
    public Team team;

    [HideInInspector]
    public Weapon weapon;
    Animator animator;
    public Vector2Int matrixPos;
    Rigidbody2D rb;

    void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public Team GetTeam()
    {
        return team;
    }

    public void OnShot(Bullet bullet)
    {
        bullet.speed = 0;
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        AlienController.Instance.OnAlienDeath(matrixPos);
    }

    public void StartShooting()
    {
        StartCoroutine(AlienShooting());
    }

    public void MoveTo(Vector2 direction, float speed)
    {
        rb.MovePosition(rb.position + direction * speed);
        animator.SetTrigger("Move");
    }

    IEnumerator AlienShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 15));
            Shoot();
        }
    }

    void Shoot()
    {
        weapon.ShootBullet();
    }
}
