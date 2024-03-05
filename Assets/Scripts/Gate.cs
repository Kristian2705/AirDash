using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public ParticleSystem leftFlame;
    public ParticleSystem rightFlame;
    public float maxBurnVolume = 0.5f;
    public float deactivateTimerAfter = 1f;
    private bool isMarkedForDeactivation;

    private AudioSource leftBurnSound;
    private AudioSource rightBurnSound;
    private Coroutine disableCoroutine;

    /// <summary>
    /// Cached gameObject of this Gate. Saves a lot of redundant operations.
    /// </summary>
    public GameObject GameObject { get; set; }

    /// <summary>
    /// Cached transform of this Gate. Saves a lot of redundant operations.
    /// </summary>
    public Transform Transform { get; set; }

    /// <summary>
    /// Do that method when the object activate for the first time.
    /// Here you can cached the important components of the object.
    /// </summary>
    public void Awake()
    {
        GameObject = gameObject;
        Transform = transform;
        leftBurnSound = leftFlame.GetComponent<AudioSource>();
        leftBurnSound.volume = maxBurnVolume;
        rightBurnSound = rightFlame.GetComponent<AudioSource>();
        rightBurnSound.volume = maxBurnVolume;
    }

    // When the object was disabled the event will trigger.
    // Right now we can stop the sound off to reduce the CPU load.
    public void OnDisable()
    {
        leftBurnSound.Pause();
        rightBurnSound.Pause();
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }
    }

    // When the object is active, we have Update loop.
    // When the gate is active we need to play the burning sound.
    // Play it only when is not played.
    public void LateUpdate()
    {
        if (!leftBurnSound.isPlaying)
        {
            leftBurnSound.Play();
        }

        if (!rightBurnSound.isPlaying)
        {
            rightBurnSound.Play();
        }
    }

    /// <summary>
    /// You can use the method to switch on and off the gameObject.
    /// Call it from GameManager.cs for more clear code.
    /// </summary>
    /// <param name="playing">Want to switch on(true) or switch off(false).</param>
    public void ToggleFlames(bool playing)
    {
        ParticleSystem.EmissionModule emission = leftFlame.emission;
        emission.enabled = playing;

        emission = rightFlame.emission;
        emission.enabled = playing;

        isMarkedForDeactivation = !playing;
        if (isMarkedForDeactivation)
        {
            disableCoroutine = StartCoroutine(DeactivateGateAfterTimeout());
        }
    }

    /// <summary>
    /// Coroutine which will wait some time and deactivate the gate object itself.
    /// Wait some time because we want to player doesn't see the deactivation after he passes the gate.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeactivateGateAfterTimeout()
    {
        yield return new WaitForSeconds(deactivateTimerAfter);
        GameObject.SetActive(false);
    }
}
