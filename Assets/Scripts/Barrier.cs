using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    int lives = 5;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.GetComponent<Bullet>()) {
            lives--;
            if (lives <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
