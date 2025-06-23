using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int weaponHitpoints = 200;
    [SerializeField] private AudioClip audioClip;
    [Range(0, 100)][SerializeField] private int audioVolume = 50;
    [SerializeField] private GameObject boss;
    [SerializeField] private Animator otherCannon, missileLaunchers;
    [SerializeField] private int weaponNumber = 1;
    private BossScript bossScript;
    private FlickerScript flickerScript;
    private Animator animator;
    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        flickerScript = gameObject.GetComponent<FlickerScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("BossHealth", bossScript.HealthInPercent());
        if (weaponHitpoints <= 0)
        {
            SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
            bossScript.TakeDamage(100);

            if (weaponNumber == 1)
            {
                bossScript.animator.SetBool("UpCannonDestroyed", true);
            }
            else if (weaponNumber == 2)
            {
                bossScript.animator.SetBool("DownCannonDestroyed", true);
            }
            animator.SetBool("CannonDamaged", true);
            otherCannon.SetBool("DoNextState", !otherCannon.GetBool("DoNextState"));
            missileLaunchers.SetBool("DoNextState", !missileLaunchers.GetBool("DoNextState"));
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (bossScript.isMoving)
        {
            flickerScript.Flicker(2, .12f);
            weaponHitpoints -= damage;
        }
    }
}
