using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject firePrefab;
    private PlayerScript playerScript;
    [SerializeField] private AudioClip[] fireClips;
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
        int random = Random.Range(0, 3);
        SoundFXManager.Instance.playSoundFXClip(fireClips[random], transform, 0.6f);
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
    }
}
