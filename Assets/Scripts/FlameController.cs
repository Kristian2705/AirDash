using System;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public ParticleSystem leftFlame;
    public ParticleSystem rightFlame;

    public float maxBurnVolume = 0.5f;

    private AudioSource leftBurnSound;
    private AudioSource rightBurnSound;

    public bool burnSoundsPlaying;
    public float burnVolumeDownRate = 0.0001f;

    public void Awake()
    {
        leftBurnSound = leftFlame.GetComponent<AudioSource>();
        rightBurnSound = rightFlame.GetComponent<AudioSource>();
        leftBurnSound.Stop();
        rightBurnSound.Stop();
    }

    public void LateUpdate()
    {
        burnVolumeDownRate = 0.003f;
        if (burnSoundsPlaying)
        {
            leftBurnSound.volume = Mathf.Clamp(leftBurnSound.volume + burnVolumeDownRate, 0, maxBurnVolume);
            rightBurnSound.volume = Mathf.Clamp(rightBurnSound.volume + burnVolumeDownRate, 0, maxBurnVolume);
            if (rightBurnSound.volume > 0 && !rightBurnSound.isPlaying || !leftBurnSound.isPlaying)
            {
                leftBurnSound.Play();
                rightBurnSound.Play();
            }
        }
        else
        {
            leftBurnSound.volume = Mathf.Clamp(leftBurnSound.volume - burnVolumeDownRate, 0, maxBurnVolume);
            rightBurnSound.volume = Mathf.Clamp(rightBurnSound.volume - burnVolumeDownRate, 0, maxBurnVolume);
            if (rightBurnSound.volume == 0 && rightBurnSound.isPlaying || leftBurnSound.isPlaying)
            {
                leftBurnSound.Stop();
                rightBurnSound.Stop();
            }
        }
    }

    public void ToggleFlames(bool playing)
    {
        ParticleSystem.EmissionModule emission = leftFlame.emission;
        emission.enabled = playing;

        emission = rightFlame.emission;
        emission.enabled = playing;

        burnSoundsPlaying = playing;
    }
}
