using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.SceneLoadingSystem
{
	/// <summary>
	/// 游戏场景的通用数据
	/// </summary>
	
	[CreateAssetMenu(fileName = "New Scene", menuName = "SO/New Scene")]
	public partial class GameSceneSO : ScriptableObject
	{
		[Header("Information")]
#if UNITY_EDITOR // See GameSceneSOEditor.cs
		public UnityEditor.SceneAsset sceneAsset;
#endif
		[HideInInspector]
		public string scenePath;
		public string SceneName;
		[TextArea] public string shortDescription;

		//当场景栈调用新的场景/返回至原场景时调用
		public UnityEvent OnScenePause, OnSceneResume;
		//依次为场景退出（关门开始），场景卸载（关门结束），场景资源与数据加载完毕（开门开始），场景开始运行（开门结束）
		//当调用CallScene时会触发OnSceneQuit但不会触发OnSceneUnload
		public UnityEvent OnFadeoutStart, OnFadeoutEnd, OnFadeinStart, OnFadeinEnd;
	}

}