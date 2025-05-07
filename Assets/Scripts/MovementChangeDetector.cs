using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementChangeDetector : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D coll) {
        if(coll.transform.name.Contains("wall")) {
            AlienController.Instance.direction *= -1;
        }
   }
}
