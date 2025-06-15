using UnityEngine;
using System.Collections;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private Drop[] drops;
    private GameObject moving;

    void Start()
    {
        moving = GameObject.FindGameObjectWithTag("Moving");
    }
    public void SpawnDrop()
    {
        float random = Random.Range(0, 100);

        foreach (Drop drop in drops)
        {
            if (random <= drop.dropChance)
            {
                GameObject obj = Instantiate(drop.dropPrefab, transform.position, transform.rotation, moving.transform);
                LogPickup logScript = obj.GetComponent<LogPickup>();
                if (logScript) logScript.entryName = drop.entryName;
            }
        }
    }

    [System.Serializable]
    public class Drop
    {
        public string entryName;
        public GameObject dropPrefab;
        [Range(0, 100)] public float dropChance;
    }
}
