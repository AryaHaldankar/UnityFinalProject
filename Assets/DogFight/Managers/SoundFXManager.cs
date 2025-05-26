using UnityEngine;
using System.Collections.Generic;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip bgSound;
    [SerializeField] private AudioClip gunFire;
    [SerializeField] private AudioClip emptyGunClick;
    private Dictionary<string, AudioClip> sounds;
    private AudioSource bgSoundSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        sounds = new Dictionary<string, AudioClip>();
        sounds["Explosion"] = explosion;
        sounds["BgSound"] = bgSound;
        sounds["GunFire"] = gunFire;
        sounds["EmptyGunClick"] = emptyGunClick;
    }

    // Update is called once per frame
    public void PlaySoundFXClip(string clipName, Transform spawnPoint, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = sounds[clipName];
        audioSource.volume = volume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayBgSound(float volume)
    {
        bgSoundSource = Instantiate(soundFXObject);
        bgSoundSource.clip = sounds["BgSound"];
        bgSoundSource.volume = volume;
        bgSoundSource.loop = true;
        bgSoundSource.Play();
    }

    public void StopBackgroundSound()
    {
        bgSoundSource.Stop();
    }
}
