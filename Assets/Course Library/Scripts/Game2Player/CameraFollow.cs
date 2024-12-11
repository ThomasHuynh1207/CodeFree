using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // Đối tượng mà camera sẽ theo dõi
    private Vector3 offset = new Vector3(0, 30, -55); // Khoảng cách giữa camera và người chơi

    void Start()
    {
        // Đặt vị trí ban đầu cho camera
        if (player != null)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Đặt vị trí camera dựa trên vị trí của Player và offset
            transform.position = player.transform.position + player.transform.TransformDirection(offset);

            // Xoay camera mượt mà để nhìn về hướng của Player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }
}
