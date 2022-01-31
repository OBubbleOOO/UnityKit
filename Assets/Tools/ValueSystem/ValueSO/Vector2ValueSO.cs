using UnityEngine;
using UnityEngine.Events;

namespace OBubbleKit.ValueSystem
{
    [CreateAssetMenu(menuName = "SO/Values/Vector2 Value")]
    public class Vector2ValueSO : ValueSOBase<Vector2>
    {

        [SerializeField] private Vector2Event _onValueChanged;
        public override UnityEvent<Vector2> OnValueChanged => _onValueChanged;
    }

    [System.Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {

    }
}