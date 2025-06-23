using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private GameObject backgroundMusic, bossMusic;
    [SerializeField] private float fadeOutTime = 3f, fadeInTime = 3f;

    private AudioSource backgroundAudio, bossAudio;
    private float fadeOutTimer, fadeInTimer;
    private bool isTriggered = false;
    private float backgroundStartVolume, bossStartVolume;
    private bool bossStarted = false;

    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
        bossMusic = GameObject.FindGameObjectWithTag("BossMusic");
        backgroundAudio = backgroundMusic.GetComponent<AudioSource>();
        bossAudio = bossMusic.GetComponent<AudioSource>();

        backgroundStartVolume = backgroundAudio.volume;
        bossStartVolume = bossAudio.volume;

        fadeOutTimer = fadeOutTime;
        fadeInTimer = fadeInTime;

        // Start boss audio muted and inactive
    }

    void Update()
    {
        if (!isTriggered) return;

        if (fadeOutTimer > 0f)
        {
            float t = 1f - (fadeOutTimer / fadeOutTime);
            backgroundAudio.volume = backgroundStartVolume * Mathf.Pow(10f, -2f * t);
            fadeOutTimer -= Time.deltaTime;
        }
        else
        {
            if (!bossStarted)
            {
                backgroundAudio.Stop();
                bossAudio.Play();
                bossAudio.volume = 0f;
                bossStarted = true;
            }

            if (fadeInTimer > 0f)
            {
                float t = fadeInTimer / fadeInTime;
                bossAudio.volume = bossStartVolume * Mathf.Pow(10f, -2f * t);
                fadeInTimer -= Time.deltaTime;
            }
            else
            {
                bossAudio.volume = bossStartVolume;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }
}
