using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SceneData
{
    [XmlAttribute("id")]
    public string id;

    [XmlElement("Line")]
    public List<string> line;
}
