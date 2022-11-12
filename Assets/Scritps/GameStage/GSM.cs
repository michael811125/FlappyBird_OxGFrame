using OxGFrame.GSIFrame;

public class GSM : GameStageManagerBase<GSM>
{
    /*
    // >>> EXAMPLE <<<

    public static byte none = 0;
    public static byte startupStage = 1;
    public static byte logoStage = 2;
    public static byte patchStage = 3;
    public static byte loginStage = 4;
    public static byte enterStage = 5;

    // >>> EXAMPLE <<<
    */

    public const byte MAIN_MENU_STAGE_ID = 1;
    public const byte GAME_PLAY_STAGE_ID = 2;

    public GSM()
    {
        /*
        // >>> EXAMPLE <<<

        GameStageBase gameStage;

        // None
        gameStage = new NoneStageExample(GameStageManager.none);
        this.AddGameStage(GameStageManager.none, gameStage);

        // 1. Game Startup Stage
        gameStage = new StartupStageExample(GameStageManager.startupStage);
        this.AddGameStage(GameStageManager.startupStage, gameStage);

        // 2. Game Logo Show Stage
        gameStage = new LogoStageExample(GameStageManager.logoStage);
        this.AddGameStage(GameStageManager.logoStage, gameStage);

        // 3. Game Patch Stage (The App in this stage is Hot-Update stage. The H5 doesn't require Hot-Update, so you can to do preload in this Stage.)
        gameStage = new PatchStageExample(GameStageManager.patchStage);
        this.AddGameStage(GameStageManager.patchStage, gameStage);

        // 4. Game Login Stage
        gameStage = new LoginStageExample(GameStageManager.loginStage);
        this.AddGameStage(GameStageManager.loginStage, gameStage);

        // 5. Game Enter Stage
        gameStage = new EnterStageExample(GameStageManager.enterStage);
        this.AddGameStage(GameStageManager.enterStage, gameStage);

        // ※Remark: Custom create your own stage of game

        // >>> EXAMPLE <<<
        */

        GameStageBase gameStage;

        // 1. 主選單階段
        gameStage = new MainMenuStage(MAIN_MENU_STAGE_ID);
        this.AddGameStage(MAIN_MENU_STAGE_ID, gameStage);

        // 2. 遊戲階段
        gameStage = new GamePlayStage(GAME_PLAY_STAGE_ID);
        this.AddGameStage(GAME_PLAY_STAGE_ID, gameStage);
    }

    /// <summary>
    /// Call by Main MonoBehaviour Start (Such as CoreSystem)
    /// </summary>
    public override void OnStart()
    {
        /* Start first game stage in here. (like this ↓)  */
        //this.ChangeGameStage(GameStageManager.startupStage);

        // 首次遊戲階段 (主選單階段)
        this.ChangeGameStage(MAIN_MENU_STAGE_ID);
    }

    /// <summary>
    /// Call by Main MonoBehaviour Update (Such as CoreSystem)
    /// </summary>
    /// <param name="dt"></param>
    public override void OnUpdate(float dt = 0.0f)
    {
        base.OnUpdate(dt); // Must be called
    }
}

