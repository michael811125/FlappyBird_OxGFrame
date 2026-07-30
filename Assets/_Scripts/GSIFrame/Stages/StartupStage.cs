using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader;
using OxGFrame.GSIFrame;

/// <summary>
/// First game stage. Kept intentionally empty so it is obvious where project wide
/// bootstrapping (config, locale, account, ...) would go before anything is shown.
/// <para>
/// 第一個遊戲階段。刻意保持空白, 讓人一眼看出專案層級的啟動初始化
/// (設定檔、語系、帳號等) 應該放在畫面出現之前的哪個位置。
/// </para>
/// </summary>
public class StartupStage : GSIBase
{
    // Execution order: OnCreate (once) > OnEnter (every change) > OnUpdate > OnExit
    // 執行順序: OnCreate (僅一次) > OnEnter (每次切換) > OnUpdate > OnExit

    /// <summary>
    /// Called once when the game stage is created.
    /// <para>遊戲階段建立時呼叫一次。</para>
    /// </summary>
    public async override UniTask OnCreate()
    {
        /* Do somethings OnCreate once in here */
        /* 在此處進行僅一次的 OnCreate 初始化 */
    }

    /// <summary>
    /// Called every time this game stage is entered.
    /// <para>每次進入此遊戲階段時呼叫。</para>
    /// </summary>
    public async override UniTask OnEnter()
    {

    }

    /// <summary>
    /// Called per frame while this game stage is running.
    /// <para>此遊戲階段運行期間每幀呼叫。</para>
    /// </summary>
    public override void OnUpdate(float dt = 0.0f)
    {
        // Poll until the bundle related config has finished initializing
        // 輪詢 Bundle 相關配置是否已經初始完畢
        if (AssetPatcher.IsInitialized())
        {
            this.StopUpdate();
            GSIManager.ChangeStage<LogoStage>();
        }
    }

    /// <summary>
    /// Called when leaving this game stage.
    /// <para>離開此遊戲階段時呼叫。</para>
    /// </summary>
    public override void OnExit()
    {
        /* Do somethings OnExit in here */
        /* 在此處進行 OnExit 的處理 */
    }
}
