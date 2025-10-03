using System.IO;
using YooAsset.Editor;

[DisplayName("收集 MP4 文件")]
public class CollectMP4 : IFilterRule
{
    public string FindAssetType => "t:VideoClip";

    public bool IsCollectAsset(FilterRuleData data)
    {
        return Path.GetExtension(data.AssetPath) == ".mp4";
    }
}
