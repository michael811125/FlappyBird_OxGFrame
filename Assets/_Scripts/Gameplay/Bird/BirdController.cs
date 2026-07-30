using Cysharp.Threading.Tasks;
using OxGFrame.MediaFrame;
using OxGKit.Utilities.TextureAnim;
using UnityEngine;

/// <summary>
/// Controls the bird: hovering before the run starts, flapping, tilting and death.
/// It talks to the rest of the game only through CoreSystem, so gameplay objects
/// stay decoupled from the stage flow.
/// <para>
/// 控制小鳥：開始前的盤旋、拍翅、傾斜與死亡。
/// 僅透過 CoreSystem 與遊戲其他部分溝通,
/// 讓遊戲物件與階段流程保持解耦。
/// </para>
/// </summary>
public class BirdController : MonoBehaviour
{
    public float thrust = 225f;         // Thrust | 衝力
    public float minTiltSmooth = 1f;    // Minimum tilt smoothing | 最小傾斜度
    public float maxTiltSmooth = 5f;    // Maximum tilt smoothing | 最大傾斜度
    public float hoverAmplitude = 0.1f; // Hover amplitude | 盤旋震幅
    public float hoverFrequency = 5f;   // Hover frequency | 盤旋頻率

    private float _elapsedDt;
    private float _tiltSmooth;
    private Rigidbody2D _rigid;
    private Quaternion _downRotation;
    private Quaternion _upRotation;

    private bool _firstTap = false;

    private TextureAnimation _textureAnimation;

    private void Start()
    {
        this._tiltSmooth = this.maxTiltSmooth;
        this._rigid = this.gameObject.GetComponent<Rigidbody2D>();
        this._downRotation = Quaternion.Euler(0, 0, -90);
        this._upRotation = Quaternion.Euler(0, 0, 35);

        this._textureAnimation = this.gameObject.GetComponent<TextureAnimation>();
    }

    private void Update()
    {
        float dt = CoreSystem.deltaTime;

        // Check whether this is still before the first tap
        // 判斷是否初次點擊
        if (!this._firstTap)
        {
            // Hover the player before starting the game
            // 遊戲開始前讓小鳥盤旋
            this._UpdateHoverState(dt);
        }
        else
        {
            // Rotate downward while falling
            // 下墜時逐漸轉向朝下
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this._downRotation, this._tiltSmooth * dt);
        }

        // Limit the rotation that can occur to the player
        // 限制小鳥可以旋轉的角度範圍
        this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Mathf.Clamp(this.transform.rotation.z, this._downRotation.z, this._upRotation.z), transform.rotation.w);
    }

    private void LateUpdate()
    {
        if (CoreSystem.IsGameStart())
        {
            if (!this._firstTap)
            {
                // first tap
                // 初次點擊
                this._firstTap = true;

                // This code checks the first tap. After first tap the tutorial image is removed and game starts
                // 初次點擊後移除教學圖並正式開始遊戲
                this._rigid.linearVelocity = Vector2.zero;
                // Initial gravity scale
                // 初始重力比率
                this._rigid.gravityScale = 1f;
            }

            // Left mouse button clicked
            // 點擊滑鼠左鍵
            if (Input.GetMouseButtonDown(0))
            {
                // Restore the gravity scale
                // 恢復重力比率
                this._rigid.gravityScale = 1f;
                this._tiltSmooth = this.minTiltSmooth;
                // Pitch up
                // 往上俯衝角度
                this.transform.rotation = this._upRotation;
                // Reset the velocity
                // 速率歸零
                this._rigid.linearVelocity = Vector2.zero;
                // Push the player upwards
                // 給予向上的推力
                this._rigid.AddForce(Vector2.up * thrust);

                // Play the flying sound effect
                // 播放飛起音效
                MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.FlySfx).Forget();
            }
        }

        // Falling under gravity
        // 重力下降
        if (this._rigid.linearVelocity.y < -1f)
        {
            // Increase gravity so that downward motion is faster than upward motion
            // 加大重力讓下墜比上升更快
            this._tiltSmooth = this.maxTiltSmooth;
            // Heavier gravity scale
            // 加重重力比率
            this._rigid.gravityScale = 2f;
        }
    }

    /// <summary>
    /// Make the bird hover
    /// <para>小鳥盤旋</para>
    /// </summary>
    /// <param name="dt"></param>
    private void _UpdateHoverState(float dt)
    {
        this._elapsedDt += dt;
        float yWave = this.hoverAmplitude * Mathf.Sin(hoverFrequency * this._elapsedDt);
        transform.localPosition = new Vector3(0, yWave, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check whether the trigger is a Score marker
        // 判斷觸發是否為 Score
        if (collider.transform.CompareTag("Score"))
        {
            // Destroy the score trigger object
            // 銷毀分數觸發物件
            Destroy(collider.gameObject);

            // Add to the score
            // 增加分數
            CoreSystem.AddScore();
        }
        // Check whether the trigger is a Pipe
        // 判斷觸發是否為 Pipe
        else if (collider.transform.CompareTag("Pipe"))
        {
            // Destroy the Obstacles after they reach a certain area on the screen
            // 障礙物抵達畫面特定區域後銷毀
            foreach (Transform child in collider.transform.parent.transform)
            {
                // Disable the pipe collider (mainly so the bird can fall through onto the ground)
                // 關閉 Pipe 碰撞 (主要是讓 Bird 可以穿越掉落至 Ground)
                child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            // The bird dies once it hits something
            // 小鳥撞擊後即死亡
            this.BirdHitAndDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collistion)
    {
        if (collistion.transform.CompareTag("Ground"))
        {
            this._rigid.simulated = false;
            transform.rotation = this._downRotation;

            // The bird dies once it hits something
            // 小鳥撞擊後即死亡
            this.BirdHitAndDead();
        }
    }

    /// <summary>
    /// Handle the bird hitting something and dying
    /// <para>小鳥撞擊跟死亡</para>
    /// </summary>
    public void BirdHitAndDead()
    {
        // Play the hit sound effect
        // 播放撞擊音效
        MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.HitSfx).Forget();

        // Game over
        // 遊戲結束
        if (CoreSystem.IsGameStart()) CoreSystem.GameOver();

        // Reset the velocity
        // 歸零速率
        this._rigid.linearVelocity = Vector2.zero;

        // Stop the animation (an Animator based sequence would work too)
        // 動畫停止 (也可以使用 Animator 製作序列動畫)
        if (this._textureAnimation.enabled) this._textureAnimation.enabled = false;
    }
}
