using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, IShootable
{
    public Team team;

    [HideInInspector]
    public Weapon weapon;

    void Awake() {
        weapon = GetComponentInChildren<Weapon>();
    }

    void Start() {
        StartCoroutine(AlienShooting());
    }

    public Team GetTeam() {
        return team;
    }

    public void OnShot(Bullet bullet) {
        bullet.speed = 0;
        Destroy(this.gameObject);
    }

    IEnumerator AlienShooting() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(3,15));
            Shoot();
        }     
    }

    void Shoot() {
        weapon.ShootBullet();
    }
}
