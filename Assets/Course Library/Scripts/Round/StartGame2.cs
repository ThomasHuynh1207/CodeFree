using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame2 : MonoBehaviour
{
    public Button startButton;  // Nút bắt đầu
    private bool gameStarted = false;

    void Start()
    {
        // Đảm bảo nút startButton được gán từ Unity
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);

        }
        else
        {
            Debug.LogError("Start button is not assigned in the inspector.");
        }
    }

    // Hàm gọi khi nút Start được click
    void OnStartButtonClicked()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            // Chuyển sang Scene Game1  
            SceneManager.LoadScene("Game2");
        }
    }
}
