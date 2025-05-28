using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public static BGMController Instance;
    public const string PitchParameter = "PitchValue";
    public float increaseAmount;
    float increased = 1;
    AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    [ContextMenu("Increase")]
    public void IncreaseSpeed()
    {
        increased += increaseAmount;
        audioSource.pitch = increased;
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat(PitchParameter, 1 / increased);
    }
    void OnDisable()
    {
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat(PitchParameter, 1);
    }
}
