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
    public Alien[,] aliens = new Alien[16, 5];
    void Awake()
    {
        Instance = this;
        SetMatrix();
        SetInitialShooting();
        StartCoroutine(Movement());
    }

    public void OnAlienDeath(Vector2Int matrixPos)
    {
        StopAllCoroutines();
        if (matrixPos.y + 1 >= aliens.GetLength(1))
        {
            return;
        }
        Alien nextAlien = aliens[matrixPos.x, matrixPos.y + 1];
        nextAlien.StartShooting();
    }

    void SetMatrix()
    {
        Alien[] alienGOs = GetComponentsInChildren<Alien>();
        float offsetX = 8;
        float offsetY = -0.25f;
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
        Vector2 direction = Vector2.right;
        while (true)
        {
            for (int i = 0; i < aliens.GetLength(1); i++)
            {
                for (int j = 0; j < aliens.GetLength(0); j++)
                {
                    aliens[j, i].MoveTo(direction, alienSpeed);
                }
                yield return new WaitForSeconds(movementDelay);
            }
            
        }
        
    }
}
