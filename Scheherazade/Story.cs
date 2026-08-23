using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Story")]
public class Story
{
    [XmlElement("Scene")]
    public List<SceneData> scenes;
}
