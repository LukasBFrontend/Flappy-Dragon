using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [Range(0, 100)][SerializeField] private float audioVolume;
    public float speed = 20;
    public int damage = 50;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        BossWeapon bossWeapon = hitInfo.GetComponent<BossWeapon>();
        BossScript bossScript = hitInfo.GetComponent<BossScript>();

        if (IsHostile(hitInfo))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
            }
            else if (bossWeapon != null)
            {
                bossWeapon.TakeDamage(damage);
                SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
            }
            else if (bossScript != null)
            {
                bossScript.TakeDamage(damage);
                SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
            }

            Destroy(gameObject);
        }

    }

    private bool IsHostile(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Ground", "Robot", "Missile", "Enemy" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            return true;
        }

        return false;
    }
}
