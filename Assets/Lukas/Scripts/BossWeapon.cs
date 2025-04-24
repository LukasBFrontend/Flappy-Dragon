using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int weaponHitpoints = 200;
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private float volume = 0.2f;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private int weaponNumber = 1;
    private BossScript bossScript;
    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponHitpoints <= 0)
        {
            SoundFXManager.Instance.playSoundFXClip(audioClip, transform, volume);
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
        weaponHitpoints -= damage;
    }
}
