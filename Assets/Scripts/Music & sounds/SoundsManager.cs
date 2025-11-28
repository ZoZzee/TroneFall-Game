using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]private float defaultDistance;
    public float audioDistance = 0;
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private GameObject musicPrefab;

    private Vector3 nextSoundPosition;

    public static SoundsManager instance;

    private List<GameObject> soundsPlayingNow = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = GetAudioobject(position).GetComponent<AudioSource>();
        InializeAudioSource(audioSource, clip);

        Destroy(audioSource.gameObject, clip.length);
    }
    public void PlayMusic(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = GetAudioobject(position).GetComponent<AudioSource>();

        InializeAudioSource(audioSource, clip);

        Destroy(audioSource.gameObject, clip.length);
    }
     
    public void PlaySoundEnemy(AudioClip clip, Vector3 position)
    {
        if(soundsPlayingNow.Count <=1)
        {
            AudioSource audioSource = GetAudioobject(position).GetComponent<AudioSource>();
            InializeAudioSource(audioSource, clip);

            soundsPlayingNow.Add(audioSource.gameObject);
            Invoke(nameof(DestroyAudioClip),clip.length);
        }
    }

    private void DestroyAudioClip(GameObject toDestroy)
    {
        soundsPlayingNow.Remove(toDestroy);
        Destroy(toDestroy);
    }

    public void SetNextPosition(Transform newTransform)
    {
        nextSoundPosition = newTransform.position;
    }

    public void PlayClipOnPosition(AudioClip clip)
    {
        PlaySound(clip, nextSoundPosition);
        nextSoundPosition = Vector3.zero;
    }
    public void PlayMusicClip(AudioClip clip)
    {
        PlaySound(clip, nextSoundPosition);
        nextSoundPosition = Vector3.zero;
    }

    private void InializeAudioSource(AudioSource audioSource, AudioClip clip)
    {
        if (audioDistance != 0)
        {
            audioSource.maxDistance = audioDistance;
            audioDistance = 0;
        }
        else
        {
            audioSource.maxDistance = defaultDistance;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    private GameObject GetAudioobject(Vector3 position)
    {
        return Instantiate(soundPrefab, position, Quaternion.identity, null);
    }
}
