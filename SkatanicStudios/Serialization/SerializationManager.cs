using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SkatanicStudios.Serialization
{
    public class SerializationManager
    {

        public static bool Save(string savename, object saveData)
        {

            BinaryFormatter binary = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves/" + savename + ".rpgtr";

            FileStream file = File.Create(path);

            binary.Serialize(file, saveData);

            file.Close();

            return true;

        }

        public static string[] GetSaves()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
            }

            return Directory.GetFiles(Application.persistentDataPath + "/saves/");
        }

        public static SaveGame Load(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            BinaryFormatter binary = GetBinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);
            SaveGame save = (SaveGame)binary.Deserialize(file);
            file.Close();

            return save;
        }

        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter binary = new BinaryFormatter();
            // 2. Construct a SurrogateSelector object
            SurrogateSelector ss = new SurrogateSelector();


            Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
            ss.AddSurrogate(typeof(Vector3),
                            new StreamingContext(StreamingContextStates.All),
                            v3ss);

            KeyframeSerializationSurrogate kss = new KeyframeSerializationSurrogate();
            ss.AddSurrogate(typeof(Keyframe), new StreamingContext(StreamingContextStates.All), kss);

            AnimationCurveSerializationSurrogate acss = new AnimationCurveSerializationSurrogate();
            ss.AddSurrogate(typeof(AnimationCurve), new StreamingContext(StreamingContextStates.All), acss);


            // 5. Have the formatter use our surrogate selector
            binary.SurrogateSelector = ss;

            return binary;
        }
    }
}
