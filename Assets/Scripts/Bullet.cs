using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerController>()) {
            return;
        }
        Destroy(this.gameObject);
    }
}
