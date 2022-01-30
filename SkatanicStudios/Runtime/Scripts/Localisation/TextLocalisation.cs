using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkatanicStudios.Localisation
{

    public class TextLocalisation
    {
        public static bool isInit;

        public enum Language
        {
            English,
            French,
            Spanish,
            German,
            Italian
        }

        public static Language language = Language.English;

        public static string GetLocalisedValue(string key)
        {
            if (!isInit) { Init(); }

            string value = key;

            switch (language)
            {
                case Language.English:
                    localisedEN.TryGetValue(key, out value);
                    break;
                case Language.French:
                    localisedFR.TryGetValue(key, out value);
                    break;
                case Language.German:
                    localisedDE.TryGetValue(key, out value);
                    break;
                case Language.Italian:
                    localisedIT.TryGetValue(key, out value);
                    break;
                case Language.Spanish:
                    localisedES.TryGetValue(key, out value);
                    break;
            }

            if (value == null)
            {
                value = key;
            }

            //Parse the value for any needed changes
            if (value != null && value != string.Empty)
            {
                if (Application.isPlaying)
                {
                    if (TextLocalisationManager.instance != null)
                    {
                        value = TextLocalisationManager.Replace(value);
                        value = value.Replace("\\n", "\n");
                    }
                }
            }

            //Set the first letter to uppercase
            if (string.IsNullOrEmpty(value) || value.Length <= 1)
            {
                return value;
            }
            else
            {
                return char.ToUpper(value[0]) + value.Substring(1);
            }


        }



        private static Dictionary<string, string> localisedEN;

        public static Dictionary<string, string> GetDictionaryForEditor()
        {
            if (!isInit) { Init(); }
            return localisedEN;
        }

        private static Dictionary<string, string> localisedFR;
        private static Dictionary<string, string> localisedES;
        private static Dictionary<string, string> localisedDE;
        private static Dictionary<string, string> localisedIT;

        public static string AssetPath = "Assets/LivingTheDeal/Runtime/Data/localisation.csv";

        private static void Init()
        {
            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();

            ReloadDic(csvLoader);

            isInit = true;

        }

        public static void ReplaceValue(string value, string key)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();
            csvLoader.EditCSV(value, key);
            csvLoader.LoadCSV();

            ReloadDic(csvLoader);

        }

        public static void AddValue(string value, string key)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();
            csvLoader.AppendCSV(value, key);
            csvLoader.LoadCSV();

            ReloadDic(csvLoader);

        }

        public static void RemoveKey(string key)
        {
            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();
            csvLoader.Remove(key);
            csvLoader.LoadCSV();

            ReloadDic(csvLoader);
        }

        public static void ReloadDic(CSVLoader csvLoader)
        {
            localisedEN = csvLoader.GetLanguageValues("en");
            localisedFR = csvLoader.GetLanguageValues("fr");
            localisedES = csvLoader.GetLanguageValues("es");
            localisedDE = csvLoader.GetLanguageValues("de");
            localisedIT = csvLoader.GetLanguageValues("it");
        }

        public static void Refresh()
        {
            Init();
        }



    }
}
