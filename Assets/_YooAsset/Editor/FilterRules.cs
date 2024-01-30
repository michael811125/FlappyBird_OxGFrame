using System.IO;
using YooAsset.Editor;

[DisplayName("收集 MP4 檔案")]
public class CollectMP4 : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        return Path.GetExtension(data.AssetPath) == ".mp4";
    }
}
