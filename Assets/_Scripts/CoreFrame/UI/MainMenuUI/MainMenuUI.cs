using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.UIFrame;
using OxGFrame.MediaFrame;
using OxGKit.ButtonSystem;
using OxGKit.TimeSystem;
using UnityEngine;

/// <summary>
/// Main menu UI. Wires up the Play button and runs the bird hover animation on a
/// standalone real-time updater so it stays steady regardless of the game frame rate.
/// <para>
/// 主選單介面。綁定 Play 按鈕, 並以獨立的現實時間 Updater 播放小鳥盤旋動畫,
/// 使其不受遊戲幀率影響而保持穩定。
/// </para>
/// </summary>
public class MainMenuUI : UIBase
{
    #region Binding Components
    protected GameObject _rate;
    protected ButtonPlus _rateBtnPlus;
    protected GameObject _play;
    protected ButtonPlus _playBtnPlus;
    protected GameObject _rank;
    protected ButtonPlus _rankBtnPlus;
    protected Transform _birdTrans;

    /// <summary>
    /// Auto binding section
    /// <para>自動綁定區塊</para>
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._rate = this.collector.GetNode("Rate");
        this._rateBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Rate*BtnPlus");
        this._play = this.collector.GetNode("Play");
        this._playBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Play*BtnPlus");
        this._rank = this.collector.GetNode("Rank");
        this._rankBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Rank*BtnPlus");
        this._birdTrans = this.collector.GetNodeComponent<Transform>("Bird*Trans");
    }
    #endregion

    /// <summary>
    /// Called once when the instance is created.
    /// <para>實例建立時呼叫一次。</para>
    /// </summary>
    public override void OnCreate()
    {
        this._flyUpdater = new RTUpdater();
    }

    /// <summary>
    /// Called before showing, open sub objects here with async.
    /// <para>顯示前呼叫, 可在此以非同步開啟子物件。</para>
    /// </summary>
    protected override async UniTask OnPreShow()
    {
        /**
        * Open sub with async
        * 以非同步開啟子物件
        */
    }

    /// <summary>
    /// Called before closing, close sub objects here.
    /// <para>關閉前呼叫, 可在此關閉子物件。</para>
    /// </summary>
    protected override void OnPreClose()
    {
        /**
        * Close sub
        * 關閉子物件
        */
    }

    /// <summary>
    /// Bind component events here.
    /// <para>在此處綁定元件事件。</para>
    /// </summary>
    protected override void OnBind()
    {
        this._InitEvents();
    }

    /// <summary>
    /// Called on every show.
    /// <para>每次顯示時呼叫。</para>
    /// </summary>
    protected override void OnShow(object obj)
    {
        // Start the updater
        // 啟動 Updater
        this._flyUpdater.Start();
    }

    /// <summary>
    /// Called per frame while showing.
    /// <para>顯示期間每幀呼叫。</para>
    /// </summary>
    protected override void OnUpdate(float dt)
    {
    }

    /// <summary>
    /// Show animation, animationEnd must be invoked when it finishes.
    /// <para>顯示動畫, 結束時必須呼叫 animationEnd。</para>
    /// </summary>
    protected override void OnShowAnimation(AnimationEnd animationEnd)
    {
        // Must be called when the animation ends
        // 動畫結束時必須呼叫
        animationEnd();
    }

    /// <summary>
    /// Close animation, animationEnd must be invoked when it finishes.
    /// <para>關閉動畫, 結束時必須呼叫 animationEnd。</para>
    /// </summary>
    protected override void OnCloseAnimation(AnimationEnd animationEnd)
    {
        // Must be called when the animation ends
        // 動畫結束時必須呼叫
        animationEnd();
    }

    /// <summary>
    /// Called on close.
    /// <para>關閉時呼叫。</para>
    /// </summary>
    protected override void OnClose()
    {
        // Stop the updater
        // 停止 Updater
        this._flyUpdater.Stop();
    }

    /// <summary>
    /// Called on release (CloseAndDestroy).
    /// <para>釋放時呼叫 (CloseAndDestroy)。</para>
    /// </summary>
    public override void OnRelease()
    {

    }

    public float frequency = 10f;         // Preferred default, oscillation frequency (times/s) | 較佳預設值, 震動頻率 (次數/s)
    public float amplitude = 2f;          // Preferred default, oscillation amplitude | 較佳預設值, 震動幅度 (次數/s)
    public float yOffset = 0;             // Positional Y-offset | 位移 Y-Offset

    private float _elapsedDt = 0;         // Elapsed time | 消逝時間
    private RTUpdater _flyUpdater = null; // Standalone updater for the flying animation | 飛行動畫的獨立 Updater

    private void _InitEvents()
    {
        // Remove every listener first so no stale event remains
        // 移除所有事件, 確保沒有其他不必要的事件
        this._playBtnPlus.onClick.RemoveAllListeners();
        // Add the Play click event
        // 加入 Play 點擊事件
        this._playBtnPlus.onClick.AddListener(() =>
        {
            // Play the transition sound effect
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.SwooshingSfx).Forget();

            // Start the game
            // 開始遊戲
            CoreSystem.EnterGame();
        });

        // Use a standalone real-time updater at 9 FPS so the flying animation stays steady
        // 獨立建立以現實時間控制的 Updater 進行穩定刷新飛行動畫, 並設置 9 FrameRate
        this._flyUpdater.targetFrameRate = 9;
        this._flyUpdater.onUpdate = this._UpdateBirdFlyWave;
    }

    private void _UpdateBirdFlyWave(float dt)
    {
        // Formula: [1 deg = 180 deg / pi, 1 rad = pi / 180 deg]
        //         1 rad = (pi / 180) * 1 deg => degree-to-radian constant
        //         1 deg = (180 / pi) * 1 rad => radian-to-degree constant
        // Extra:
        // pi / 2 = 90 deg
        // (pi * 2) * r = circumference
        // pi = 180 deg
        // pi / 2 = 180 / 2 = 90 deg
        // 公式:  【1° = 180°/π, 1rad = π/180°】
        //         1弧度 = (π/180) * 1角度 => 角度轉弧度常數
        //         1角度 = (180/π) * 1弧度 => 弧度轉角度常數
        // 額外:
        // π / 2 = 90°
        // (π * 2) * r = 圓周長
        // π = 180°
        // π / 2 = 180 / 2  = 90°

        // Accumulate the elapsed time
        // 記錄消逝時間
        this._elapsedDt += dt;

        // Compute the current theta from the elapsed time and the frequency
        // 以目前消逝的時間和頻率計算現在的 θ
        float theta = this.frequency * this._elapsedDt;
        //Debug.Log($"Theta: {theta}");

        // Compute the y-axis wave (it swings up and down, so drive y)
        // 計算 y-axis wave (上下擺動, 所以控制 y)
        float yWave = this.amplitude * Mathf.Sin(theta) + this.yOffset;
        //Debug.Log($"Mathf.Sin: {Mathf.Sin(theta)}");

        // Apply the positional offset
        // 座標位移計算
        if (this._birdTrans != null) this._birdTrans.position += new Vector3(0, yWave, 0);
    }
}
