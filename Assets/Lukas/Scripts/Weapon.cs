using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bluefirePrefab;
    //public Animator animator;

    private PlayerScript playerScript;

    private bool playerIsAlive;

    void Start()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        playerIsAlive = playerScript.playerIsAlive;
        //animator = GetComponent<Animator>();
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
        Instantiate(bluefirePrefab, firePoint.position, firePoint.rotation);
        //animator.SetBool("IsShooting", true);
    }
}
