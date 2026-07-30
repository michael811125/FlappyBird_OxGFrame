using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.SRFrame;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gameplay scene resource. Picks a random background and bird, scrolls the ground
/// and spawns pipes on a timer while the run is active.
/// <para>
/// 遊戲場景資源。隨機挑選背景與小鳥、捲動地面,
/// 並在遊戲進行中依計時器生成水管。
/// </para>
/// </summary>
public class GameplaySR : SRBase
{
    #region Binding Components
    protected SpriteRenderer _bgSprRen;
    protected Renderer _groundRen;
    protected Transform _birdContainerTrans;
    protected Transform _pipeContainerTrans;

    /// <summary>
    /// Auto binding section
    /// <para>自動綁定區塊</para>
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._bgSprRen = this.collector.GetNodeComponent<SpriteRenderer>("Bg*SprRen");
        this._groundRen = this.collector.GetNodeComponent<Renderer>("Ground*Ren");
        this._birdContainerTrans = this.collector.GetNodeComponent<Transform>("BirdContainer*Trans");
        this._pipeContainerTrans = this.collector.GetNodeComponent<Transform>("PipeContainer*Trans");
    }
    #endregion

    /// <summary>
    /// Called once when the instance is created.
    /// <para>實例建立時呼叫一次。</para>
    /// </summary>
    public override void OnCreate()
    {
        /**
         * Do somethings init once in here
         * 在此處進行僅一次的初始化
         */
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
    }

    /// <summary>
    /// Called on every show.
    /// <para>每次顯示時呼叫。</para>
    /// </summary>
    protected override void OnShow(object obj)
    {
        this._InitBackground();
        this._InitBird();
    }

    /// <summary>
    /// Called per frame while showing.
    /// <para>顯示期間每幀呼叫。</para>
    /// </summary>
    protected override void OnUpdate(float dt)
    {
        if (CoreSystem.IsGameStart())
        {
            this._UpdateGroundScroll();
            this._UpdatePipeGenerator(dt);
        }
    }

    /// <summary>
    /// Called on close.
    /// <para>關閉時呼叫。</para>
    /// </summary>
    protected override void OnClose()
    {

    }

    /// <summary>
    /// Called on release (CloseAndDestroy).
    /// <para>釋放時呼叫 (CloseAndDestroy)。</para>
    /// </summary>
    public override void OnRelease()
    {

    }

    [Header("Ground Scroll Speed")]
    public float scrollSpeed = 0.5f;

    [Header("Background Options")]
    public List<Sprite> bgs = new List<Sprite>();

    [Header("Bird Options")]
    public List<GameObject> birds = new List<GameObject>();

    [Header("Pipe Options")]
    public Vector3 pipeStartSpawnPosition = new Vector3(5, 0, 0);
    public float pipeIntervalTime = 1.5f;
    public List<GameObject> pipes = new List<GameObject>();
    private float _pipeIntervalTimer;

    private void _UpdateGroundScroll()
    {
        Vector2 textureOffset = new Vector2(Time.time * this.scrollSpeed, 0);
        this._groundRen.material.mainTextureOffset = textureOffset;
    }

    private void _UpdatePipeGenerator(float dt)
    {
        this._pipeIntervalTimer += dt;
        if (this._pipeIntervalTimer > this.pipeIntervalTime)
        {
            // Wait for some time, create an obstacle, then set wait time to 0 and start again
            // 等待一段時間後生成障礙物, 再將等待時間歸零重新計算
            this._pipeIntervalTimer = 0;
            int idx = Random.Range(0, this.pipes.Count);
            GameObject instPipe = Instantiate(this.pipes[idx], this.pipeStartSpawnPosition, Quaternion.identity, this._pipeContainerTrans);
            instPipe.name = $"Pipe_{idx}";
        }
    }

    private void _InitBackground()
    {
        int idx = Random.Range(0, this.bgs.Count);
        this._bgSprRen.sprite = this.bgs[idx];
    }

    private void _InitBird()
    {
        int idx = Random.Range(0, this.birds.Count);
        Instantiate(this.birds[idx], this._birdContainerTrans.position, Quaternion.identity, this._birdContainerTrans);
    }
}
