using UnityEngine;

public class CharcterAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioCollection[] AudioCollections;


    public void PlayClip(string clip)
    {
        audioSource.PlayOneShot(GetClip(clip));
    }

    public AudioClip GetClip(string clip)
    {
        for(int i =0; i< AudioCollections.Length; i++)
        {
            if (AudioCollections[i].name == clip)
            {
                return AudioCollections[i].clips[UnityEngine.Random.Range(0, AudioCollections[i].clips.Length)];
            }
        }

        Debug.Log("No clip with name " + clip);
        return null;
    }

    public void PlayHitSounds()
    {
        for (int i = 0; i < AudioCollections.Length; i++)
        {
            AudioClip clip = AudioCollections[i].clips[UnityEngine.Random.Range(0, AudioCollections[i].clips.Length)];
            SoundsManager.instance.PlaySoundEnemy(clip, this.transform.position);
        }
    }

    [SerializeField]
    public class AudioCollection
    {
        public string name;
        public AudioClip[] clips;
    }

}
