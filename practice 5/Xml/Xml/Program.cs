using System;
using System.IO;
using System.Xml.Serialization;
namespace Xml
{
    [XmlRoot("Configuration")]
    public class Configuration
    {
        public bool skipIntro;
        public string filePath;
        public Adjustment adjustments;

        public class Adjustment
        {
            public float brightness;
            public float contrast;
        }
        public override string ToString()
        {
            return $"Configuration: {filePath}, {skipIntro}, {adjustments.brightness}, {adjustments.contrast}";

        }
    }
    [XmlRootAttribute("skipIntro")]
    public class configuration
    {
        public string filePath;
        public bool skipIntro;
        public Adjustment adjustments;

        public class Adjustment
        {
            [XmlAttribute()]
            public float brightness;
            [XmlAttribute()]
            public float contrast;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Configuration));
            StreamReader reader = new StreamReader(@"C:\Users\Myhasik\projects\practicalExercises\practice 5\xml1.xml");
            Configuration configuration = (Configuration)ser.Deserialize(reader);
            Console.WriteLine(configuration.ToString());
            reader = new StreamReader(@"C:\Users\Myhasik\projects\practicalExercises\practice 5\xml2.xml");
            configuration configuration1 = (configuration)ser.Deserialize(reader);
            Console.WriteLine(configuration1.ToString());


        }
    }
}
