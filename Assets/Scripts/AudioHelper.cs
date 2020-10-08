using UnityEngine;

public static class AudioHelper 
{
public static void CreateAudioObject(AudioClip audioClip, string name, Vector3 position)
    {
        var audioObject = new GameObject();
        audioObject.name = name;
        audioObject.transform.position = position;
        var audioSource = audioObject.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        var lifeTime = audioObject.gameObject.AddComponent<LifeTime>();
        lifeTime.lifeTimeSeconds = audioSource.clip.length;
    }
}
