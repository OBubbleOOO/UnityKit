using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    /// <summary>
    /// 用于不同组件间的数据同步
    /// 需要同步的组件绑定至同一个ValueSO，当一个组件数据发生变动时，其他所有组件同步变动
    /// 具有两种同步模式：数据模式与事件模式
    /// </summary>
    public abstract class ValueSyncerBase<T> : MonoBehaviour
    {
        /// <summary>
        /// 是否为数据模式
        /// </summary>
        public bool IsEventMode;
        public abstract ValueSOBase<T> ValueSO { get; }

        //两个bool用于防止循环触发同步事件
        private bool _listen = true;
        private bool _dispatch = true;

        #region 数据模式

        #endregion

        #region 事件模式
        //以下两个成员起对偶作用
        //在需要同步的MonoBehavior内与该脚本内分别绑定事件监听

        /// <summary>
        /// 当ValueSO变化时自动触发该事件
        /// 绑定该事件与想要同步的MonoBehavior变量即可将ValueSO的变化同步至其他脚本
        /// </summary>
        public abstract UnityEvent<T> OnValueChanged { get; }

        /// <summary>
        /// 在Inspector中绑定想要同步的MonoBehavior事件与该方法
        /// 即可将该MonoBehavior事件变化同步至ValueSO
        /// </summary>
        public void ChangeValue(T value)
        {
            if(_dispatch)
            {
                _listen = false;
                ValueSO.value = value;
                _listen = true;
            }
        }
        #endregion

        private void OnEnable()
        {
            _listen = true;
            ValueChanged(ValueSO.value);
            ValueSO.OnValueChanged.AddListener(ValueChanged);
        }

        private void OnDisable()
        {
            ValueSO.OnValueChanged.RemoveListener(ValueChanged);
        }

        private void ValueChanged(T value)
        {
            if(_listen)
            {
                _dispatch = false;
                OnValueChanged.Invoke(value);
                _dispatch = true;
            }
        }
    }
}
