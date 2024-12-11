using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // Hàm này được gọi khi nút thoát được nhấn
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");

        // Thoát game khi chạy bản build
        Application.Quit();

#if UNITY_EDITOR
        // Dừng chế độ Play trong Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
