using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public float defaultDistance;
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private AudioSource audioSource;

    private Vector3 nextSoundPosition;

    public static SoundsManager instance;

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

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(soundPrefab, position, Quaternion.identity, null).GetComponent<AudioSource>();

        audioSource.maxDistance = defaultDistance;

        audioSource.clip = clip;
        audioSource.Play();

        Destroy(audioSource.gameObject, clip.length);
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
}
