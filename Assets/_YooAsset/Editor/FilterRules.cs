using System.IO;
using YooAsset.Editor;

[DisplayName("收集 4DViews 檔案")]
public class Collect4DS : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        return Path.GetExtension(data.AssetPath) == ".4ds";
    }
}
