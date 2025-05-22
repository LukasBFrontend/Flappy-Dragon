using UnityEngine;
using UnityEngine.UI;

public class TextSync : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private Text text;
    private Text syncText;
    void Start()
    {
        syncText = textObject.GetComponent<Text>();
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        text.text = syncText.text;
    }
}
