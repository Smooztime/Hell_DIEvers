using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source----")]
    [SerializeField] AudioSource musicScouce;
    [SerializeField] AudioSource SFXScouce;

    [Header("------ Audio Clip----")]
    public AudioClip Background;
    public AudioClip Walk;
    public AudioClip Jump;
    public AudioClip jumpRope;
    
    public object SFXSource { get; private set; }
    private void Start()
    {
        musicScouce.clip = Background;
        musicScouce.Play();
    }


   
    public void PlaySFX(AudioClip clip)
    {
     object value =  SFXSource.PlayOneShot(clip);
    }
}
