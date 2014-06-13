using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class StevenService : IStevenService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }


        public string GetXml()
        {
            string strOutput;
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"c:\users\steven.gong\documents\visual studio 2012\Projects\WcfServiceLibraryE\WcfServiceLibraryE\Employees2.xml");
            //strOutput = xmlDoc.InnerXml;

            XDocument xDoc = getXmlDoc();
            strOutput = xDoc.ToString();

            return strOutput;
        }

        public string getAge(string firstName)
        {
            XDocument xDoc = getXmlDoc();
            var age = xDoc.Root.Elements("Employee").Where(e => e.Elements("Name").Elements("FirstName").FirstOrDefault().Value.Equals(firstName)).Select(e=>e.Elements("Age").FirstOrDefault()).FirstOrDefault().Value;
            return age.ToString();
        }



        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void SetData(Dictionary<string, string> dicEmp)
        {

            XDocument xDoc = getXmlDoc();

            foreach (KeyValuePair<string, string> pair in dicEmp)
            {
                xDoc.Root.Elements("Employee").Elements("Name").Where(e => e.Element("FirstName").Value.Equals(pair.Key)).Select(e => e.Element("LastName")).Single().SetValue(pair.Value);
            }

            xDoc.Save(getRelativePath());
        }

        XDocument getXmlDoc()
        {
            return XDocument.Load(getRelativePath());
        }
        string getRelativePath()
        {
            StringBuilder relaPath = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory);
            relaPath.Replace("bin\\Debug", string.Empty);
            return relaPath + "Employees2.xml";
        }
    }
}
