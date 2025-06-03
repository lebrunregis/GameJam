using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] clips; // glisse les sons ici dans l'inspecteur
    public AudioSource audioSource;
    public float delay = 15f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            PlayRandomSound();
            timer = 0f;
        }
    }

    void PlayRandomSound()
    {
        if (clips.Length == 0) return;

        int index = Random.Range(0, clips.Length);
        audioSource.clip = clips[index];
        audioSource.Play();
    }
}
