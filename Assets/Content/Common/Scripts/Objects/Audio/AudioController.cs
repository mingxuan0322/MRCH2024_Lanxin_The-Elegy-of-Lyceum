using System.Collections;
using UnityEngine;

//Scripted by Prof. Zhang
public class AudioController : MonoBehaviour
{
    // Reference to the AudioSource component on this GameObject
    private AudioSource audioSource;

    // Fade duration for volume changes
    public float fadeDuration = 1.0f;

    // Target volume for fade-in and fade-out operations
    private float targetVolume = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Automatically gets the AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource is attached, throw a warning if not
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on this GameObject. AudioController requires an AudioSource component.");
        }
    }

    /// <summary>
    /// Plays the attached AudioSource with a fade-in effect.
    /// </summary>
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            StartCoroutine(FadeInRoutine(fadeDuration, targetVolume));
        }
    }

    /// <summary>
    /// Stops the attached AudioSource with a fade-out effect.
    /// </summary>
    public void StopAudio()
    {
        if (audioSource != null)
        {
            StartCoroutine(FadeOutRoutine(fadeDuration));
        }
    }

    /// <summary>
    /// Sets the volume of the attached AudioSource with a fade effect.
    /// </summary>
    /// <param name="volume">Target volume value between 0.0 and 1.0</param>
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            // Start fading to the new target volume
            StartCoroutine(FadeToVolumeRoutine(volume, fadeDuration));
        }
    }

    /// <summary>
    /// Coroutine to fade in the audio to the target volume.
    /// </summary>
    private IEnumerator FadeInRoutine(float duration, float targetVolume)
    {
        audioSource.volume = 0f;  // Start from volume 0
        audioSource.Play();       // Play the audio

        float startVolume = 0f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;  // Wait until the next frame
        }

        audioSource.volume = targetVolume;  // Ensure we set to target volume at the end
    }

    /// <summary>
    /// Coroutine to fade out the audio and stop it.
    /// </summary>
    private IEnumerator FadeOutRoutine(float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            yield return null;  // Wait until the next frame
        }

        audioSource.Stop();  // Stop the audio once faded out
        audioSource.volume = startVolume;  // Reset volume for next play
    }

    /// <summary>
    /// Coroutine to gradually change the volume to a target value.
    /// </summary>
    /// <param name="volume">Target volume</param>
    /// <param name="duration">Time taken to change the volume</param>
    private IEnumerator FadeToVolumeRoutine(float volume, float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, volume, timer / duration);
            yield return null;  // Wait until the next frame
        }

        audioSource.volume = volume;  // Ensure it reaches the target volume at the end
    }
}
