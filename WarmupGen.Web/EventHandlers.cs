using Microsoft.AspNetCore.Components;

namespace WarmupGen.Web
{
	[EventHandler("oncustomtransitionend", typeof(CustomTransitionEndEventArgs), enableStopPropagation: true, enablePreventDefault: false)]
	public static class EventHandlers
	{
	}

	public class CustomTransitionEndEventArgs : EventArgs
    {
        public string? PropertyName { get; set; }
    }
}
