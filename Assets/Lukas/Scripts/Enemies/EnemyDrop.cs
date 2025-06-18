using UnityEngine;
using System.Collections.Generic;

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
        List<GameObject> dropList = new();

        foreach (Drop drop in drops)
        {
            if (random <= drop.dropChance)
            {
                GameObject obj = Instantiate(drop.dropPrefab, transform.position, Quaternion.identity, moving.transform);
                dropList.Add(obj);
                LogPickup logScript = obj.GetComponent<LogPickup>();
                if (logScript) logScript.entryName = drop.entryName;
            }
        }

        for (int i = 0; i < dropList.Count; i++)
        {
            float minX = -dropList.Count / 2;

            float moveX = minX + i * dropList.Count;

            dropList[i].transform.position = new Vector2(transform.position.x + moveX * .35f, transform.position.y);
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
