using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/Double Value")]
    public class DoubleValueSO : ValueSOBase<double>
    {
        [SerializeField] private DoubleEvent _onValueChanged;
        public override UnityEvent<double> OnValueChanged => _onValueChanged;
    }


    [System.Serializable]
    public class DoubleEvent : UnityEvent<double>
    {

    }
}