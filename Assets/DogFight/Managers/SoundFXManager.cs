using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    public void playSoundFXClip(AudioClip audioClip, Transform spawnPoint, float volume){
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }

    public void playSoundFX(AudioClip audioClip, Transform spawnPoint, float volume){
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }
}
