using UnityEngine;

public class Collectable : MonoBehaviour
{
    public LogicScript logic;
    public int points = 20;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private float audioVolume = 1f;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            SoundFXManager.Instance.playSoundFXClip(collectClip, transform, audioVolume);

            logic.AddScore(points);

            Destroy(gameObject);
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
