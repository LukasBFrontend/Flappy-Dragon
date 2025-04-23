using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int weaponHitpoints = 250;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (weaponHitpoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        weaponHitpoints -= damage;
    }
}
