using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SkatanicStudios.Localisation
{

    public class CSVLoader
    {
        private TextAsset _csvFile; //reference file;

        private char _lineSeperator = '\n'; // It defines line seperate character
        private char _surround = '"';
        private string[] _fieldSeperator = { "\",\"" } ; // It defines field seperate chracter

        public void LoadCSV()
        {
            TextLocalisationAsset asset = (TextLocalisationAsset) Resources.Load("localisation");
            _csvFile = asset.textAsset;
        }

        public Dictionary<string, string> GetLanguageValues(string attributeId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();


            string[] lines = _csvFile.text.Split(_lineSeperator);


            int attributeIndex = -1;

            //Get the headers so it knows which dictionary to creeate
            string[] headers = lines[0].Split(_fieldSeperator, StringSplitOptions.None);

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeId))
                {
                    attributeIndex = i;
                    break;
                }
            }

            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");


            for (int i=1; i<lines.Length; i++)  
            {
                string line = lines[i];

                string[] fields = CSVParser.Split(line);

                for (int f = 0; f < fields.Length; f++)
                {
                    fields[f] = fields[f].TrimStart(' ', '"');
                    fields[f] = fields[f].TrimEnd('"');
                }
                
                if (fields.Length > attributeIndex)
                {
                    var key = fields[0];

                    if (dictionary.ContainsKey(key)) { continue; }

                    var value = fields[attributeIndex];
                   
                    dictionary.Add(key, value);
                }
            }

            return dictionary;
        }


        public void AppendCSV(string value, string key)
        {

            string appended = string.Format("\n\"{0}\",\"{1}\",\"\",\"\",\"\",\"\"", key,  value);
            File.AppendAllText(TextLocalisation.AssetPath, appended);

            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
                Debug.Log("Added " + key);
            #endif
        }

        public void Remove(string key)
        {
            //Split the text into lines
            string[] lines = _csvFile.text.Split(_lineSeperator);

            //split the lines into keys
            string[] keys = new string[lines.Length];

            //go through each line
            for (int i=0; i<lines.Length; i++)
            {
                string line = lines[i];

                //Assign the key of each line
                keys[i] = line.Split(_fieldSeperator, StringSplitOptions.None)[0];
            }

            int index = -1;
            for (int i=0; i<keys.Length; i++)
            {
                if (keys[i].Contains(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
            {

                string[] newLines;
                newLines = lines.Where(w => w != lines[index]).ToArray();

                string replaced = string.Join(_lineSeperator.ToString(), newLines);
                Debug.Log("Removed " + key);
                File.WriteAllText(TextLocalisation.AssetPath, replaced);
            }

        }

        public void EditCSV(string value, string key)
        {
            Remove(key);
            AppendCSV(value, key);
        }

    }
}