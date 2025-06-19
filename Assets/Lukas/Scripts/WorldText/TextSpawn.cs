using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawn : Singleton<TextSpawn>
{
    [SerializeField] private GameObject textPrefab;

    public void SpawnText(string text, Vector2 point)
    {
        GameObject obj = Instantiate(textPrefab, point, quaternion.identity, transform);
        obj.GetComponent<Text>().text = text;
    }
}
