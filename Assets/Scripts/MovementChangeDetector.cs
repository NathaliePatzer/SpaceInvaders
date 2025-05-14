using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementChangeDetector : MonoBehaviour
{
    public Vector2Int direction;
    public Transform wall;
    void OnTriggerEnter2D(Collider2D coll)
    {
        Bullet bullet;
        if (coll.transform == wall)
        {
            AlienController.Instance.direction = direction;
        }
        if (coll.TryGetComponent<Bullet>(out bullet) && bullet.team == Team.Player)
        {
            WhenDisabled();
        }

    }
    void WhenDisabled()
    {
        Alien alien = GetComponent<Alien>();
        int y = alien.matrixPos.y + 1;

        for (int x = alien.matrixPos.x; x > -1 && x < AlienController.Instance.aliens.GetLength(0); x += direction.x)
        {
            for (; y < AlienController.Instance.aliens.GetLength(1); y++)
            {
                if (AlienController.Instance.aliens[x, y] != null)
                {
                    MovementChangeDetector detector = AlienController.Instance.aliens[x, y].gameObject.AddComponent<MovementChangeDetector>();
                    detector.direction = direction;
                    detector.wall = wall;
                    this.enabled = false;
                    return;
                }
            }
            y = 0;
        }

    }
}
