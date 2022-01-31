using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/Int Value")]
    public class IntValueSO : ValueSOBase<int>
    {
        [SerializeField] private IntEvent _onValueChanged;
        public override UnityEvent<int> OnValueChanged => _onValueChanged;
    }

    [System.Serializable]
    public class IntEvent : UnityEvent<int>
    {

    }
}