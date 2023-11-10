using YooAsset.Editor;

public class AddressByFilePathInAssets : IAddressRule
{
    string IAddressRule.GetAssetAddress(AddressRuleData data)
    {
        return data.AssetPath.Replace(@"Assets/", string.Empty);
    }
}
