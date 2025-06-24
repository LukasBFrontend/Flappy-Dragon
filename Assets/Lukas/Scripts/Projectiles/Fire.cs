using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [Range(0, 100)][SerializeField] private float audioVolume;
    public float speed = 20;
    public int damage = 50;
    private Rigidbody2D rigidBody;
    PlayerScript player;
    Animator animator;

    void Start()
    {

        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.linearVelocity = transform.right * speed;
    }

    void Update()
    {
        if (gameObject.transform.position.x > 24) Destroy(gameObject);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("HasRedPower", player.activePowerUp == PowerUp.Red);
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
            }
            else if (bossWeapon != null)
            {
                bossWeapon.TakeDamage(damage);
            }
            else if (bossScript != null)
            {
                bossScript.TakeDamage(damage);
            }

            SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);

            Destroy(gameObject);
        }
        else if (hitInfo.tag == "Ground")
        {

        }

    }

    private bool IsHostile(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Ground", "Robot", "Missile", "Enemy", "Boss" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            return true;
        }

        return false;
    }
}
