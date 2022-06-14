using UnityEngine;

public static class AudioPlayer
{
    public static void Play(AudioClip clip, bool loopClip) {
        GameObject g = new GameObject("Audio Player", typeof(AudioSource));
        AudioSource audioSource = g.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loopClip;
        audioSource.Play();

        if (!loopClip)
            Object.Destroy(g, clip.length);
    }
}