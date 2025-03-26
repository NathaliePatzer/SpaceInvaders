using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, IShootable
{
    public Team team;

    public Team GetTeam() {
        return team;
    }

    public void OnShot(Bullet bullet) {
        if(bullet.team != team) {
            Destroy(this.gameObject);
        }
    }
}
