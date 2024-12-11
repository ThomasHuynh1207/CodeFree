using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2Player : MonoBehaviour
{
    public int pointValue = 10; // Số điểm nhận được khi chạm vào checkpoint
    private bool isCollected = false; // Cờ để đảm bảo checkpoint chỉ được nhặt một lần

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu checkpoint đã được nhặt
        if (isCollected) return;

        // Kiểm tra nếu đối tượng chạm có tag là Player1 hoặc Player2
        if (other.CompareTag("Player1"))
        {
            GameManager2Player.Instance.UpdateScore(1, GameManager2Player.Instance.GetScore(1) + pointValue);
            isCollected = true; // Đánh dấu checkpoint đã được nhặt
            Destroy(gameObject); // Xóa checkpoint sau khi được nhặt
        }
        else if (other.CompareTag("Player2"))
        {
            GameManager2Player.Instance.UpdateScore(2, GameManager2Player.Instance.GetScore(2) + pointValue);
            isCollected = true; // Đánh dấu checkpoint đã được nhặt
            Destroy(gameObject); // Xóa checkpoint sau khi được nhặt
        }
    }
}
