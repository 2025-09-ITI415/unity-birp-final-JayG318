using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class AudioZone : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeTime = 1.0f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float zoneVolume = 0.35f;

    float targetVolume = 0f;

    void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        audioSource.volume = Mathf.MoveTowards(
            audioSource.volume,
            targetVolume,
            Time.deltaTime / Mathf.Max(0.0001f, fadeTime)
        );

        // Stop once fully faded out
        if (targetVolume <= 0f && audioSource.isPlaying && audioSource.volume <= 0.0001f)
            audioSource.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        targetVolume = zoneVolume;

        if (!audioSource.isPlaying)
        {
            audioSource.volume = 0f;
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        targetVolume = 0f;
    }
}
