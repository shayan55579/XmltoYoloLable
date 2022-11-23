using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Serialization;

using Xml2CSharp;

var folderAddress = Path.Combine(args[0], "Letters");
Console.WriteLine(folderAddress);
Directory.CreateDirectory(folderAddress);
var counter = 0;

foreach (var file in Directory.EnumerateFiles(args[0], "*.xml"))
{
    Console.WriteLine(file);

    var serializer = new XmlSerializer(typeof(Annotation));
    using var xmlReader = XmlReader.Create(file);
    var annotaion = (Annotation)serializer.Deserialize(xmlReader);

    var elements = annotaion.Object;
    var imageFileName = file.Replace(".xml", ".jpg");
    
    
    var image = (Bitmap)Image.FromFile(imageFileName);
    
    foreach (var item in elements)
    {
        try
        {
            var letterFile = image.Clone(item.BoundingBox.Rect, image.PixelFormat);
            var folderAddressForLetter = Path.Combine(folderAddress, item.Name);
            Directory.CreateDirectory(folderAddressForLetter);
            var fileName = Path.Combine(folderAddressForLetter, $"{counter:D6}.png");
            
            letterFile.Save(fileName, ImageFormat.Png);

            counter++;
        }
        catch
        {

        }
        
    }
}
