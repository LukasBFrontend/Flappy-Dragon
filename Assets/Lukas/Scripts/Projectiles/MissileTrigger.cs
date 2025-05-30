using Unity.Mathematics;
using UnityEngine;

public class MissileTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip missileClip, warningClip;
    [SerializeField] private int audioVolume = 50;
    [SerializeField] private GameObject missile, missileWarning;
    [SerializeField] private float timer = 2f;

    private GameObject activeWarningInstance;
    private Missile missileScript;
    private int xPosition = 9;
    private bool timerEnabled = false;

    private void Start()
    {
        missileScript = missile.GetComponent<Missile>();
    }

    private void Update()
    {
        if (timerEnabled)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timerEnabled = false;
                activeWarningInstance.SetActive(false);
                missileScript.isMoving = true;
                SoundFXManager.Instance.playSoundFXClip(missileClip, transform, audioVolume);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            SoundFXManager.Instance.playSoundFXtimes(warningClip, transform, audioVolume, 3, 0.3f);
            activeWarningInstance = Instantiate(missileWarning, new Vector2(xPosition, missile.transform.position.y), Quaternion.identity);
            timerEnabled = true;
        }
    }

    private bool IsPlayer(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Player" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            return true;
        }

        return false;
    }
}
