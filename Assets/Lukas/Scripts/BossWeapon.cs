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
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        weaponHitpoints -= damage;
    }
}
