using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int points = 20;
    [SerializeField] private AudioClip collectClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    private LogicScript logic;
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
