using System.Xml.Serialization;

namespace ePlatform.Integration.Helpers
{
    public class XmlNamespaceHelper
    {
        private static XmlSerializerNamespaces _InvoiceNamespaces;

        public static XmlSerializerNamespaces InvoiceNamespaces
        {
            get
            {
                if (XmlNamespaceHelper._InvoiceNamespaces == null)
                {
                    XmlNamespaceHelper._InvoiceNamespaces = new XmlSerializerNamespaces();
                    XmlNamespaceHelper._InvoiceNamespaces.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("cctc", "urn:un:unece:uncefact:documentation:2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
                    XmlNamespaceHelper._InvoiceNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                }
                return XmlNamespaceHelper._InvoiceNamespaces;
            }
        }
    }
}