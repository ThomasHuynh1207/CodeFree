using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackRound : MonoBehaviour
{
    public Button QuayLaiBtn;  // Nút bắt đầu
    

    void Start()
    {
        // Đảm bảo nút QuayLaiBtn được gán từ Unity
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
            // Chuyển sang Scene Round
            SceneManager.LoadScene("Round");
    }
}
