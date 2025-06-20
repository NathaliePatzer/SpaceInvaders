using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class AlienController : MonoBehaviour
{
    public static AlienController Instance;
    public float alienSpeed = 0.1f;
    public float movementDelay = 0.1f;
    public Queue<Vector2> direction;
    public Alien[,] aliens = new Alien[15, 5];
    public SpecialAlien specialAlienPrefab;
    public List<Transform> walls;
    int remainingAliens;
    void Awake()
    {
        direction = new Queue<Vector2>();
        direction.Enqueue(Vector2.right);
        remainingAliens = aliens.GetLength(0) * aliens.GetLength(1);
        Instance = this;
        SetMatrix();
        SetInitialShooting();
    }
    void Start()
    {
        StartCoroutine(Movement());
        StartCoroutine(SpecialAlienRoutine());
    }

    public void OnAlienDeath(Vector2Int matrixPos)
    {
        Alien nextAlien = null;
        remainingAliens--;
        movementDelay -= 0.0025f;
        BGMController.Instance.IncreaseSpeed();

        if (remainingAliens <= 0)
        {
            GameOver.Instance.OnGameOver(1000);
        }

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

    IEnumerator SpecialAlienRoutine()
    {
        while (true)
        {
            int rand = Random.Range(-3, 7);
            yield return new WaitForSeconds(10 + rand);
            int side = Random.Range(0, 2);
            int otherside = 1 - side;
            Vector3 position = new Vector3(walls[side].position.x, 4.0f, 0);
            SpecialAlien specialAlien = Instantiate(specialAlienPrefab, position, Quaternion.identity, this.transform);
            specialAlien.StartMove(walls[otherside]);
        }
    }
}
