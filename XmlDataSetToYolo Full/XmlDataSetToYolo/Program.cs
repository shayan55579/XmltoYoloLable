using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Serialization;

using Xml2CSharp;

var labelIndex = new Dictionary<string, string>();

if (args[1] == "true")
{
    foreach (var file in Directory.EnumerateFiles(args[0]).Where(a => a.EndsWith(".xml")))
    {
        var serializer = new XmlSerializer(typeof(Annotation));
        using var xmlReader = XmlReader.Create(file);
        var annotaion = (Annotation)serializer.Deserialize(xmlReader);

        var elements = annotaion.Object;

        foreach (var item in elements)
        {
            if (item.Name == "کل ناحیه پلاک")
            { }
            else
                labelIndex.TryAdd(item.Name, item.Name);

        }
    }

    File.WriteAllLines("labels.txt", labelIndex.OrderBy(a => a.Key).Select(a => a.Key).ToArray());

}

var lines = File.ReadAllLines("labels.txt");
labelIndex.Clear();
for (int i = 0; i < lines.Length; i++)
{
    labelIndex.Add(lines[i], i.ToString());
}

var folderAddress = Path.Combine(args[0], "Plates");
Directory.CreateDirectory(folderAddress);
var counter = 0;
foreach (var file in Directory.EnumerateFiles(args[0]).Where(a => a.EndsWith(".xml")))
{
    Console.WriteLine(file);

    var serializer = new XmlSerializer(typeof(Annotation));
    using var xmlReader = XmlReader.Create(file);
    var annotaion = (Annotation)serializer.Deserialize(xmlReader);

    var elements = annotaion.Object;
    var imageFileName = file.Replace(".xml", ".jpg");
    
    
    var image = (Bitmap)Image.FromFile(imageFileName);
    
    foreach (Xml2CSharp.Object v in elements)
    {
        try
        {
            var imageData = new List<string>();
            //var plateParts = elements.Skip(i * 9).Take(9);
            //var plate = plateParts.First();
            //var plateRectangle = new RectangleF(plate.BoundingBox.Xmin, plate.BoundingBox.Ymin, plate.BoundingBox.Xmax - plate.BoundingBox.Xmin, plate.BoundingBox.Ymax - plate.BoundingBox.Ymin);
            //var plateLetters = plateParts.Skip(1).Select(a => new
            //{
            //    a.Name,
            //    Rect = new RectangleF(a.BoundingBox.Xmin - plate.BoundingBox.Xmin,
            //                        a.BoundingBox.Ymin - plate.BoundingBox.Ymin,
            //                        a.BoundingBox.Xmax - a.BoundingBox.Xmin,
            //                        a.BoundingBox.Ymax - a.BoundingBox.Ymin)
            //});

            //var normalizedLetters = plateLetters.Select(a => new
            //{
            //    a.Name,
            //    Rect = new RectangleF((a.Rect.X + a.Rect.Width / 2) / plateRectangle.Width,
            //                        (a.Rect.Y + a.Rect.Height / 2) / plateRectangle.Height,
            //                        a.Rect.Width / plateRectangle.Width,
            //                        a.Rect.Height / plateRectangle.Height)
            //});

            //imageData.AddRange(normalizedLetters.Select(a => $"{labelIndex[a.Name]} {a.Rect.X} {a.Rect.Y} {a.Rect.Width} {a.Rect.Height}"));

            var letterRectangle = new RectangleF((v.BoundingBox.X + v.BoundingBox.Width / 2) / plateRectangle.Width,
                                    (v.BoundingBox.Y + v.BoundingBox.Height / 2) / plateRectangle.Height,
                                    v.BoundingBox.Width / plateRectangle.Width,
                                    v.BoundingBox.Height / plateRectangle.Height)
            // for croping files
            var plateFile = image.Clone(plateRectangle, image.PixelFormat);
            var folderAddressForLetter = Path.Combine(folderAddress, )
            var fileName = Path.Combine(folderAddress, $"{counter:D6}.txt");
            var croppedFileName = fileName.Replace(".txt", ".png");
            plateFile.Save(croppedFileName, ImageFormat.Png);

            File.WriteAllLines(fileName, imageData);
            counter++;
        }
        catch
        {

        }
        
    }
}
