using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadedTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Alien>())
            GameOver.Instance.OnGameOver(100);
    }
}
