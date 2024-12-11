using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject settingsPanel;       // Panel cài đặt
    public Slider volumeSlider;            // Slider chỉnh âm lượng
    public Toggle muteToggle;              // Toggle tắt/mở nhạc
    public AudioSource audioSource;        // AudioSource của nhạc nền
    public static AudioManager Instance;   // Singleton instance

    private float savedVolume = 0.5f;      // Giá trị âm lượng lưu trữ mặc định

    void Start()
    {
        // Khôi phục âm lượng và trạng thái mute từ PlayerPrefs
        savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f); // Giá trị mặc định 0.5 nếu chưa lưu
        bool isMuted = PlayerPrefs.GetInt("Mute", 0) == 1; // Trạng thái mute, 0 là không mute, 1 là mute

        audioSource.volume = savedVolume;
        audioSource.mute = isMuted;

        // Khởi tạo giá trị Slider và Toggle
        volumeSlider.value = savedVolume;
        muteToggle.isOn = isMuted;

        // Gán sự kiện cho Slider và Toggle
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        muteToggle.onValueChanged.AddListener(OnMuteToggle);

        
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không phá hủy khi chuyển Scene
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Xóa đối tượng trùng lặp
        }

        
    }

    

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true); // Hiển thị Panel
            Debug.Log("Settings Panel opened.");
        }
        else
        {
            Debug.LogError("SettingsPanel is null in OpenSettings!");
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // Ẩn Panel
        }
    }

    public void OnVolumeChange(float value)
    {
        if (!audioSource.mute) // Chỉ thay đổi âm lượng khi chưa tắt tiếng
        {
            audioSource.volume = value;
        }
        savedVolume = value; // Lưu giá trị âm lượng hiện tại
        PlayerPrefs.SetFloat("Volume", savedVolume); // Lưu âm lượng vào PlayerPrefs
    }

    public void OnMuteToggle(bool isMuted)
    {
        audioSource.mute = isMuted; // Tắt/mở tiếng nhạc
        PlayerPrefs.SetInt("Mute", isMuted ? 1 : 0); // Lưu trạng thái mute vào PlayerPrefs

        if (!isMuted)
        {
            audioSource.volume = savedVolume; // Khôi phục âm lượng khi bật nhạc lại
        }
        else
        {
            audioSource.volume = 0; // Tắt tiếng khi mute
        }
    }

    // Hàm thay đổi nhạc nền
    public void ChangeMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Đảm bảo lưu PlayerPrefs khi kết thúc ứng dụng hoặc Scene
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }


}


