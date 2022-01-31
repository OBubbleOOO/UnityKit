using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    /// <summary>
    /// 保存一个简单数值对象以及它的变化事件
    /// 也可以直接作为事件通道使用
    /// 实现该类时，需要同步创建一个可序列化的UnityEvent类，可参考其他具象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueSOBase<T> : ScriptableObject
    {
        [TextArea] public string description;
        [SerializeField] private T _value;

        /// <summary>
        /// 作为事件通道使用时，给value赋值以调用事件
        /// </summary>
        public virtual T value
        {
            get => _value;
            set
            {
                _value = value;
                RaiseEvent();
            }
        }

        [ContextMenu("RaiseEvent")]
        public void RaiseEvent()
        {
            OnValueChanged.Invoke(_value);
        }

        public abstract UnityEvent<T> OnValueChanged { get; }
    }
}