using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;
    public Vector2 bulletDirection;
    public float bulletSpeed;
    public float cooldown;
    float lastShot;
    GameObject bulletHolder;
    IShootable shootable;
    AudioSource audioSource;

    private void Awake()
    {
        bulletHolder = GameObject.Find("BulletHolder");
        shootable = GetComponentInParent<IShootable>();
        audioSource = GetComponent<AudioSource>();
    }
    public void ShootBullet()
    {
        if (Time.time - lastShot < cooldown)
        {
            return;
        }
        lastShot = Time.time;
        Bullet instantieted = Instantiate(bullet, transform.position, Quaternion.identity, bulletHolder.transform);
        instantieted.direction = bulletDirection;
        instantieted.speed = bulletSpeed;
        instantieted.team = shootable.GetTeam();
        instantieted.enabled = true;

        if (audioSource != null)
        {
            audioSource.Play();
        }

    }
}
