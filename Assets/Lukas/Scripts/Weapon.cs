using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject firePrefab;

    private PlayerScript playerScript;

    private bool playerIsAlive;

    void Start()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        playerIsAlive = playerScript.playerIsAlive;
    }
    // Update is called once per frame
    void Update()
    {
        playerIsAlive = playerScript.playerIsAlive;
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerIsAlive && !LogicScript.Instance.isPaused)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
    }
}
