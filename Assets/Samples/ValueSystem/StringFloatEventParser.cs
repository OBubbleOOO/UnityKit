using OBubbleKit.ValueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OBubbleKit.Samples
{
    /// <summary>
    /// 用于转换String事件与Float事件
    /// </summary>
    public class StringFloatEventParser : MonoBehaviour
    {
        public StringEvent StringEvent;
        public FloatEvent FloatEvent;

        public void OnStringChanged(string value)
        {
            float f;
            if(float.TryParse(value, out f))
            {
                FloatEvent.Invoke(f);
            }
        }
        public void OnFloatChanged(float value)
        {
            StringEvent.Invoke(value.ToString());
        }
    }
}
