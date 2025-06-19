using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (LogicScript.Instance.LvlIsActive() && SceneManager.GetActiveScene().name == "Lvl 1") text.text = LogicScript.playerScore.ToString();
    }
}
