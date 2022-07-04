using UnityEngine.Audio;
using UnityEngine;

public class GunAudioController : MonoBehaviour
{

    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip noAmmoClip;
    [SerializeField] private AudioClip reloadClip;

    private AudioSource gunSoundEffect;

    private void Awake()
    {
        gunSoundEffect = GetComponent<AudioSource>();
    }

    public void Play(string action = "shoot")
    {
        switch(action)
        {
            case "shoot":
                gunSoundEffect.clip = shootClip;
                break;
            case "no ammo":
                gunSoundEffect.clip = noAmmoClip;
                break;
            case "reload":
                gunSoundEffect.clip = reloadClip;
                break;
        }
        gunSoundEffect.Play();
    }
}
