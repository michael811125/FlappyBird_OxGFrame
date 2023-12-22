using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.SRFrame;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySR : SRBase
{
    #region Binding Components
    protected SpriteRenderer _bgSprRen;
    protected Renderer _groundRen;
    protected Transform _birdContainerTrans;
    protected Transform _pipeContainerTrans;

    /// <summary>
    /// Auto Binding Section
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

    public override void OnCreate()
    {
        /**
         * Do Somethings Init Once In Here
         */
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
    }

    protected override void OnShow(object obj)
    {
        this._InitBackground();
        this._InitBird();
    }

    protected override void OnUpdate(float dt)
    {
        if (CoreSystem.IsGameStart())
        {
            this._UpdateGroundScroll();
            this._UpdatePipeGenerator(dt);
        }
    }

    protected override void OnClose()
    {

    }

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
