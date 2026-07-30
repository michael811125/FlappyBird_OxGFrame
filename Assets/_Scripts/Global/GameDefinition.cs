#define BUNDLE_TYPE

/// <summary>
/// YooAsset package names used by this project.
/// <para>
/// 本專案使用的 YooAsset Package 名稱。
/// </para>
/// </summary>
public static class Pkgs
{
    public readonly static string BuiltinPkg = "BuiltinPkg";
    public readonly static string PatchPkg = "PatchPkg";
}

/// <summary>
/// Scene resource names. The res# prefix tells OxGFrame to load from Resources;
/// the AssetBundle branches use a package name instead.
/// <para>
/// 場景資源名稱。res# 前綴代表由 OxGFrame 從 Resources 載入；
/// AssetBundle 分支則改用 Package 名稱。
/// </para>
/// </summary>
public static class SRs
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}CoreFrame/SR/";
#endif

    // Scenes
    // 場景
    public readonly static string MainMenuSR = $"{_path}MainMenuSR";       // Main menu scene | 主選單場景
    public readonly static string GameplaySR = $"{_path}GameplaySR";       // Gameplay scene | 遊戲場景
}

/// <summary>
/// UI asset names, resolved the same way as SRs.
/// <para>
/// UI 資源名稱, 解析方式與 SRs 相同。
/// </para>
/// </summary>
public static class UIs
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}CoreFrame/UI/";
#endif

    // UIs
    // 介面
    public readonly static string AwaitingUI = $"{_path}AwaitingUI";       // Awaiting UI (common) | 等待介面 (Common)
    public readonly static string DescriptionUI = $"{_path}DescriptionUI"; // Description UI (common) | 說明介面 (Common)
    public readonly static string DoubleCheckUI = $"{_path}DoubleCheckUI"; // Double check UI (common) | 重複確認介面 (Common)
    public readonly static string LoadingUI = $"{_path}LoadingUI";         // Loading UI (common) | 加載介面 (Common)
    public readonly static string PatchUI = $"{_path}PatchUI";             // Patch UI | 補丁更新介面
    public readonly static string LogoUI = $"{_path}LogoUI";               // Logo UI | 商標介面
    public readonly static string MainMenuUI = $"{_path}MainMenuUI";       // Main menu UI | 主選單介面
    public readonly static string GetReadyUI = $"{_path}GetReadyUI";       // Get ready UI | 準備介面
    public readonly static string ScoreUI = $"{_path}ScoreUI";             // Score UI | 分數介面
    public readonly static string SettlementUI = $"{_path}SettlementUI";   // Settlement UI | 結算介面
}

/// <summary>
/// Audio asset names, resolved the same way as SRs.
/// <para>
/// 音訊資源名稱, 解析方式與 SRs 相同。
/// </para>
/// </summary>
public static class Audios
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}MediaFrame/Audio/";
#endif

    // Background music (BGM)
    // 背景音樂 BGM
    public readonly static string MenuBgm = $"{_path}MenuBgm";             // Main menu BGM | 主選單背景音樂

    // General sound effects
    // 通用音效 General
    public readonly static string SwooshingSfx = $"{_path}SwooshingSfx";   // Transition SFX | 轉場音效

    // Interaction sound effects
    // 互動音效 Interact
    public readonly static string ScoreSfx = $"{_path}ScoreSfx";           // Scoring SFX | 分數音效
    public readonly static string HitSfx = $"{_path}HitSfx";               // Hit SFX | 撞擊音效
    public readonly static string FlySfx = $"{_path}FlySfx";               // Flying SFX | 飛起音效
}