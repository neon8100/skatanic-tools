using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace SkatanicStudios.Serialization.Surrogates
{
    public class KeyframeSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Keyframe keyframe = (Keyframe)obj;
            info.AddValue("inTangent", keyframe.inTangent);
            info.AddValue("outTangent", keyframe.outTangent);
            info.AddValue("tangentMode", keyframe.tangentMode);
            info.AddValue("time", keyframe.time);
            info.AddValue("value", keyframe.value);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Keyframe keyframe = (Keyframe)obj;
            keyframe.inTangent = (float)info.GetValue("inTangent", typeof(float));
            keyframe.outTangent = (float)info.GetValue("outTangent", typeof(float));
            keyframe.tangentMode = (int)info.GetValue("tangentMode", typeof(int));
            keyframe.time = (float)info.GetValue("time", typeof(float));
            keyframe.value = (float)info.GetValue("value", typeof(float));


            obj = keyframe;
            return obj;
        }
    }
}

