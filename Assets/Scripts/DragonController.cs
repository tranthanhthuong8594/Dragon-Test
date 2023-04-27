using System;
using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{
    private const float deceleration = 0.7f;     // Tỷ lệ giảm tốc
    private const float acceleration = 1.02f;    // Tỷ lệ tăng tốc

    [SerializeField] float jumpSpeed = 22f;      // Tốc độ nhảy
    [SerializeField] float currentJumpSpeed;     // Tốc độ nhảy hiện tại (nhảy lên thì tốc độ sẽ giảm dần)
    [SerializeField] float jumpDelay = 0.1f;     // Khoảng thời gian nhảy
    [SerializeField] float jumpTimer = 0f;       // Đếm thời gian nhảy

    [SerializeField] float fallSpeed = 5.5f;     // Tốc độ rơi
    [SerializeField] float currentFallSpeed;     // Tốc độ rơi hiện tại (rơi xuống thì tốc độ sẽ tăng dần)
    [SerializeField] float fallDelay = 0.13f;    // Khoảng thời gian tạm ngưng khi nhảy lên, sau time này sẽ rơi
    [SerializeField] float fallTimer = 0f;       // Đếm thời gian tạm ngưng

    bool isJump = false;    // Được phép nhảy khi click chuột
    bool isFall = true;     // Bắt đầu rơi

    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float yLimit = -3.7f;

    // Start is called before the first frame update
    void Awake()
    {
        this.currentJumpSpeed = this.jumpSpeed;
        this.currentFallSpeed = this.fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu Click chuột thì play âm thanh và cho phép nhảy
        if (Input.GetMouseButtonDown(0))
        {
            AudioController.Instance.AudioSource.Play();
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        DragonFall();
        DragonJump();
        CheckCollide();
    }

    public void DragonJump()
    {
        if (this.isJump)
        {
            // Nếu endGame thì không làm gì cả
            if (GamePlayController.Instance.endGame) return;

            // Di chuyển Dragon lên theo tốc độ currentSpeed giảm dần
            transform.Translate(Vector2.up * this.currentJumpSpeed * Time.fixedDeltaTime);
            this.currentJumpSpeed *= deceleration;

            this.isFall = false;                    // Nếu đang nhảy thì không rơi

            this.jumpTimer += Time.fixedDeltaTime;

            // Tính góc quay khi nhảy
            float angel = 0;
            angel = Mathf.Lerp(0, 80, this.jumpTimer / (this.jumpDelay * this.rotateSpeed));
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angel);     // Quay đối tượng con để không ảnh hưởng Translate

            // Nếu thời gian nhảy kết thúc thì reset các giá trị
            if (this.jumpTimer > this.jumpDelay)
            {
                this.jumpTimer = 0f;
                this.isJump = false;
                this.currentJumpSpeed = this.jumpSpeed;
                Invoke("_IsFall", fallDelay);       // Sau khoảng time fallDelay sẽ cho phép rơi
            }
        }
    }

    void _IsFall()
    {
        this.isFall = true;
        this.fallTimer = 0f;
        this.currentFallSpeed = this.fallSpeed;
    }

    void DragonFall()
    {
        if (this.isFall)
        {
            // Rơi tốc độ nhanh dần
            transform.Translate(Vector2.down * this.currentFallSpeed * Time.fixedDeltaTime);
            this.currentFallSpeed *= acceleration;

            // Tính toán góc quay
            this.fallTimer += Time.fixedDeltaTime;
            float angel = 0;
            angel = Mathf.Lerp(0, -80, this.fallTimer / (this.fallDelay * this.rotateSpeed));
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angel);
        }
    }

    // Kiểm tra có va chạm với ground không, nếu có thì kết thúc game
    private void CheckCollide()
    {
        if(transform.position.y < yLimit)
        {
            GamePlayController.Instance.EndGame();
        }    
    }
}
