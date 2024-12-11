using UnityEngine;
using UnityEngine.UI;

public class ChangeImageType : MonoBehaviour
{
    public Image image; // Tham chiếu tới đối tượng Image trong Scene

    void Start()
    {
        // Đảm bảo Image được cấp và đã có trong Inspector
        if (image != null)
        {
            // Đặt kiểu hiển thị là Filled
            image.type = Image.Type.Filled;

            // Tùy chỉnh các thuộc tính khác của Filled
            image.fillMethod = Image.FillMethod.Radial360; // Chọn lấp đầy theo dạng vòng tròn

            // Cập nhật điểm bắt đầu lấp đầy (nếu cần thiết)
            image.fillOrigin = (int)Image.Origin360.Top; // Lấp đầy từ phía trên của vòng tròn (có thể thay đổi)

            // Đặt phần lấp đầy ở mức 50%
            image.fillAmount = 0.5f; // Thay đổi giá trị này từ 0.0f đến 1.0f để điều khiển độ đầy của vòng tròn
        }
    }
}
