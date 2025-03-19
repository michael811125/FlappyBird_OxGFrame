using Cysharp.Threading.Tasks;
using OxGFrame.MediaFrame;
using OxGKit.SingletonSystem;
using UnityEngine;

public class CoreSystem : MonoSingleton<CoreSystem>
{
    public static int frameCount = 0;
    public static float deltaTime = 0f;

    [Range(24, 60)]
    public int frameRate = 30;

    private static int _currentScore;

    protected override void OnCreate()
    {
        // Init instance first
        GetInstance();
    }

    protected override void OnStart()
    {
        // 30 for Mobile, 60 for Desktop
        Application.targetFrameRate = this.frameRate;

        // 驅動 GSIManager 的 Start
        GSIManager.DriveStart();
    }

    private void Update()
    {
        frameCount = Time.frameCount;
        deltaTime = Time.deltaTime;

        // 驅動 GSIManager 的 Update
        GSIManager.DriveUpdate(deltaTime);

        // 每 60 幀刷新檢查 FrameRate 設置
        if (Time.frameCount % 60 == 0) Application.targetFrameRate = this.frameRate;
    }

    protected override void OnRelease()
    {

    }

    #region Score Controls
    /// <summary>
    /// 重置分數
    /// </summary>
    public static void ResetScore()
    {
        // 歸零
        _currentScore = 0;
    }

    /// <summary>
    /// 增加分數
    /// </summary>
    public static void AddScore()
    {
        // 分數 + 1
        _currentScore++;

        // 播放增加分數音效
        MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.ScoreSfx).Forget();
    }

    /// <summary>
    /// 取得分數
    /// </summary>
    /// <returns></returns>
    public static int GetScore()
    {
        return _currentScore;
    }

    /// <summary>
    /// 記錄最佳分數
    /// </summary>
    /// <param name="score"></param>
    public static void SaveBestScore()
    {
        // 判斷分數如果有 > 最佳分數, 才進行記錄
        if (GetScore() > GetBestScore()) PlayerPrefs.SetInt("BestScore", GetScore());
    }

    /// <summary>
    /// 取得最佳分數
    /// </summary>
    /// <returns></returns>
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }
    #endregion

    #region Game Stage Controls
    /// <summary>
    /// [Global] 遊戲是否開始
    /// </summary>
    /// <returns></returns>
    public static bool IsGameStart()
    {
        return GSIManager.GetStage<GameplayStage>().IsGameStart();
    }

    /// <summary>
    /// 前往主選單階段
    /// </summary>
    public static void GoToMenu()
    {
        // 切換至 MainMenuStage
        GSIManager.ChangeStage<MainMenuStage>();
    }

    /// <summary>
    /// 進入遊戲
    /// </summary>
    public static void EnterGame()
    {
        // 切換 MainMenuStage 步驟
        GSIManager.GetStage<MainMenuStage>().ChangeStep(MainMenuStage.MainMenuStep.START_GAME_PLAY);
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    public static void StartGame()
    {
        // 切換 GamingStage 步驟
        GSIManager.GetStage<GameplayStage>().ChangeStep(GameplayStage.GamePlayStep.START_GAME);
    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    public static void GameOver()
    {
        // 切換 GamingStage 步驟
        GSIManager.GetStage<GameplayStage>().ChangeStep(GameplayStage.GamePlayStep.GAMEOVER);
    }

    /// <summary>
    /// 重新遊玩 (實際上就是強制重新切換一次 GamePlayStage)
    /// </summary>
    public static void Replay()
    {
        GSIManager.ChangeStage<GameplayStage>(true);
    }
    #endregion
}
