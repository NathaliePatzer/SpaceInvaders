using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    int lives = 15;
    public Sprite[] damageSprites;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Bullet>())
        {
            lives--;
            UpdateSprite();
            
            if (lives <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void UpdateSprite()
    {
        int index = 0;

        if (lives <= 5)
            index = 2;
        else if (lives <= 10)
            index = 1;
        else
            index = 0;

        if (damageSprites != null && damageSprites.Length > index)
        {
            spriteRenderer.sprite = damageSprites[index];
        }
    }
    
}
