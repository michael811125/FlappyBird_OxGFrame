using YooAsset.Editor;

/// <summary>
/// 打包 4ds 原生文件
/// </summary>
[DisplayName("打包 4ds 原生文件")]
public class Pack4DSFile : IPackRule
{
    PackRuleResult IPackRule.GetPackRuleResult(PackRuleData data)
    {
        string bundleName = data.AssetPath;
        PackRuleResult result = new PackRuleResult(bundleName, "4ds");
        return result;
    }
}

