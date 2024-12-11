using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue = 10; // Điểm khi mục tiêu bị bắn trúng

    public void DestroyTarget()
    {
        Destroy(gameObject); // Xóa mục tiêu khỏi Scene
    }
}
