using OxGFrame.CenterFrame;
using OxGFrame.CenterFrame.EventCenter;

namespace FlappyBird.Hotfix.Runtime
{
    public class UIEventCenter : CenterBase<UIEventCenter, EventBase>
    {
        public UIEventCenter()
        {
            this.Register<EDescriptionUI>();
            this.Register<EDoubleCheckUI>();
        }
    }
}