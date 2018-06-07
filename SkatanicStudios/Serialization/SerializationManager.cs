using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using SkatanicStudios.Serialization.Surrogates;

namespace SkatanicStudios.Serialization
{
    public class SerializationManager
    {
        //Set this in your project to give a custom extension to the save file.
        public static string fileExtension = ".save";

        /// <summary>
        /// Stores a serializable class 
        /// </summary>
        /// <param name="savename"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public static bool Save(string savename, object saveData)
        {

            BinaryFormatter binary = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves/" + savename + fileExtension;

            FileStream file = File.Create(path);

            binary.Serialize(file, saveData);

            file.Close();

            return true;

        }
        /// <summary>
        /// Returns a list of all the 
        /// </summary>
        /// <returns></returns>
        public static string[] GetSaves()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
            }

            return Directory.GetFiles(Application.persistentDataPath + "/saves/");
        }

        public static object Load(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            BinaryFormatter binary = GetBinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);
            object save = binary.Deserialize(file);
            file.Close();

            return save;
        }

        //This allows us to format non-serializable unity classes by converting them to a serializable class. 
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

            binary.SurrogateSelector = ss;

            return binary;
        }
    }
}
