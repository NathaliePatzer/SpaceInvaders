using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class AlienController : MonoBehaviour
{
    public static AlienController Instance;
    public float alienSpeed = 0.1f;
    public float movementDelay = 0.1f;
    public Queue<Vector2> direction;
    public Alien[,] aliens = new Alien[15, 5];
    void Awake()
    {
        direction = new Queue<Vector2>();
        direction.Enqueue(Vector2.right);
        Instance = this;
        SetMatrix();
        SetInitialShooting();
        StartCoroutine(Movement());
        StartCoroutine(SpeedIncrease());
    }

    public void OnAlienDeath(Vector2Int matrixPos)
    {
        Alien nextAlien = null;
        for (int i = matrixPos.y + 1; i < aliens.GetLength(1) && nextAlien == null; i++)
        {
            nextAlien = aliens[matrixPos.x, i];
        }
        if (nextAlien == null)
        {
            return;
        }

        nextAlien.StartShooting();
    }

    void SetMatrix()
    {
        Alien[] alienGOs = GetComponentsInChildren<Alien>();
        float offsetX = 8;
        float offsetY = -1.44f; //valor do Y do alien mais inferior à esquerda 
        foreach (Alien a in alienGOs)
        {
            int xPos = Mathf.FloorToInt(a.transform.localPosition.x + offsetX);
            int yPos = Mathf.FloorToInt((a.transform.localPosition.y + offsetY) / 0.75f);
            aliens[xPos, yPos] = a;
            a.matrixPos = new Vector2Int(xPos, yPos);
            //Debug.LogFormat("Alien {0} is in position ({1},{2})", a.name, xPos, yPos);
        }
    }

    void SetInitialShooting()
    {
        for (int i = 0; i < aliens.GetLength(0); i++)
        {
            aliens[i, 0].StartShooting();
        }

    }

    IEnumerator Movement()
    {

        while (true)
        {
            Vector2 currentDirection = direction.Dequeue();
            if (direction.Count == 0)
            {
                direction.Enqueue(currentDirection);
            }

            for (int i = 0; i < aliens.GetLength(1); i++)
            {
                for (int j = 0; j < aliens.GetLength(0); j++)
                {
                    if (aliens[j, i] != null)
                    {
                        aliens[j, i].MoveTo(currentDirection, alienSpeed);
                    }
                }
                yield return new WaitForSeconds(movementDelay);
            }

        }

    }

    IEnumerator SpeedIncrease()
    {
        while (movementDelay > 0.001f)
        {
            movementDelay -= 0.00001f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
