using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    private GameObject moving;
    private bool activated = false;
    void Start()
    {
        moving = GameObject.FindGameObjectWithTag("Moving");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.transform.position.x < transform.position.x)
        {
            LogicScript.Instance.SetRespawn(moving.transform.position + new Vector3(-1f, -0f, 0f));
            SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
        }
    }
}
