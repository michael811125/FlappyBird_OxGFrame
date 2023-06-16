using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.SRFrame;

public class GamePlaySC : SRBase
{
    public override void OnInit()
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
        this._InitComponents();
    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Somethings Init With Every Showing In Here
         */

        this._InitBackground();
        this._InitBird();
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */

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

    // 初始 GamePlaySC 相關組件

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

    private SpriteRenderer _bg;
    private Renderer _ground;
    private Transform _birdContainer;
    private Transform _pipeContainer;

    private void _InitComponents()
    {
        // Note: collector.GetNode return type is GameObject

        // 綁定方式 (bind with name, also you can use drag to assign it) show you how to bind
        this._bg = this.collector.GetNode("Bg").GetComponent<SpriteRenderer>();
        this._ground = this.collector.GetNode("Ground").GetComponent<Renderer>();
        this._birdContainer = this.collector.GetNode("BirdContainer").transform;
        this._pipeContainer = this.collector.GetNode("PipeContainer").transform;
    }

    private void _UpdateGroundScroll()
    {
        Vector2 textureOffset = new Vector2(Time.time * this.scrollSpeed, 0);
        this._ground.material.mainTextureOffset = textureOffset;
    }

    private void _UpdatePipeGenerator(float dt)
    {
        this._pipeIntervalTimer += dt;
        if (this._pipeIntervalTimer > this.pipeIntervalTime)
        {
            // Wait for some time, create an obstacle, then set wait time to 0 and start again
            this._pipeIntervalTimer = 0;
            int idx = Random.Range(0, this.pipes.Count);
            GameObject instPipe = Instantiate(this.pipes[idx], this.pipeStartSpawnPosition, Quaternion.identity, this._pipeContainer);
            instPipe.name = $"Pipe_{idx}";
        }
    }

    private void _InitBackground()
    {
        int idx = Random.Range(0, this.bgs.Count);
        this._bg.sprite = this.bgs[idx];
    }

    private void _InitBird()
    {
        int idx = Random.Range(0, this.birds.Count);
        Instantiate(this.birds[idx], this._birdContainer.position, Quaternion.identity, this._birdContainer);
    }
}
