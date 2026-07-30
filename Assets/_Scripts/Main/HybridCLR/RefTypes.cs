namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// AOT generic instantiation for HybridCLR. Generic types that only ever appear in
    /// hotfix code must be referenced here, otherwise the AOT compiler strips them and
    /// the hotfix assembly fails at runtime.
    /// <para>
    /// HybridCLR 的 AOT 泛型實例化。僅在熱更代碼中出現的泛型型別必須在此被引用,
    /// 否則 AOT 編譯會將其裁剪, 導致熱更程序集在執行期失敗。
    /// </para>
    /// </summary>
    public class RefTypes
    {
        public RefTypes()
        {

        }
    }
}