using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyHuongDan : MonoBehaviour
{
    public Button HuongDanBtn;  // Nút bắt đầu
    private bool gameStarted = false;

    void Start()
    {
        // Đảm bảo nút HuongDanBtn được gán từ Unity
        if (HuongDanBtn != null)
        {
            HuongDanBtn.onClick.AddListener(OnStartButtonClicked);

        }
        else
        {
            Debug.LogError("HuongDanBtn is not assigned in the inspector.");
        }
    }

    // Hàm gọi khi nút HuongDanBtn được click
    void OnStartButtonClicked()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            // Chuyển sang Scene LobbyHuongDan
            SceneManager.LoadScene("LobbyHuongDan");
        }
    }
}
