using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    public class FloatSyncer : ValueSyncerBase<float>
    {
        [SerializeField] private FloatValueSO _valueSO;
        [SerializeField] private FloatEvent _onValueChanged;
        public override UnityEvent<float> OnValueChanged => _onValueChanged;
        public override ValueSOBase<float> ValueSO => _valueSO;
    }
}
