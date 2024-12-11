using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyTroVe : MonoBehaviour
{
    public Button QuayLaiBtn;  // Nút quay lại
    private bool gameStarted = false;

    void Start()
    {
        // Đảm bảo nút startButton được gán từ Unity
        if (QuayLaiBtn != null)
        {
            QuayLaiBtn.onClick.AddListener(OnStartButtonClicked);

        }
        else
        {
            Debug.LogError("QuayLaiBtn is not assigned in the inspector.");
        }
    }

    // Hàm gọi khi nút QuayLaiBtn được click
    void OnStartButtonClicked()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            // Chuyển sang Scene Lobby
            SceneManager.LoadScene("Lobby");
        }
    }
}
