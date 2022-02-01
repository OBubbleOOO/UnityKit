using OBubbleKit.ValueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace OBubbleKit.SceneLoadingSystem
{
	/// <summary>
	/// 管理场景流程
	/// 包括转场，后台加载等
	/// TODO: CallScene
	/// </summary>
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private LoadEventChannelSO _jumpToScene = default;
		[SerializeField] private IEnumeratorEventChannelSO _addInitialTask = default;

		[Header("References")]
		[SerializeField] private BoolValueSO _isLoading = default;
		//用于在场景加载时屏蔽低层鼠标输入
		[SerializeField] private RawImage _sceneLoadingMask;
		[SerializeField] private TransitionController[] _transitions;

		private AsyncOperation _sceneAsyncOperation = default;
		//新场景初始化时需要执行的耗时操作（如加载谱面）在场景加载的第一帧(Start时)通过AddInitialTask置于此处
		private List<IEnumerator> _sceneInitialTasks = new List<IEnumerator>();
		private Dictionary<string, TransitionController> _transitionDic;

		public float TransitionStayTime;
		public GameSceneSO CurrentScene { get; private set; }

		private void Awake()
		{
			_isLoading.value = false;
			_transitionDic = new Dictionary<string, TransitionController>();
			for (int i = 0; i < _transitions.Length; i++)
			{
				if(!_transitionDic.ContainsKey(_transitions[i].ID))
				{
					_transitionDic.Add(_transitions[i].ID, _transitions[i]);
				}
			}
		}

		private void OnEnable()
		{
			if (_jumpToScene != null)
			{
				_jumpToScene.OnLoadingRequested += JumpToScene;
			}
			if (_addInitialTask != null)
			{
				_addInitialTask.OnEventRaised += AddInitialTask;
			}
		}

		private void OnDisable()
		{
			if (_jumpToScene != null)
			{
				_jumpToScene.OnLoadingRequested -= JumpToScene;
			}
			if (_addInitialTask != null)
			{
				_addInitialTask.OnEventRaised -= AddInitialTask;
			}
		}

		/// <summary>
		/// 场景跳转，清空当前场景栈并将其全部卸载，随后加载目标场景
		/// </summary>
		/// <param name="sceneToLoad">要跳转的目标场景</param>
		/// <param name="tasks">要处理的任务队列，一般为目标场景需要的数据</param>
		/// <param name="transitionID">转场控制器ID</param>
		private void JumpToScene(GameSceneSO sceneToLoad, IEnumerator[] tasks, string transitionID)
		{
			if (_isLoading.value)
			{
				Debug.LogWarning("不能在转场时发送场景跳转指令");
				return;
			}
			if(_transitionDic.ContainsKey(transitionID))
			{
				StartCoroutine(JumpToSceneCoroutine(sceneToLoad, tasks, _transitionDic[transitionID]));
			}
			else
			{
				StartCoroutine(JumpToSceneCoroutine(sceneToLoad, tasks, null));
			}
		}
		/// <summary>
		/// 场景跳转流程
		/// 即场景淡出->异步卸载/加载场景->加载资源（执行tasks）->等待场景加载结束->执行场景初始化操作->场景淡入
		/// 其中夹杂着对场景事件的调用
		/// </summary>
		private IEnumerator JumpToSceneCoroutine(GameSceneSO sceneToLoad, IEnumerator[] tasks, TransitionController transition)
		{
			_isLoading.value = true;
			_sceneLoadingMask.enabled = true;
			//场景淡出
			CurrentScene?.OnFadeoutStart?.Invoke();
			yield return StartCoroutine(transition.Fadeout());
            if (CurrentScene != null)
			{
				CurrentScene.OnFadeoutEnd?.Invoke();
			}
			CurrentScene = null;
			//加载新场景
			_sceneInitialTasks.Clear();
			_sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.SceneName);
			while (!_sceneAsyncOperation.isDone)
			{
				yield return null;
			}
			yield return StartCoroutine(WaitForLoading(tasks, transition));
			//这里需要停顿一帧，等待新场景订阅OnSceneReady事件及发送初始化任务队列
			yield return null;
			for (int i = 0; i < _sceneInitialTasks.Count; i++)
			{
				yield return _sceneInitialTasks[i];
			}
			_sceneInitialTasks.Clear();
			CurrentScene = sceneToLoad;
			//场景淡入
			sceneToLoad.OnFadeinStart?.Invoke();
			yield return StartCoroutine(transition.Fadein());
			sceneToLoad.OnFadeinEnd?.Invoke();
			_sceneLoadingMask.enabled = false;
			_isLoading.value = false;
		}

		/// <summary>异步处理所有任务与场景加载并等待其结束</summary>
		private IEnumerator WaitForLoading(IEnumerator[] tasks, TransitionController transition)
		{
			if (transition != null)
			{
				yield return new WaitForSeconds(TransitionStayTime);
			}
			if (tasks != null)
			{
				Coroutine[] coroutineList = new Coroutine[tasks.Length];
				for (int i = 0; i < tasks.Length; i++)
				{
					coroutineList[i] = StartCoroutine(tasks[i]);
				}
				for (int i = 0; i < tasks.Length; i++)
				{
					yield return coroutineList[i];
				}
			}
			while (!_sceneAsyncOperation.isDone)
			{
				yield return null;
			}
		}

		private void AddInitialTask(IEnumerator task)
		{
			_sceneInitialTasks.Add(task);
		}
	}
}
