using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    [SerializeField] private float pointsForHeart = 300;
    private Heart[] hearts;
    private float accumalatedPoints = 0;
    private int maxHearts;
    private int currentHearts;
    private PlayerScript player;
    void Start()
    {
        hearts = GetComponentsInChildren<Heart>();
        maxHearts = hearts.Length;
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHearts = PlayerScript.hearts;

        for (int i = 0; i < maxHearts; i++)
        {
            if (currentHearts < maxHearts)
            {
                if (i < currentHearts) hearts[i].SetHeartFraction(1f);
                else if (i == currentHearts)
                {
                    Debug.Log(accumalatedPoints + " " + pointsForHeart);
                    hearts[i].SetHeartFraction(accumalatedPoints / pointsForHeart);
                }
                else hearts[i].SetHeartFraction(0f);
            }
            else
            {
                hearts[i].SetHeartFraction(1f);
            }
        }

    }
    public void ResetProgress()
    {
        accumalatedPoints = 0;
    }
    public void Progress(int points)
    {
        if (PlayerScript.hearts < maxHearts) accumalatedPoints += points;
        if (accumalatedPoints >= pointsForHeart)
        {
            PlayerScript.hearts++;
            accumalatedPoints %= pointsForHeart;
        }
    }
}
