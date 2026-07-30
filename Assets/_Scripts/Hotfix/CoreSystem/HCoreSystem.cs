using Cysharp.Threading.Tasks;
using OxGFrame.MediaFrame;
using OxGKit.SingletonSystem;
using UnityEngine;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// [Hotfix.Assembly] The main program of the hotfix project.
    /// It lives in the hotfix main scene, is loaded only after the hotfix assemblies
    /// are in place, and drives HGSIManager for the whole gameplay flow.
    /// <para>
    /// [熱更工程] 熱更工程的主程序。
    /// 掛載於熱更主場景, 於熱更程序集就位後才被載入,
    /// 並驅動 HGSIManager 帶起整個遊戲流程。
    /// </para>
    /// </summary>
    public class HCoreSystem : MonoSingleton<HCoreSystem>
    {
        /// <summary>
        /// Cached Time.frameCount, so other scripts do not have to touch the Unity API.
        /// <para>快取的 Time.frameCount, 讓其他腳本不必再存取 Unity API。</para>
        /// </summary>
        public static int frameCount = 0;

        /// <summary>
        /// Cached Time.deltaTime of the current frame.
        /// <para>快取的當幀 Time.deltaTime。</para>
        /// </summary>
        public static float deltaTime = 0f;

        /// <summary>
        /// Target frame rate, adjustable in the inspector.
        /// <para>目標幀率, 可於 Inspector 調整。</para>
        /// </summary>
        [Range(24, 60)]
        public int frameRate = 30;

        /// <summary>
        /// Score of the current run (reset on every replay).
        /// <para>本局分數 (每次重新遊玩都會歸零)。</para>
        /// </summary>
        private static int _currentScore;

        /// <summary>
        /// Called once when the MonoSingleton is created.
        /// <para>MonoSingleton 建立時呼叫一次。</para>
        /// </summary>
        protected override void OnCreate()
        {
            // Init loggers
            // 初始化 Logger
            Loggers.InitLoggers();

            // Init instance first
            // 先初始化實例
            GetInstance();
        }

        /// <summary>
        /// Called on the MonoBehaviour Start.
        /// <para>於 MonoBehaviour 的 Start 呼叫。</para>
        /// </summary>
        protected override void OnStart()
        {
            // 30 for Mobile, 60 for Desktop
            // 行動裝置用 30, 桌機用 60
            Application.targetFrameRate = this.frameRate;

            // Drive the GSIManager start flow (this fires the first game stage)
            // 驅動 GSIManager 的 Start (此處會啟動第一個遊戲階段)
            HGSIManager.DriveStart();
        }

        private void Update()
        {
            frameCount = Time.frameCount;
            deltaTime = Time.deltaTime;

            // Drive the GSIManager update flow (this drives OnUpdate of the running stage)
            // 驅動 GSIManager 的 Update (此處會驅動運行中階段的 OnUpdate)
            HGSIManager.DriveUpdate(deltaTime);

            // Refresh the frame rate setting every 60 frames
            // 每 60 幀刷新檢查 FrameRate 設置
            if (Time.frameCount % 60 == 0) Application.targetFrameRate = this.frameRate;
        }

        /// <summary>
        /// Called when the MonoSingleton is released.
        /// <para>MonoSingleton 釋放時呼叫。</para>
        /// </summary>
        protected override void OnRelease()
        {

        }

        #region Score Controls
        /// <summary>
        /// Reset the score
        /// <para>重置分數</para>
        /// </summary>
        public static void ResetScore()
        {
            // Set back to zero
            // 歸零
            _currentScore = 0;
        }

        /// <summary>
        /// Add to the score
        /// <para>增加分數</para>
        /// </summary>
        public static void AddScore()
        {
            // Score + 1
            // 分數 + 1
            _currentScore++;

            // Play the scoring sound effect
            // 播放增加分數音效
            MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.ScoreSfx).Forget();
        }

        /// <summary>
        /// Get the score of the current run
        /// <para>取得分數</para>
        /// </summary>
        /// <returns></returns>
        public static int GetScore()
        {
            return _currentScore;
        }

        /// <summary>
        /// Record the best score
        /// <para>記錄最佳分數</para>
        /// </summary>
        /// <param name="score"></param>
        public static void SaveBestScore()
        {
            // Only record when the current score beats the best score
            // 判斷分數如果有 > 最佳分數, 才進行記錄
            if (GetScore() > GetBestScore()) PlayerPrefs.SetInt("BestScore", GetScore());
        }

        /// <summary>
        /// Get the best score
        /// <para>取得最佳分數</para>
        /// </summary>
        /// <returns></returns>
        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt("BestScore", 0);
        }
        #endregion

        #region Game Stage Controls
        /// <summary>
        /// [Global] Whether the gameplay has started
        /// <para>[全域] 遊戲是否開始</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsGameStart()
        {
            return HGSIManager.GetStage<GameplayStage>().IsGameStart();
        }

        /// <summary>
        /// Go to the main menu stage
        /// <para>前往主選單階段</para>
        /// </summary>
        public static void GoToMenu()
        {
            // Change to MainMenuStage
            // 切換至 MainMenuStage
            HGSIManager.ChangeStage<MainMenuStage>();
        }

        /// <summary>
        /// Enter the game
        /// <para>進入遊戲</para>
        /// </summary>
        public static void EnterGame()
        {
            // Change the step of MainMenuStage
            // 切換 MainMenuStage 步驟
            HGSIManager.GetStage<MainMenuStage>().ChangeStep(MainMenuStage.MainMenuStep.START_GAME_PLAY);
        }

        /// <summary>
        /// Start the game
        /// <para>開始遊戲</para>
        /// </summary>
        public static void StartGame()
        {
            // Change the step of GameplayStage
            // 切換 GamingStage 步驟
            HGSIManager.GetStage<GameplayStage>().ChangeStep(GameplayStage.GamePlayStep.START_GAME);
        }

        /// <summary>
        /// Game over
        /// <para>遊戲結束</para>
        /// </summary>
        public static void GameOver()
        {
            // Change the step of GameplayStage
            // 切換 GamingStage 步驟
            HGSIManager.GetStage<GameplayStage>().ChangeStep(GameplayStage.GamePlayStep.GAMEOVER);
        }

        /// <summary>
        /// Replay (in practice this just force-changes to GameplayStage again)
        /// <para>重新遊玩 (實際上就是強制重新切換一次 GamePlayStage)</para>
        /// </summary>
        public static void Replay()
        {
            HGSIManager.ChangeStage<GameplayStage>(true);
        }
        #endregion
    }
}