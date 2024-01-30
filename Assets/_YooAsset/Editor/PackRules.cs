using YooAsset.Editor;

/// <summary>
/// 打包 MP4 原生文件
/// </summary>
[DisplayName("打包 MP4 原生文件")]
public class PackMP4File : IPackRule
{
    PackRuleResult IPackRule.GetPackRuleResult(PackRuleData data)
    {
        string bundleName = data.AssetPath;
        PackRuleResult result = new PackRuleResult(bundleName, "mp4");
        return result;
    }
}

