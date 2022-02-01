using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace OBubbleKit.SceneLoadingSystem
{
    /// <summary>
    /// 转场控制器，接收SceneLoader发送的转场信号并控制动画播放
    /// 作为控制器基类，提供一个简单实现
    /// </summary>
    public class TransitionController : MonoBehaviour
    {
        /// <summary>
        /// 该控制器的唯一标识符，需要转场的组件通过该标识符引用该控制器
        /// </summary>
        public string ID;
        [SerializeField] protected Animator _animator;
        //淡出/淡入时间
        [SerializeField] protected float FadeoutTime;
        [SerializeField] protected float FadeinTime;
        //Anim的淡出/淡入触发器名称
        [SerializeField] protected string _fadeoutTrigger;
        [SerializeField] protected string _fadeinTrigger;

        /// <summary>场景淡出</summary>
        public IEnumerator Fadeout()
        {
            _animator?.SetTrigger(_fadeoutTrigger);
            yield return new WaitForSeconds(FadeoutTime);
        }

        public IEnumerator Fadein()
        {
            _animator?.SetTrigger(_fadeinTrigger);
            yield return new WaitForSeconds(FadeinTime);
        }
    }
}
