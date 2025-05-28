using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBase : MonoBehaviour
{
    public int scoreValue;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public Team GetTeam()
    {
        return Team.Aliens;
    }

    public virtual void OnShot(Bullet bullet)
    {
        bullet.speed = 0;
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        ScoreSystem.Instance.AddScore(scoreValue);
    }
}
