using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Serialization;

namespace HarvestApiPoco.Service
{
    internal abstract class BaseAdaptor<T>
    {
        private static readonly XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                                                                          {
                                                                              DtdProcessing = DtdProcessing.Parse,
                                                                              XmlResolver = new XmlPreloadedResolver(),
                                                                              ConformanceLevel =
                                                                                  ConformanceLevel.Document,
                                                                          };


        private readonly string itemNodeName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNodeName">
        /// Singular name of poco, this is the inner node name, 
        /// for example it would be project in the below xml:
        /// <projects>
        ///  <project>
        ///    ...
        ///  </project>
        /// </projects>
        /// </param>
        protected BaseAdaptor(string itemNodeName)
        {
            this.itemNodeName = itemNodeName;
        }


        public T Create(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) throw new ArgumentNullException("xml");
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), xmlReaderSettings))
            {
                return Create(XDocument.Load(xmlReader).Elements().First());
            }
        }


        private static T Create(XElement xElement)
        {
            if (xElement == null) throw new ArgumentNullException("xElement");

            Type pocoType = typeof (T);
            object poco = Activator.CreateInstance(pocoType);
            var pocoProperties = new List<PropertyInfo>(pocoType.GetProperties());
            foreach (XElement element in xElement.Elements())
            {
                object value = element.Value;
                string name = element.Name.LocalName.Replace("-", string.Empty);
                PropertyInfo propertyInfo =
                    pocoProperties.FirstOrDefault(i => i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

                if (propertyInfo == null) continue;
                if (!propertyInfo.CanWrite) continue;


                Type conversionType = propertyInfo.PropertyType;

                bool isNullableType = conversionType.IsNullableType();//  Nullable.GetUnderlyingType(conversionType) == null  && !(value is string);

                if (isNullableType)
                {
                    conversionType = Nullable.GetUnderlyingType(conversionType);
                    value = string.IsNullOrEmpty(value.ToString()) ? null : Convert.ChangeType(value, conversionType);
                }
                else
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
        //              //  value.To<>();

        //                Type genericClass = typeof(value.To);
        //// MakeGenericType is badly named
        //                Type constructedClass = genericClass.MakeGenericType(conversionType);

                        value = Convert.ChangeType(value, conversionType);
                    }
                    else
                    {
                        if (conversionType == typeof (Decimal))
                        {
                            value = Decimal.Zero;
                        }
                        else if (conversionType == typeof (int))
                        {
                            value = 0;
                        }
                        else if (conversionType == typeof (DateTime))
                        {
                            value = DateTime.MinValue;
                        }
                        else if (conversionType == typeof (bool))
                        {
                            value = false;
                        }
                        else if (conversionType != typeof (string))
                        {
                            throw new ApplicationException(
                                "Application Error:Default null value for non nullable type " + conversionType.Name +
                                " is not implemented.");
                        }
                    }
                }

                propertyInfo.SetValue(poco, value, null);
            }
            return (T) poco;
        }

        public IList<T> CreateList(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) throw new ArgumentNullException("xml");

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), xmlReaderSettings))
            {
                XDocument document = XDocument.Load(xmlReader);

                return new List<T>(
                    from p in document.Descendants(itemNodeName)
                    select Create(p)
                    );
            }
        }

        public string ToXml(T poco)
        {
            var serializer = new XmlSerializer(typeof (T));
            var writer = new StringWriter();
            serializer.Serialize(writer, poco);

            StringBuilder builder = writer.GetStringBuilder();
            return builder.ToString();
        }
    }
}