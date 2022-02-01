using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
	/// <summary>
	/// This class is used for Events that have no arguments (Example: Exit game event)
	/// </summary>

	[CreateAssetMenu(menuName = "SO/Events/Standard/Void Event Channel")]
	public class VoidEventChannelSO : EventChannelBaseSO
	{
		public UnityEvent OnEventRaised;

		[ContextMenu("RaiseEvent")]
		public void RaiseEvent()
		{
			if (OnEventRaised != null)
            {
				OnEventRaised.Invoke();
			}
		}
	}
}
