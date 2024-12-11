using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheLeGame : MonoBehaviour
{
    public Button RuleBtn;  // Nút bắt đầu


    void Start()
    {
        // Đảm bảo nút RuleBtn được gán từ Unity
        if (RuleBtn != null)
        {
            RuleBtn.onClick.AddListener(OnStartButtonClicked);

        }
        else
        {
            Debug.LogError("RuleBtn is not assigned in the inspector.");
        }
    }

    // Hàm gọi khi nút RuleBtn được click
    void OnStartButtonClicked()
    {
        // Chuyển sang Scene TheLe
        SceneManager.LoadScene("TheLe");
    }
}
