using Cysharp.Threading.Tasks;
using OxGFrame.MediaFrame;
using OxGKit.Utilities.TextureAnim;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float thrust = 225f;         // 衝力
    public float minTiltSmooth = 1f;    // 最小傾斜度
    public float maxTiltSmooth = 5f;    // 最大傾斜度
    public float hoverAmplitude = 0.1f; // 盤旋震幅
    public float hoverFrequency = 5f;   // 盤旋頻率

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

        // 判斷是否初次點擊
        if (!this._firstTap)
        {
            // Hover the player before starting the game
            this._UpdateHoverState(dt);
        }
        else
        {
            // Rotate downward while falling
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this._downRotation, this._tiltSmooth * dt);
        }

        // Limit the rotation that can occur to the player
        this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Mathf.Clamp(this.transform.rotation.z, this._downRotation.z, this._upRotation.z), transform.rotation.w);
    }

    private void LateUpdate()
    {
        if (CoreSystem.IsGameStart())
        {
            if (!this._firstTap)
            {
                // first tap
                this._firstTap = true;

                // This code checks the first tap. After first tap the tutorial image is removed and game starts
                this._rigid.velocity = Vector2.zero;
                // 初始重力比率
                this._rigid.gravityScale = 1f;
            }

            // 點擊滑鼠左鍵
            if (Input.GetMouseButtonDown(0))
            {
                // 恢復重力比率
                this._rigid.gravityScale = 1f;
                this._tiltSmooth = this.minTiltSmooth;
                // 往上俯衝角度
                this.transform.rotation = this._upRotation;
                // 速率歸零
                this._rigid.velocity = Vector2.zero;
                // Push the player upwards
                this._rigid.AddForce(Vector2.up * thrust);

                // 播放飛起音效
                MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.FlySfx).Forget();
            }
        }

        // 重力下降
        if (this._rigid.velocity.y < -1f)
        {
            // Increase gravity so that downward motion is faster than upward motion
            this._tiltSmooth = this.maxTiltSmooth;
            // 加重重力比率
            this._rigid.gravityScale = 2f;
        }
    }

    /// <summary>
    /// 小鳥盤旋
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
        // 判斷觸發是否為 Score
        if (collider.transform.CompareTag("Score"))
        {
            // 銷毀分數觸發物件
            Destroy(collider.gameObject);

            // 增加分數
            CoreSystem.AddScore();
        }
        // 判斷觸發是否為 Pipe
        else if (collider.transform.CompareTag("Pipe"))
        {
            // Destroy the Obstacles after they reach a certain area on the screen
            foreach (Transform child in collider.transform.parent.transform)
            {
                // 關閉 Pipe 碰撞 (主要是讓 Bird 可以穿越掉落至 Ground)
                child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

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

            // 小鳥撞擊後即死亡
            this.BirdHitAndDead();
        }
    }

    /// <summary>
    /// 小鳥撞擊跟死亡
    /// </summary>
    public void BirdHitAndDead()
    {
        // 播放撞擊音效
        MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.HitSfx).Forget();

        // 遊戲結束
        if (CoreSystem.IsGameStart()) CoreSystem.GameOver();

        // 歸零速率
        this._rigid.velocity = Vector2.zero;

        // 動畫停止 (也可以使用 Animator 製作序列動畫)
        if (this._textureAnimation.enabled) this._textureAnimation.enabled = false;
    }
}
