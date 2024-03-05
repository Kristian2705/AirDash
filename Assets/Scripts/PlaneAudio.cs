using UnityEngine;

public class PlaneAudio : MonoBehaviour
{
    public float minPitchEngine;
    private AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (GameManager.state == GameManager.GameState.Dead)
        {
            if (source.pitch >= minPitchEngine)
            {
                source.pitch -= Time.deltaTime;
            }
        }
    }
}
