using OxGFrame.CenterFrame;
using OxGFrame.CenterFrame.EventCenter;

/// <summary>
/// UI event center. Registers every UI event so callers raise an event instead of
/// depending on a concrete UI class.
/// <para>
/// UI 事件中心。註冊所有 UI 事件, 讓呼叫端只需送出事件,
/// 而不必相依於具體的 UI 類別。
/// </para>
/// </summary>
public class UIEventCenter : CenterBase<UIEventCenter, EventBase>
{
    public UIEventCenter()
    {
        this.Register<EDescriptionUI>();
        this.Register<EDoubleCheckUI>();
    }
}
