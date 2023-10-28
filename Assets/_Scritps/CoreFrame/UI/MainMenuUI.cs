using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using OxGFrame.MediaFrame;
using OxGKit.Utilities.Timer;

public class MainMenuUI : UIBase
{
    public override void OnCreate()
    {
        this._flyUpdater = new RTUpdater();
    }

    protected override async UniTask OnPreShow()
    {
        /**
        * Open Sub With Async
        */
    }

    protected override void OnPreClose()
    {
        /**
        * Close Sub
        */
    }

    protected override void OnBind()
    {
        this._InitComponents();
        this._InitEvents();
    }

    protected override void OnShow(object obj)
    {
        // 啟動 Updater
        this._flyUpdater.Start();
    }

    protected override void OnUpdate(float dt)
    {
    }

    protected override void ShowAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void HideAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void OnClose()
    {
        // 停止 Updater
        this._flyUpdater.Stop();
    }

    public override void OnRelease()
    {

    }

    public float frequency = 10f;         // 較佳預設值, 震動頻率 (次數/s)
    public float amplitude = 2f;          // 較佳預設值, 震動幅度 (次數/s)
    public float yOffset = 0;             // 位移 Y-Offset
                                          
    private float _elapsedDt = 0;         // 消逝時間
    private RTUpdater _flyUpdater = null; // 飛行動畫的獨立 Updater 

    // 組件拖曳方式
    public Button rate;
    public Button rank;

    // 組件綁定方式
    private Transform _bird;
    private Button _playBtn;


    private void _InitComponents()
    {
        this._bird = this.collector.GetNode("Bird").transform;
        this._playBtn = this.collector.GetNode("Play").GetComponentInChildren<Button>();
    }

    private void _InitEvents()
    {
        // 移除所有事件, 確保沒有其他不必要的事件
        this._playBtn.onClick.RemoveAllListeners();
        // 加入 Play 點擊事件
        this._playBtn.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Audios.SwooshingSfx).Forget();

            // 開始遊戲
            CoreSystem.EnterGame();
        });

        // 獨立建立以現實時間控制的 Updater 進行穩定刷新飛行動畫, 並設置 9 FrameRate
        this._flyUpdater.targetFrameRate = 9;
        this._flyUpdater.onUpdate = this._UpdateBirdFlyWave;
    }

    private void _UpdateBirdFlyWave(float dt)
    {
        // 公式:  【1° = 180°/π, 1rad = π/180°】
        //         1弧度 = (π/180) * 1角度 => 角度轉弧度常數
        //         1角度 = (180/π) * 1弧度 => 弧度轉角度常數
        // 額外:
        // π / 2 = 90°
        // (π * 2) * r = 圓周長
        // π = 180°
        // π / 2 = 180 / 2  = 90°

        // 記錄消逝時間
        this._elapsedDt += dt;

        // 以目前消逝的時間和頻率計算現在的 θ
        float theta = this.frequency * this._elapsedDt;
        //Debug.Log($"Theta: {theta}");

        // 計算 y-axis wave (上下擺動, 所以控制 y)
        float yWave = this.amplitude * Mathf.Sin(theta) + this.yOffset;
        //Debug.Log($"Mathf.Sin: {Mathf.Sin(theta)}");

        // 座標位移計算
        this._bird.position += new Vector3(0, yWave, 0);
    }
}
