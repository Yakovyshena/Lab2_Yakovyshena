using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Lab2_YakovyshenaK27
{
    interface IAnalizatorXMLStrategy
    {
        List<DanceStudio> Search(DanceStudio dance);
    }

    //DOM
     class AnalizatorXMLDOMStrategy : IAnalizatorXMLStrategy
    {
        public List<DanceStudio> Search(DanceStudio dance)
        {
            List<DanceStudio> result = new List<DanceStudio>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xml");

            XmlNode node = doc.DocumentElement;
            foreach (XmlNode nod in node.ChildNodes)
            {
                string Style = "";
                string Level = "";
                string Day = "";
                string Time = "";
                string Teacher = "";
                string Hall = "";

                foreach (XmlAttribute attribute in nod.Attributes)
                {
                    if (attribute.Name.Equals("Style") && (attribute.Value.Equals(dance.Style) || dance.Style.Equals(String.Empty)))
                        Style = attribute.Value;

                    if (attribute.Name.Equals("Level") && (attribute.Value.Equals(dance.Level) || dance.Level.Equals(String.Empty)))
                        Level = attribute.Value;

                    if (attribute.Name.Equals("Day") && (attribute.Value.Equals(dance.Day) || dance.Day.Equals(String.Empty)))
                        Day = attribute.Value;

                    if (attribute.Name.Equals("Time") && (attribute.Value.Equals(dance.Time) || dance.Time.Equals(String.Empty)))
                        Time = attribute.Value;

                    if (attribute.Name.Equals("Teacher") && (attribute.Value.Equals(dance.Teacher) || dance.Teacher.Equals(String.Empty)))
                        Teacher = attribute.Value;

                    if (attribute.Name.Equals("Hall") && (attribute.Value.Equals(dance.Hall) || dance.Hall.Equals(String.Empty)))
                        Hall = attribute.Value;
                }

                if (Style != "" && Level != "" && Day != "" && Time != "" && Teacher != "" && Hall != "")
                {
                    DanceStudio danceStudio = new DanceStudio();
                    danceStudio.Style = Style;
                    danceStudio.Level = Level;
                    danceStudio.Day = Day;
                    danceStudio.Time = Time;
                    danceStudio.Teacher = Teacher;
                    danceStudio.Hall = Hall;

                    result.Add(danceStudio);
                }
            }
            return result;
        }
    }

    //SAX
     class AnalizatorXMLSAXStrategy : IAnalizatorXMLStrategy
    {
        public List<DanceStudio> Search(DanceStudio dance)
        {
            List<DanceStudio> AllResult = new List<DanceStudio>();
            var xmlReader = new XmlTextReader(@"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xml");

            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        string Style = "";
                        string Level = "";
                        string Day = "";
                        string Time = "";
                        string Teacher = "";
                        string Hall = "";

                        if (xmlReader.Name.Equals("Style") && (xmlReader.Value.Equals(dance.Style) || dance.Style.Equals(String.Empty)))
                        {
                            Style = xmlReader.Value;
                            xmlReader.MoveToNextAttribute();

                            if (xmlReader.Name.Equals("Level") && (xmlReader.Value.Equals(dance.Level) || dance.Level.Equals(String.Empty)))
                            {
                                Level = xmlReader.Value;
                                xmlReader.MoveToNextAttribute();

                                if (xmlReader.Name.Equals("Day") && (xmlReader.Value.Equals(dance.Day) || dance.Day.Equals(String.Empty)))
                                {
                                    Day = xmlReader.Value;
                                    xmlReader.MoveToNextAttribute();

                                    if (xmlReader.Name.Equals("Time") && (xmlReader.Value.Equals(dance.Time) || dance.Time.Equals(String.Empty)))
                                    {
                                        Time = xmlReader.Value;
                                        xmlReader.MoveToNextAttribute();

                                        if (xmlReader.Name.Equals("Teacher") && (xmlReader.Value.Equals(dance.Teacher) || dance.Teacher.Equals(String.Empty)))
                                        {
                                            Teacher = xmlReader.Value;
                                            xmlReader.MoveToNextAttribute();

                                            if (xmlReader.Name.Equals("Hall") && (xmlReader.Value.Equals(dance.Hall) || dance.Hall.Equals(String.Empty)))
                                            {
                                                Hall = xmlReader.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Style != "" && Level != "" && Day != "" && Time != "" && Teacher != "" && Hall != "")
                        {
                            DanceStudio danceStudio = new DanceStudio();
                            danceStudio.Style = Style;
                            danceStudio.Level = Level;
                            danceStudio.Day = Day;
                            danceStudio.Time = Time;
                            danceStudio.Teacher = Teacher;
                            danceStudio.Hall = Hall;

                            AllResult.Add(danceStudio);
                        }
                    }
                }
            }
            xmlReader.Close();
            return AllResult;
        }
    }

    //LINQ to XML
     class AnalizatorXMLLINQStrategy : IAnalizatorXMLStrategy
    {
        public List<DanceStudio> Search(DanceStudio dance)
        {
            List<DanceStudio> allResult = new List<DanceStudio>();
            var doc = XDocument.Load(@"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xml");
            var result = from obj in doc.Descendants("Dance")
                         where
                         (
                         (obj.Attribute("Style").Value.Equals(dance.Style) || dance.Style.Equals(String.Empty)) &&
                         (obj.Attribute("Level").Value.Equals(dance.Level) || dance.Level.Equals(String.Empty)) &&
                         (obj.Attribute("Day").Value.Equals(dance.Day) || dance.Day.Equals(String.Empty)) &&
                         (obj.Attribute("Time").Value.Equals(dance.Time) || dance.Time.Equals(String.Empty)) &&
                         (obj.Attribute("Teacher").Value.Equals(dance.Teacher) || dance.Teacher.Equals(String.Empty)) &&
                         (obj.Attribute("Hall").Value.Equals(dance.Hall) || dance.Hall.Equals(String.Empty))
                         )
                         select new
                         {
                             style = (string)obj.Attribute("Style"),
                             level = (string)obj.Attribute("Lavel"),
                             day = (string)obj.Attribute("Day"),
                             time = (string)obj.Attribute("Time"),
                             teacher = (string)obj.Attribute("Teacher"),
                             hall = (string)obj.Attribute("Hall")
                         };
            foreach (var n in result)
            {
                DanceStudio danceStudio = new DanceStudio();
                danceStudio.Style = n.style;
                danceStudio.Level = n.level;
                danceStudio.Day = n.day;
                danceStudio.Time = n.time;
                danceStudio.Teacher = n.teacher;
                danceStudio.Hall = n.hall;

                allResult.Add(danceStudio);
            }
            return allResult;
        }
    }
}
