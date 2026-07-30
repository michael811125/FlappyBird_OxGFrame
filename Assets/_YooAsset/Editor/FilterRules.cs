using System.IO;
using YooAsset.Editor;

[DisplayName("收集 MP4 文件")]
public class CollectMP4 : IAssetFilterRule
{
    public string FindAssetType => EAssetFilterType.VideoClip.ToString();

    public bool IsCollectAsset(AssetFilterRuleData data)
    {
        return Path.GetExtension(data.AssetPath) == ".mp4";
    }
}
