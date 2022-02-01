using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
	[CreateAssetMenu(menuName = "SO/Events/Standard/IEnumerator Event Channel")]
	public class IEnumeratorEventChannelSO : EventChannelBaseSO
	{
		public UnityAction<IEnumerator> OnEventRaised;

		public void RaiseEvent(IEnumerator enumerator)
		{
			OnEventRaised?.Invoke(enumerator);
		}
	}
}