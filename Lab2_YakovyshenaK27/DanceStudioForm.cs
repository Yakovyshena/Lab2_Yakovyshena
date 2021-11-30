using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.IO;


namespace Lab2_YakovyshenaK27
{
    public partial class DanceStudioForm : Form
    {
        public DanceStudioForm()
        {
            InitializeComponent();
        }

        private void DanceStudioForm_Load(object sender, EventArgs e)
        {
            GetInfoDanceStudio();
        }

        public void GetInfoDanceStudio()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xml");

            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("Dance");

            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode n = childNodes.Item(i);
                addItems(n);
            }
        }

        private void addItems(XmlNode n)
        {
            if (!StyleComboBox.Items.Contains(n.SelectSingleNode("Style").Value))
                StyleComboBox.Items.Add(n.SelectSingleNode("Style").Value);

            if (!LevelComboBox.Items.Contains(n.SelectSingleNode("Level").Value))
                LevelComboBox.Items.Add(n.SelectSingleNode("Level").Value);

            if (!DayComboBox.Items.Contains(n.SelectSingleNode("Day").Value))
                DayComboBox.Items.Add(n.SelectSingleNode("Day").Value);

            if (!TimeComboBox.Items.Contains(n.SelectSingleNode("Time").Value))
                TimeComboBox.Items.Add(n.SelectSingleNode("Time").Value);

            if (!TeacherComboBox.Items.Contains(n.SelectSingleNode("Teacher").Value))
                TeacherComboBox.Items.Add(n.SelectSingleNode("Teacher").Value);

            if (!HallComboBox.Items.Contains(n.SelectSingleNode("Hall").Value))
                HallComboBox.Items.Add(n.SelectSingleNode("Hall").Value);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            search();
        }
        public void search()
        {
            InfoRichTextBox.Text = "";
            DanceStudio dance = new DanceStudio();

            if (StyleCheckBox.Checked)
                dance.Style = StyleComboBox.SelectedItem.ToString();

            if (LevelCheckBox.Checked)
                dance.Level = LevelComboBox.SelectedItem.ToString();

            if (DayCheckBox.Checked)
                dance.Day = DayComboBox.SelectedItem.ToString();

            if (TimeCheckBox.Checked)
                dance.Time = TimeComboBox.SelectedItem.ToString();

            if (TeacherCheckBox.Checked)
                dance.Teacher = TeacherComboBox.SelectedItem.ToString();

            if (HallCheckBox.Checked)
                dance.Hall = HallComboBox.SelectedItem.ToString();



            IAnalizatorXMLStrategy analizator = new AnalizatorXMLDOMStrategy();

            if (DOMRadioButton.Checked)
                analizator = new AnalizatorXMLDOMStrategy();

            if (SAXRadioButton.Checked)
                analizator = new AnalizatorXMLSAXStrategy();

            if (LINQtoXMLRadioButton.Checked)
                analizator = new AnalizatorXMLLINQStrategy();


            //Search search = new Search(analizator, dance);
            //List<DanceStudio> results = search.SearchAlgorithm(analizator);
            var results = analizator.Search(dance);



            foreach (DanceStudio danc in results)
            {
                InfoRichTextBox.Text += "Style: " + danc.Style + "\n";
                InfoRichTextBox.Text += "Level: " + danc.Level + "\n";
                InfoRichTextBox.Text += "Day: " + danc.Day + "\n";
                InfoRichTextBox.Text += "Time: " + danc.Time + "\n";
                InfoRichTextBox.Text += "Teacher: " + danc.Teacher + "\n";
                InfoRichTextBox.Text += "Hall: " + danc.Hall + "\n";

                InfoRichTextBox.Text += "\n\n\n";
            }

        }


        private void TransformationButton_Click(object sender, EventArgs e)
        {
            transform();
            MessageBox.Show("Success!");
        }

        private void transform()
        {
            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(@"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xslt");
            string fXML = @"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.html";
            string fHTML = @"C:\Users\test\source\repos\Lab2_YakovyshenaK27\DanceStudio.xml";
            xsl.Transform(fXML, fHTML);
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            InfoRichTextBox.Text = "";
        }
    }
}
    

