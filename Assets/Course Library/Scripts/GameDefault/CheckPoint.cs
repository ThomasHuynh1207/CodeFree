using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int pointValue = 10; // Điểm được cộng khi qua checkpoint

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm là người chơi (tag là "Player")
        if (other.CompareTag("Player"))
        {
            // Lấy tham chiếu đến GameManager
            GameManager gameManager = GameManager.Instance;

            if (gameManager != null)
            {
                // Cộng điểm thông qua GameManager
                gameManager.AddScore(pointValue);

                Debug.Log("Checkpoint reached! +" + pointValue + " points");
            }
            else
            {
                Debug.LogError("Không tìm thấy GameManager trong Scene!");
            }

            // Hủy checkpoint để tránh cộng điểm lại
            Destroy(gameObject);
        }
    }
}
