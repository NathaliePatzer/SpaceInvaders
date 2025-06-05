using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesHearts : MonoBehaviour
{
    public static LivesHearts Instance;

    public Animator[] heartAnimators;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateHearts(int livesLeft)
    {
        for (int i = 0; i < heartAnimators.Length; i++)
        {
            if (i >= livesLeft)
            {
                heartAnimators[i].SetTrigger("Disappear");
                Debug.Log("Trigger enviado para coração " + i);
            }
        }
    }
    
}
