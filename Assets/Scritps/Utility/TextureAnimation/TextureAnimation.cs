using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextureAnimation : MonoBehaviour
{
    enum PlayMode
    {
        Normal,
        Reverse,
        PingPong,
        PingPongReverse
    }

    [SerializeField, Tooltip("Sprites")]
    private List<Sprite> animSprites = new List<Sprite>();
    [SerializeField, Tooltip("Loop play")]
    private bool isLoop = false;
    [SerializeField, Tooltip("Play Mode")]
    private PlayMode playMode = PlayMode.Normal;
    [SerializeField, Tooltip("Frame rate (FPS)")]
    private int frameRate = 30;
    [SerializeField, Tooltip("Ignore Time.Scale")]
    private bool ignoreTimeScale = true;

    private float _dt = 0;
    private int _spIdx = 0;

    private bool _pingPongStart = false;
    private int _pingPongCount = 0;

    private SpriteRenderer _spr = null;
    private Image _image = null;

    private void Awake()
    {
        do
        {
            this._spr = this.transform.GetComponent<SpriteRenderer>();
            if (this._spr != null) break;
            this._image = this.transform.GetComponent<Image>();
            if (this._image != null) break;
        } while (false);
    }

    private void Start()
    {
        this._Reset();
    }

    private void Update()
    {
        if (this.animSprites.Count == 0) return;

        this._UpdateTextureAnimation((this.ignoreTimeScale) ? Time.unscaledDeltaTime : Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // Ensure continuous Update calls.
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
        }
#endif
    }

    private void OnValidate()
    {
        this._Reset();
    }

    private void OnEnable()
    {
        this._Reset();
    }

    private void _Reset()
    {
        this._dt = 0;
        this._pingPongCount = 0;
        if (this.playMode == PlayMode.PingPong) this._pingPongStart = true;
        else if (this.playMode == PlayMode.PingPongReverse) this._pingPongStart = false;
    }

    private void _AutoRefreshSprite(Sprite sp)
    {
        if (this._spr != null) this._spr.sprite = sp;
        else if (this._image != null) this._image.sprite = sp;
    }

    private void _UpdateTextureAnimation(float dt)
    {
        float fps = this.frameRate;
        this._dt += dt;

        this._spIdx = Mathf.FloorToInt(this._dt * fps);

        switch (this.playMode)
        {
            case PlayMode.Normal:
                this._ModeNormal();
                break;

            case PlayMode.Reverse:
                this._ModeReverse();
                break;

            case PlayMode.PingPong:
                this._ModePingPong();
                break;

            case PlayMode.PingPongReverse:
                this._ModePingPongReverse();
                break;
        }
    }

    private void _ModeNormal()
    {
        if (!this.isLoop && this._spIdx >= this.animSprites.Count) return;

        this._spIdx %= this.animSprites.Count;
        this._AutoRefreshSprite(this.animSprites[this._spIdx]);
    }

    private void _ModeReverse()
    {
        if (!this.isLoop && this._spIdx >= this.animSprites.Count) return;

        this._spIdx %= this.animSprites.Count;
        int lastFrame = this.animSprites.Count - 1;
        int revSpIdx = lastFrame - this._spIdx;
        this._AutoRefreshSprite(this.animSprites[revSpIdx]);
    }

    private void _ModePingPong()
    {
        if (this._pingPongStart)
        {
            if (!this.isLoop)
            {
                if (this._pingPongCount >= 2) return;
            }

            if (this._spIdx >= (this.animSprites.Count - 1))
            {
                if (!this.isLoop) this._pingPongCount++;

                this._pingPongStart = false;
                this._dt = 0;
            }

            this._spIdx %= this.animSprites.Count;
            this._AutoRefreshSprite(this.animSprites[this._spIdx]);
        }
        else
        {
            if (this._spIdx >= (this.animSprites.Count - 1))
            {
                if (!this.isLoop) this._pingPongCount++;

                this._pingPongStart = true;
                this._dt = 0;
            }

            this._spIdx %= this.animSprites.Count;
            int lastFrame = this.animSprites.Count - 1;
            int reverseSpIdx = lastFrame - this._spIdx;
            this._AutoRefreshSprite(this.animSprites[reverseSpIdx]);
        }
    }

    private void _ModePingPongReverse()
    {
        if (this._pingPongStart)
        {
            if (this._spIdx >= (this.animSprites.Count - 1))
            {
                if (!this.isLoop) this._pingPongCount++;

                this._pingPongStart = false;
                this._dt = 0;
            }

            this._spIdx %= this.animSprites.Count;
            this._AutoRefreshSprite(this.animSprites[this._spIdx]);
        }
        else
        {
            if (!this.isLoop)
            {
                if (this._pingPongCount >= 2) return;
            }

            if (this._spIdx >= (this.animSprites.Count - 1))
            {
                if (!this.isLoop) this._pingPongCount++;

                this._pingPongStart = true;
                this._dt = 0;
            }

            this._spIdx %= this.animSprites.Count;
            int lastFrame = this.animSprites.Count - 1;
            int reverseSpIdx = lastFrame - this._spIdx;
            this._AutoRefreshSprite(this.animSprites[reverseSpIdx]);
        }
    }
}
