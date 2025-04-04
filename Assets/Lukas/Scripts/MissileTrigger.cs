using Unity.Mathematics;
using UnityEngine;

public class MissileTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject missile;
    public GameObject missileWarning;
    private GameObject activeWarningInstance;

    Missile missileScript;
    int xPosition = 10;

    public float timer = 2f;

    bool timerEnabled = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            activeWarningInstance = Instantiate(missileWarning, new Vector2(xPosition, missile.transform.position.y), Quaternion.identity);
            timerEnabled = true;
        }
    }

    private bool IsPlayer(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Player" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            Debug.Log($"hit {testedTag}"); //improve your debug informations

            return true;
        }

        return false;
    }

    private void Start()
    {
        missileScript = missile.GetComponent<Missile>();
    }
    private void Update()
    {
        if (timerEnabled)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Debug.Log("Timer ended.");
                timerEnabled = false;
                activeWarningInstance.SetActive(false);
                missileScript.isMoving = true;
            }
        }

    }
}
