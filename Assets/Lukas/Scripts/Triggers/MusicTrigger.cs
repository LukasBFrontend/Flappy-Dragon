using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private GameObject backgroundMusic, bossMusic;
    [SerializeField] private float fadeOutTime = 3f, fadeInTime = 3f;

    private AudioSource backgroundAudio, bossAudio;
    private float fadeOutTimer, fadeInTimer;
    private static bool isTriggered, bossStarted, musicFaded = false;
    private float backgroundStartVolume, bossStartVolume;

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

        if (LogicScript.Instance.GetRespawn().x == 0f)
        {
            if (isTriggered)
            {
                bossAudio.volume = 0f;
                backgroundAudio.volume = backgroundStartVolume;
                bossAudio.Stop();
                backgroundAudio.Play();
            }

            isTriggered = false;
            bossStarted = false;
            musicFaded = false;
        }
    }

    void Update()
    {

        if (!isTriggered || musicFaded) return;

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
                backgroundAudio.volume = backgroundStartVolume;
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
                musicFaded = true;
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
