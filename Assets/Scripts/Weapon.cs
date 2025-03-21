using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;
    public Vector2 bulletDirection;
    public float bulletSpeed;
    GameObject bulletHolder;
    IShootable shootable;

    private void Awake() {
        bulletHolder = GameObject.Find("BulletHolder");
        shootable = GetComponentInParent<IShootable>();
    }
    public void ShootBullet() {
        Bullet instantieted = Instantiate(bullet, transform.position, Quaternion.identity, bulletHolder.transform);
        instantieted.direction = bulletDirection;
        instantieted.speed = bulletSpeed;
        instantieted.team = shootable.GetTeam();
        instantieted.enabled = true;
    }
}
