using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileScript : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private Rigidbody body;
    [SerializeField] private GameObject cameraAnchor;

    private Vector3 anchorOffset;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource collectSound;
    
    private float forceFactor = 500f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        anchorOffset = this.transform.position - cameraAnchor.transform.position;
        if (!LabirinthState.isSoundsMuted)
        {
            backgroundMusic.volume = LabirinthState.musicVolume;
            backgroundMusic.Play();
        }
        LabirinthState.OnSoundsMuteChanged += SoundsMuteChanged;
        LabirinthState.OnMusicVolumeChanged += MusicVolumeChanged;
        LabirinthState.OnEffectsVolumeChanged += EffectsVolumeChanged;
    }

    private void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");

        Vector3 right = _camera.transform.right;
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 forceDirection = // new Vector3(kh, 0, kv);
            kh * right + kv * forward;

        body.AddForce(forceFactor * Time.deltaTime * forceDirection.normalized);

        cameraAnchor.transform.position = this.transform.position - anchorOffset;

        //if(backgroundMusic.volume != LabirinthState.musicVolume)
        //{
        //    backgroundMusic.volume = LabirinthState.musicVolume;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            if (!LabirinthState.isSoundsMuted)
            {
                // collectSound.volume = LabirinthState.effectsVolume;
                collectSound.Play();
            }
        }
    }

    public void SoundsMuteChanged()
    {
        backgroundMusic.mute = LabirinthState.isSoundsMuted;
    }

    public void MusicVolumeChanged()
    {
        backgroundMusic.volume = LabirinthState.musicVolume;
    }

    public void EffectsVolumeChanged()
    {
        collectSound.volume = LabirinthState.effectsVolume;
    }
}
