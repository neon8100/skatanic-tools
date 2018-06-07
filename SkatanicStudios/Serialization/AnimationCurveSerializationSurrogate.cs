using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace SkatanicStudios.Serialization.Surrogates
{
    public class AnimationCurveSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            AnimationCurve curve = (AnimationCurve)obj;
            info.AddValue("postWrapMode", curve.postWrapMode);
            info.AddValue("preWrapMode", curve.preWrapMode);
            info.AddValue("keys", curve.keys);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            AnimationCurve curve = (AnimationCurve)obj;
            curve.postWrapMode = (WrapMode)info.GetValue("postWrapMode", typeof(WrapMode));
            curve.preWrapMode = (WrapMode)info.GetValue("preWrapMode", typeof(WrapMode));
            curve.keys = (Keyframe[])info.GetValue("keys", typeof(Keyframe[]));

            return obj;
        }
    }
}
