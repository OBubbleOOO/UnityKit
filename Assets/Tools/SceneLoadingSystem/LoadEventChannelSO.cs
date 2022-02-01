using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using OBubbleKit.ValueSystem;

namespace OBubbleKit.SceneLoadingSystem
{
	/// <summary>
	/// This class is a used for scene loading events.
	/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
	/// </summary>
	[CreateAssetMenu(menuName = "SO/Events/Load Event Channel")]
	public class LoadEventChannelSO : EventChannelBaseSO
	{
		public UnityAction<GameSceneSO, IEnumerator[], string> OnLoadingRequested;

		public void RaiseEvent(GameSceneSO sceneToLoad,IEnumerator[] tasks, string transitionID)
		{
			if (OnLoadingRequested != null)
			{
				OnLoadingRequested.Invoke(sceneToLoad, tasks, transitionID);
			}
			else
			{
				Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
					"Check why there is no SceneLoader already present, " +
					"and make sure it's listening on this Load Event channel.");
			}
		}
	}

}