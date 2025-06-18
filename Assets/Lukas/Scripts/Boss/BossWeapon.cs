using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int weaponHitpoints = 200;
    [SerializeField] private AudioClip audioClip;
    [Range(0, 100)][SerializeField] private int audioVolume = 50;
    [SerializeField] private GameObject boss;
    [SerializeField] private int weaponNumber = 1;
    private BossScript bossScript;
    private FlickerScript flickerScript;
    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        flickerScript = gameObject.GetComponent<FlickerScript>();
    }

    void Update()
    {
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
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (bossScript.isMoving)
        {
            flickerScript.Flicker(2, .5f);
            weaponHitpoints -= damage;
        }
    }
}
