using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class Tutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject[] instructions;
    [SerializeField] private GameObject bluePower;
    public bool tutorialIsActive = true;
    private bool hasJumped, hasShotFire, hasShotLaser, powerUpSpawned = false;
    private float chargeTimer;
    private float chargeTime = 1f;
    private bool hasIncremented = true;
    private int step = 1;
    private float delay = 1f;
    private float timer = 0f;
    private GameObject lvl, player, moving, background, powerUpInstance;
    private Rigidbody2D rb;
    private Vector3 startPos;
    private BluePower bluePowerScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moving = GameObject.FindGameObjectWithTag("Moving");
        background = GameObject.FindGameObjectWithTag("Background");

        chargeTimer = chargeTime;

        if (player)
        {
            rb = player.GetComponent<Rigidbody2D>();
            startPos = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.Instance.tutorialIsActive && player.transform.position.x < startPos.x + 0.05f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.linearVelocityX = 0;
            LogicScript.Instance.tutorialIsActive = false;
        }

        if (!tutorialIsActive || !ScreenManager.Instance.startAtTutorial) return;
        switch (step)
        {
            case 1:
                player.transform.position = new Vector2(0, 0);
                ScreenManager.Instance.HideScoreCanvas();
                ScreenManager.Instance.HidePlayerCanvas();
                LogicScript.Instance.tutorialIsActive = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                background.GetComponent<GroundMoveScript>().enabled = false;
                moving.GetComponent<GroundMoveScript>().enabled = false;
                instructions[0].GetComponent<SpriteRenderer>().enabled = true;
                if (!hasJumped)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        timer = delay;
                        hasJumped = true;
                    }
                }
                break;
            case 2:
                instructions[0].GetComponent<SpriteRenderer>().enabled = false;
                instructions[1].GetComponent<SpriteRenderer>().enabled = true;
                ScreenManager.Instance.ShowPlayerCanvas();

                if (!hasShotFire)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        timer = delay;
                        hasShotFire = true;
                    }
                }
                break;
            case 3:
                instructions[1].GetComponent<SpriteRenderer>().enabled = false;
                instructions[2].GetComponent<SpriteRenderer>().enabled = true;

                if (!powerUpSpawned)
                {
                    powerUpInstance = Instantiate(bluePower, new Vector3(18, 0, 0), quaternion.identity);
                    powerUpInstance.GetComponent<Rigidbody2D>().linearVelocityX = -5f;
                    bluePowerScript = powerUpInstance.GetComponent<BluePower>();
                    bluePowerScript.isPreview = true;
                    powerUpSpawned = true;
                }
                if (!hasShotLaser)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        chargeTimer -= Time.deltaTime;
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        if (chargeTimer <= 0 && player.GetComponent<PlayerScript>().activePowerUp == PowerUp.Blue)
                        {
                            timer = delay;
                            hasShotLaser = true;
                            bluePowerScript.isPreview = false;
                            bluePowerScript.SetTimer(3f);
                        }
                        chargeTimer = chargeTime;
                    }
                }
                break;
            default:
                instructions[2].GetComponent<SpriteRenderer>().enabled = false;
                background.GetComponent<GroundMoveScript>().enabled = true;
                moving.GetComponent<GroundMoveScript>().enabled = true;
                rb.constraints = RigidbodyConstraints2D.None;
                LogicScript.Instance.AddScore(-1);
                rb.linearVelocityX = -3f;
                tutorialIsActive = false;
                ScreenManager.Instance.startAtTutorial = false;
                ScreenManager.Instance.ShowScoreCanvas();
                break;
        }

        if (timer <= 0 && !hasIncremented)
        {
            step++;
            hasIncremented = true;
        }
        else if (timer > 0)
        {
            hasIncremented = false;
        }

        timer -= Time.deltaTime;
    }
}
