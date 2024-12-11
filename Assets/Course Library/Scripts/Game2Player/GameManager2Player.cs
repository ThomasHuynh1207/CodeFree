using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2Player : MonoBehaviour
{
    public static GameManager2Player Instance; // Singleton

    [Header("UI Elements")]
    
    
    public Slider player1HealthBar;            // Máu của Player 1
    public Slider player2HealthBar;            // Máu của Player 2

    public TextMeshProUGUI player1ScoreText;   // Text hiển thị điểm của Player 1
    public TextMeshProUGUI player2ScoreText;   // Text hiển thị điểm của Player 2
    public TextMeshProUGUI timerText;          // Text hiển thị thời gian còn lại
    public GameObject victoryPanel;           // Panel Chiến Thắng
    public TextMeshProUGUI victoryText;       // Text hiển thị người thắng cuộc
    public GameObject ReturnBtn;        // Nút quay lại menu

    [Header("Game Settings")]
    public float matchDuration = 60f;         // Thời gian của trận đấu (giây)
    public int maxHealth = 100;               // Máu tối đa của mỗi người chơi

    private float remainingTime;              // Thời gian còn lại
    private bool isGameOver = false;          // Trạng thái kết thúc game

    private int player1Health;                // Máu hiện tại của Player 1
    private int player2Health;                // Máu hiện tại của Player 2

    private int player1Score = 0;             // Điểm của Player 1
    private int player2Score = 0;             // Điểm của Player 2


    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        remainingTime = matchDuration;

        player1Health = maxHealth;
        player2Health = maxHealth;

        // Cập nhật UI ban đầu
        UpdateHealth(1, player1Health);
        UpdateHealth(2, player2Health);
        UpdateScore(1, player1Score);
        UpdateScore(2, player2Score);
        UpdateTimerText();

        // Ẩn VictoryPanel và nút quay lại khi bắt đầu game
        victoryPanel.SetActive(false);
        ReturnBtn.SetActive(false);
    }


    private void Update()
    {
        if (!isGameOver)
        {
            // Đếm ngược thời gian
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                EndGameByTime();
            }
        }
    }

    public void UpdateHealth(int playerNumber, int health) //PlayerHealth
    {
        // Cập nhật máu hiển thị
        if (playerNumber == 1)
        {
            player1HealthBar.value = (float)health / maxHealth;
        }
        else if (playerNumber == 2)
        {
            player2HealthBar.value = (float)health / maxHealth;
        }
    }

    public void UpdateScore(int playerNumber, int score)
    {
        // Cập nhật điểm hiển thị
        if (playerNumber == 1)
        {
            player1Score = score;
            player1ScoreText.text = $"Score: {score}";
        }
        else if (playerNumber == 2)
        {
            player2Score = score;
            player2ScoreText.text = $"Score: {score}";
        }
    }

    public int GetScore(int playerNumber)
    {
        return playerNumber == 1 ? player1Score : player2Score;
    }

    private void UpdateTimerText()
    {
        // Định dạng thời gian
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void TakeDamage(int playerNumber, int damage)
    {
        if (playerNumber == 1)
        {
            player1Health -= damage;
            if (player1Health < 0) player1Health = 0;
            UpdateHealth(1, player1Health);

            if (player1Health <= 0)
            {
                EndGame(2); // Player 2 thắng
            }
        }
        else if (playerNumber == 2)
        {
            player2Health -= damage;
            if (player2Health < 0) player2Health = 0;
            UpdateHealth(2, player2Health);

            if (player2Health <= 0)
            {
                EndGame(1); // Player 1 thắng
            }
        }
    }

    

    public void EndGame(int winningPlayer)
    {
        isGameOver = true;
        Debug.Log($"Player {winningPlayer} Wins!");

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            victoryText.text = $"Player {winningPlayer} Wins!";
            ReturnBtn.SetActive(true);  // Hiển thị nút quay lại
        }
    }

    private void EndGameByTime()
    {
        isGameOver = true;

        // Phân định thắng thua dựa trên điểm
        if (player1Score > player2Score)
        {
            EndGame(1);
        }
        else if (player2Score > player1Score)
        {
            EndGame(2);
        }
        else
        {
            victoryText.text = "It's a Draw!";
            victoryPanel.SetActive(true);
        }
    }

    
}
