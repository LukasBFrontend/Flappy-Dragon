using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject moving;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LogicScript.Instance.SetRespawn(moving.transform.position);
        }
    }
}
