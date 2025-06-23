using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    private BossScript bossScript;

    private void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            this.transform.parent.transform.DetachChildren();
            bossScript.isMoving = true;
            ScreenManager.Instance.ShowBossCanvas();
            ScreenManager.Instance.HideScoreCanvas();
            LogicScript.Instance.isBossFight = true;
            gameObject.SetActive(false);
        }
    }

    private bool IsPlayer(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Player" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            return true;
        }

        return false;
    }
}
