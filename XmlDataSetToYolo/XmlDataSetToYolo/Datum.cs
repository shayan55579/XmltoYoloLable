
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Drawing;

namespace Xml2CSharp
{
    [XmlRoot(ElementName = "bndbox")]
    public class BoundingBox
    {
        [XmlElement(ElementName = "xmin")]
        public float Xmin { get; set; }
        [XmlElement(ElementName = "ymin")]
        public float Ymin { get; set; }
        [XmlElement(ElementName = "xmax")]
        public float Xmax { get; set; }
        [XmlElement(ElementName = "ymax")]
        public float Ymax { get; set; }
        
        public float X
        {
            get
            {
                return Xmin;
            }
        }
        public float Y
        {
            get
            {
                return Ymin;
            }
        }
        public float Width
        {
            get
            {
                // compute width from xmin and xmax
                return Xmax - Xmin;
            }
        }
        public float Height
        {
            get
            {
                return Ymax - Ymin;
            }
        }

        public RectangleF Rect
        {
            get
            {
                return new RectangleF(Xmin, Ymin, Width, Height);
            }
        }
    }

    [XmlRoot(ElementName = "object")]
    public class Object
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "bndbox")]
        public BoundingBox BoundingBox { get; set; }
    }

    [XmlRoot(ElementName = "annotation")]
    public class Annotation
    {
        [XmlElement(ElementName = "filename")]
        public List<string> Filename { get; set; }
        [XmlElement(ElementName = "object")]
        public List<Object> Object { get; set; }
        [XmlElement(ElementName = "folder")]
        public string Folder { get; set; }
    }

}
