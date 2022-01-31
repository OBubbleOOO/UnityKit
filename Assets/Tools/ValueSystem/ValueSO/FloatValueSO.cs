using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/Float Value")]
    public class FloatValueSO : ValueSOBase<float>
    {
        [SerializeField] private FloatEvent _onValueChanged;
        public override UnityEvent<float> OnValueChanged => _onValueChanged;
    }

    [System.Serializable]
    public class FloatEvent : UnityEvent<float>
    {

    }
}