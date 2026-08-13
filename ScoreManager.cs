using UnityEngine;
using TMPro;

/// <summary>
/// إدارة أهداف الفريقين وعرض النتيجة.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Ball Spawn")]
    [SerializeField] private Transform ballSpawnPoint;

    private int playerScore = 0;
    private int opponentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    /// <summary>
    /// إضافة هدف.
    /// </summary>
    public void AddGoal(bool playerScored)
    {
        if (playerScored)
            opponentScore++;
        else
            playerScore++;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                playerScore + "  -  " + opponentScore;
        }
    }

    public Vector3 GetBallSpawnPosition()
    {
        if (ballSpawnPoint != null)
            return ballSpawnPoint.position;

        return Vector3.zero;
    }
}
