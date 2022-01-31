using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/String Value")]
    public class StringValueSO : ValueSOBase<string>
    {

        [SerializeField] private StringEvent _onValueChanged;
        public override UnityEvent<string> OnValueChanged => _onValueChanged;
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {

    }
}