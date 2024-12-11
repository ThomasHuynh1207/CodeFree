using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public Transform firePoint;           // Điểm bắn từ nòng súng
    public float fireRate = 1f;           // Tốc độ bắn
    public float range = 100f;            // Tầm bắn của tia
    public LineRenderer lineRenderer;     // Đường tia đạn
    public ParticleSystem muzzleFlash;    // Hiệu ứng nổ của nòng súng
    public AudioSource audioSource;       // Nguồn phát âm thanh
    public AudioClip shootingSound;       // Âm thanh nổ súng
    public Image cooldownCircle;          // Hình ảnh vòng tròn thời gian

    private float nextFire = 0f;

    // Tham chiếu đến GameManager
    private GameManager gameManager;    
    private bool isCoolingDown = false;  // Trạng thái cooldown


    private void Start()
    {
        // Tìm GameManager trong Scene
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager không được tìm thấy!");
        }
    }

    void Update()
    {
        // Kiểm tra nút bắn và thời gian giữa các phát bắn
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire)
        {
            
            nextFire = Time.time + fireRate;
            StartCoroutine(CooldownRoutine());
            Shoot();
            
        }
    }

    void Shoot()
    {
        // Hiệu ứng nổ súng
        muzzleFlash.Play();

        // Phát âm thanh bắn
        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }

        RaycastHit hit;
        Vector3 targetPoint;

        // Tạo tia bắn từ firePoint
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            targetPoint = hit.point;

            // Kiểm tra nếu mục tiêu bị trúng
            if (hit.collider.CompareTag("Target"))
            {
                Target target = hit.collider.GetComponent<Target>();

                if (target != null)
                {
                    // Cộng điểm thông qua GameManager
                    if (gameManager != null)
                    {
                        gameManager.AddScore(target.pointValue);
                    }

                    // Hủy mục tiêu
                    target.DestroyTarget();
                }
            }
        }
        else
        {
            // Nếu không trúng, tia bắn đến tầm xa nhất
            targetPoint = firePoint.position + firePoint.forward * range;
        }

        // Vẽ tia laser
        StartCoroutine(DrawLaser(targetPoint));
    }

    IEnumerator CooldownRoutine()
    {
        isCoolingDown = true;

        float elapsedTime = 0f;
        if (cooldownCircle != null)
        {
            cooldownCircle.fillAmount = 0f;
        }

        while (elapsedTime < fireRate)
        {
            elapsedTime += Time.deltaTime;

            if (cooldownCircle != null)
            {
                cooldownCircle.fillAmount = elapsedTime / fireRate;
            }

            yield return null;
        }

        isCoolingDown = false;
    }


    IEnumerator DrawLaser(Vector3 targetPoint)
    {
        // Kích hoạt LineRenderer
        lineRenderer.enabled = true;

        // Đặt điểm bắt đầu và kết thúc
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetPoint);

        // Hiển thị tia trong thời gian ngắn
        yield return new WaitForSeconds(0.1f);

        // Tắt LineRenderer
        lineRenderer.enabled = false;
    }
}
