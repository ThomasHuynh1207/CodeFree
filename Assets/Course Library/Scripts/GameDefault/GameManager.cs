using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;     // Text hiển thị thời gian
    public TextMeshProUGUI scoreText;     // Text hiển thị điểm số
    public GameObject victoryPanel;      // Panel Chiến Thắng


    [Header("Game Settings")]
    public int targetScore = 100;        // Điểm cần đạt để chiến thắng
    public float timeLimit = 60f;        // Thời gian giới hạn (giây)

    private int currentScore = 0;        // Điểm hiện tại của người chơi
    private float remainingTime;         // Thời gian còn lại
    private bool isGameOver = false;     // Trạng thái kết thúc trò chơi

    [Header("Victory Panel Elements")]
    public TextMeshProUGUI victoryText;    // Text hiển thị "You Win!"
    public GameObject returnButton;       // Nút trở về Menu chính


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
        remainingTime = timeLimit; // Khởi tạo thời gian
        UpdateTimerText();         // Cập nhật UI thời gian
        UpdateScoreText();         // Cập nhật UI điểm số
    }

    private void Update()
    {
        if (!isGameOver)
        {
            // Giảm thời gian mỗi frame
            remainingTime -= Time.deltaTime;

            // Cập nhật thời gian hiển thị
            UpdateTimerText();

            // Kiểm tra khi gần hết thời gian
            if (remainingTime <= 10)
            {
                timerText.color = Color.red; // Đổi màu chữ khi gần hết giờ
            }

            // Hết thời gian
            if (remainingTime <= 0)
            {
                EndGame(false); // Thua nếu hết giờ mà không đạt đủ điểm
            }
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;

        currentScore += points; // Cộng điểm
        UpdateScoreText();      // Cập nhật UI điểm

        // Kiểm tra thắng
        if (currentScore >= targetScore)
        {
            EndGame(true); // Thắng khi đạt đủ điểm
        }
    }

    private void UpdateTimerText()
    {
        // Định dạng thời gian (phút:giây)
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore; // Hiển thị điểm
    }

    private void EndGame(bool isVictory)
    {
        isGameOver = true;
        Time.timeScale = 0;
        remainingTime = Mathf.Max(0, remainingTime); // Đảm bảo thời gian không âm
        UpdateTimerText();

        if (isVictory)
        {
            Debug.Log("You Win!");
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true); // Hiển thị Panel Chiến Thắng

                // Hiển thị nội dung trên Victory Panel
                if (victoryText != null)
                {
                    victoryText.text = "You Win!"; // Hiển thị thông điệp
                }

                if (returnButton != null)
                {
                    returnButton.SetActive(true); // Hiển thị nút trở về
                }
            }
        }
        else
        {
            // Nếu thua, chuyển đến màn hình Game Over
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ReturnToMainMenu()
    {
        // Đảm bảo game không bị tạm dừng khi trở về menu
        Time.timeScale = 1; // Đảm bảo game không bị tạm dừng
        SceneManager.LoadScene("Lobby"); // Load Scene Menu chính (MainMenu)
    }
}
