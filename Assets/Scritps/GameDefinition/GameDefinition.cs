public static class AssetGroup
{
    public const int None = 0;
}

public static class SCPath
{
    private const string _path = "CoreFrame/SC/";

    // 場景   
    public readonly static string MainMenuSC = $"{_path}MainMenuSC"; // 主選單場景
    public readonly static string GamePlaySC = $"{_path}GamePlaySC"; // 遊戲場景
}

public static class UIPath
{
    private const string _path = "CoreFrame/UI/";

    // 介面
    public readonly static string MainMenuUI = $"{_path}MainMenuUI";     // 主選單介面
    public readonly static string GetReadyUI = $"{_path}GetReadyUI";     // 準備介面
    public readonly static string ScoreUI = $"{_path}ScoreUI";           // 分數介面
    public readonly static string SettlementUI = $"{_path}SettlementUI"; // 結算介面
}

public static class AudioPath
{
    private const string _path = "MediaFrame/Audio/";

    // 背景音樂
    public readonly static string MenuBgm = $"{_path}MenuBgm";           // 主選單背景音樂

    // 音效
    public readonly static string ScoreSfx = $"{_path}ScoreSfx";         // 分數音效
    public readonly static string HitSfx = $"{_path}HitSfx";             // 撞擊音效
    public readonly static string FlySfx = $"{_path}FlySfx";             // 飛起音效
    public readonly static string SwooshingSfx = $"{_path}SwooshingSfx"; // 轉場音效
}