using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;


namespace SkatanicStudios.Localisation
{
    public class XMLLoader
    {
        private XDocument _xmlDocument;
        private IEnumerable<XElement> _data;

        public void LoadXML(string path)
        {

            _xmlDocument = XDocument.Load(path);


            _data = _xmlDocument.Descendants("data").Elements();
        }

        public Dictionary<string, string> GetLanguageValues(string attributeId) {

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (XElement element in _data)
            {
                if (element.Element(attributeId) == null)
                {
                    Debug.Log(element.Value);
                    //Adds a "NO TRANSLATION" to dictionary
                    string key = element.Element("key").Value;
                    dictionary.Add(element.Element("key").Value, string.Format("{0}:{1}", element.Element("key").Value, attributeId));
                }
                else
                {
                    if (dictionary.ContainsKey(element.Element("key").Value)) { continue; }
                    dictionary.Add(element.Element("key").Value, element.Element(attributeId).Value);
                }

            }


            return dictionary;


        }
    
  
    }
}
