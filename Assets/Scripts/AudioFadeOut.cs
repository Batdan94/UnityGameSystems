using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFadeOut {

    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime = 1f)
    {
        float startVolume = audioSource.volume;
        audioSource.Play();
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume / 15;
            Debug.Log(audioSource.volume.ToString());
            yield return new WaitForSeconds(fadeTime/10);
        }
        Debug.Log("Successful fadeout"); 
        audioSource.Stop();
        audioSource.volume = startVolume;
        yield return null;
    }
}
