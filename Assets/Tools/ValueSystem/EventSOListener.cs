using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    /// <summary>
    /// 由于SO无法直接引用场景中的脚本
    /// 所以使用该脚本来注册SO事件与场景脚本的事件监听关系
    /// </summary>
    public class EventSOListener : MonoBehaviour
    {
        [TextArea]
        public string description;

        //若要注册多个事件或者其他类型的SO事件，需要创建一个新的VoidEventChannelSO并令该SO注册需要监听的事件
        public VoidEventChannelSO ListenTo;
        public UnityEvent DispatchTo;

        private void OnEnable()
        {
            ListenTo?.OnEventRaised.AddListener(DispatchTo.Invoke);
        }
        private void OnDisable()
        {
            ListenTo?.OnEventRaised.RemoveListener(DispatchTo.Invoke);
        }

        public void Dispatch()
        {
            DispatchTo.Invoke();
        }
    }
}