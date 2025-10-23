using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    private float lastTime = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayMusic()
    {
        audioSource.PlayScheduled(10);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public bool MusicEnded()
    {
        return audioSource.time >= audioSource.clip.length;
    }

    public float GetDeltaTime()
    {
        float returnVal = audioSource.time - lastTime;
        lastTime = audioSource.time;
        return returnVal;
    }

    void Update()
    {
        if(MusicEnded() && GameManager.Instance.IsPlaying)
        {
            GameManager.Instance.EndGame();
        }
    }
}
