using YooAsset.Editor;

/// <summary>
/// 打包 MP4 原生文件
/// </summary>
[DisplayName("打包 MP4 原生文件")]
public class PackMP4File : IBundlePackRule
{
    BundlePackRuleResult IBundlePackRule.GetPackRuleResult(BundlePackRuleData data)
    {
        string bundleName = data.AssetPath;
        BundlePackRuleResult result = new BundlePackRuleResult(bundleName, "mp4");
        return result;
    }
}

