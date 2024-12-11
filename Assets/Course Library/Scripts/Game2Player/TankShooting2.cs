using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting2 : MonoBehaviour
{
    public Transform firePoint;           // Điểm bắn từ nòng súng
    public float fireRate = 1f;           // Tốc độ bắn
    public float range = 100f;            // Tầm bắn của tia
    public LineRenderer lineRenderer;     // Đường tia đạn
    public ParticleSystem muzzleFlash;    // Hiệu ứng nổ của nòng súng
    public AudioSource audioSource;       // Nguồn phát âm thanh
    public AudioClip shootingSound;       // Âm thanh nổ súng
    public Image cooldownCircle;


    private float nextFire = 0f;          // Thời gian kế tiếp có thể bắn
    private bool isCoolingDown = false;  // Trạng thái cooldown

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu nhấn phím 1 và đủ thời gian để bắn tiếp
        if (Input.GetKeyDown(KeyCode.Keypad1) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate; // Cập nhật thời gian bắn tiếp theo
            StartCoroutine(CooldownRoutine());
            Shoot(); // Gọi hàm bắn
        }
    }

    void Shoot()
    {
        // Phát âm thanh bắn
        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
        // Hiệu ứng nổ của nòng súng
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Vẽ tia laser bắn
        StartCoroutine(FireLaser());
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

    private IEnumerator FireLaser()
    {
        // Bật LineRenderer nếu có
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
        }

        // Tạo raycast từ vị trí firePoint về phía trước
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            // Điểm kết thúc tia laser
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            // Kiểm tra mục tiêu và xử lý trừ máu nếu trúng
            if (hit.collider.CompareTag("Player1")) // Tag của Player 1
            {
                GameManager2Player.Instance.TakeDamage(1, 10); // Trừ 10 máu Player 1
            }
        }
        else
        {
            // Nếu không trúng, tia laser kéo dài đến khoảng cách tối đa
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * range);
            }
        }

        // Tắt laser sau 0.1 giây
        yield return new WaitForSeconds(0.1f);
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}
