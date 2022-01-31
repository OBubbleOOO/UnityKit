using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/Bool Value")]
    public class BoolValueSO : ValueSOBase<bool>
    {
        [SerializeField] private BoolEvent _onValueChanged;
        public override UnityEvent<bool> OnValueChanged => _onValueChanged;
        public void SetValue(bool newValue)
        {
            value = newValue;
        }
    }

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }
}