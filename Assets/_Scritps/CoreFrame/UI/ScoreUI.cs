using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScoreUI : UIBase
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
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */

        this._UpdateScoreText();

        if (CoreSystem.IsGameStart())
        {
            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                CoreSystem.AddScore();
            }
        }
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

    }

    public override void OnRelease()
    {

    }

    // 初始 ScoreUI 相關組件
    private Text _scoreTxt;

    private void _InitComponents()
    {
        this._scoreTxt = this.collector.GetNodeComponent<Text>("Score*Txt");
    }

    private void _UpdateScoreText()
    {
        this._scoreTxt.text = CoreSystem.GetScore().ToString();
    }
}
