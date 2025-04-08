#define BUNDLE_TYPE

public static class Pkgs
{
    public readonly static string BuiltinPkg = "BuiltinPkg";
    public readonly static string PatchPkg = "PatchPkg";
}

public static class SRs
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}CoreFrame/SR/";
#endif

    // 場景   
    public readonly static string MainMenuSR = $"{_path}MainMenuSR";       // 主選單場景
    public readonly static string GameplaySR = $"{_path}GameplaySR";       // 遊戲場景
}

public static class UIs
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}CoreFrame/UI/";
#endif

    // 介面
    public readonly static string AwaitingUI = $"{_path}AwaitingUI";       // 等待介面 (Common)
    public readonly static string DescriptionUI = $"{_path}DescriptionUI"; // 說明介面 (Common)
    public readonly static string DoubleCheckUI = $"{_path}DoubleCheckUI"; // 重複確認介面 (Common)
    public readonly static string LoadingUI = $"{_path}LoadingUI";         // 加載介面 (Common)
    public readonly static string PatchUI = $"{_path}PatchUI";             // 補丁更新介面
    public readonly static string LogoUI = $"{_path}LogoUI";               // 商標介面
    public readonly static string MainMenuUI = $"{_path}MainMenuUI";       // 主選單介面
    public readonly static string GetReadyUI = $"{_path}GetReadyUI";       // 準備介面
    public readonly static string ScoreUI = $"{_path}ScoreUI";             // 分數介面
    public readonly static string SettlementUI = $"{_path}SettlementUI";   // 結算介面
}

public static class Audios
{
#if BUNDLE_TYPE
    private static string _path = "";
#else
    private const string _PREFIX = "res#";
    private static string _path = $"{_PREFIX}MediaFrame/Audio/";
#endif

    // 背景音樂 BGM
    public readonly static string MenuBgm = $"{_path}MenuBgm";             // 主選單背景音樂

    // 通用音效 General
    public readonly static string SwooshingSfx = $"{_path}SwooshingSfx";   // 轉場音效

    // 互動音效 Interact
    public readonly static string ScoreSfx = $"{_path}ScoreSfx";           // 分數音效
    public readonly static string HitSfx = $"{_path}HitSfx";               // 撞擊音效
    public readonly static string FlySfx = $"{_path}FlySfx";               // 飛起音效
}