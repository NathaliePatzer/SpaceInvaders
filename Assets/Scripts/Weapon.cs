using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;
    public Vector2 bulletDirection;
    public float bulletSpeed;
    GameObject bulletHolder;

    private void Awake() {
        bulletHolder = GameObject.Find("BulletHolder");
    }
    public void ShootBullet() {
        Bullet instantieted = Instantiate(bullet, transform.position, Quaternion.identity, bulletHolder.transform);
        instantieted.direction = bulletDirection;
        instantieted.speed = bulletSpeed;
        instantieted.enabled = true;
    }
}
