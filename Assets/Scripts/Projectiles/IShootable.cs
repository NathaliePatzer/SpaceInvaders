using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void OnShot(Bullet bullet);
    Team GetTeam();
}
