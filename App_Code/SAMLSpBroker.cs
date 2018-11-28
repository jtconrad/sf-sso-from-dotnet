using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.IO.Compression;

        public class ConsumeResponse
        {
            private XmlDocument xmlDoc;
            private Certificate certificate;

            public ConsumeResponse()
            {
                LoadProperties.initProperties();
                certificate = new Certificate();
                certificate.loadCert(LoadProperties.certificate);
            }

            public void LoadXml(string xml)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.LoadXml(xml);
            }

            public string LoadXmlFromBase64(string response)
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                LoadXml(enc.GetString(Convert.FromBase64String(response)));
                return response;
            }

            public bool isAuthenticated()
            {
                bool status = false;
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                XmlNodeList nodeList = xmlDoc.SelectNodes("//ds:Signature", manager);

                SignedXml signedXml = new SignedXml(xmlDoc);
                foreach (XmlNode node in nodeList)
                {
                    signedXml.LoadXml((XmlElement)node);
                    try
                    {
                        status = signedXml.CheckSignature(certificate.cert, true);
                    }
                    catch { }
                    if (!status)
                        return false;
                    return status;
                }
                return status;
            }

            public string getSubject()
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);
                return node.InnerText;
            }
            public string getUsername()
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='username']", manager);
                return node.InnerText;
            }

            public string getEmail()
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='email']", manager);
                return node.InnerText;
            }

            public string getPortalUser()
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='is_portal_user']", manager);
                return node.InnerText;
            }
        }

        public class AuthnRequest
        {
            public string id;
            private string issueInstant;

            public AuthnRequest()
            {
                LoadProperties.initProperties();
                id = "_" + System.Guid.NewGuid().ToString();
                issueInstant = System.DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            }

            public string GetRequest()
            {

                using (StringWriter writer = new StringWriter())
                {
                    XmlWriterSettings writerSetting = new XmlWriterSettings();
                    writerSetting.OmitXmlDeclaration = true;

                    using (XmlWriter xmlWriter = XmlWriter.Create(writer, writerSetting))
                    {
                        xmlWriter.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                        xmlWriter.WriteAttributeString("ID", id);
                        xmlWriter.WriteAttributeString("Version", "2.0");
                        xmlWriter.WriteAttributeString("IssueInstant", issueInstant);
                        xmlWriter.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                        xmlWriter.WriteAttributeString("AssertionConsumerServiceURL", LoadProperties.assertionConsumerServiceUrl);
                        xmlWriter.WriteAttributeString("Destination", LoadProperties.idpIssuerUrl);
                        xmlWriter.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                        xmlWriter.WriteString(LoadProperties.issuer);
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }

                    byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(writer.ToString());
                    using (MemoryStream output = new MemoryStream())
                    {
                        using (DeflateStream zip = new DeflateStream(output, CompressionMode.Compress))
                            zip.Write(toEncodeAsBytes, 0, toEncodeAsBytes.Length);
                        byte[] compressed = output.ToArray();
                        return System.Convert.ToBase64String(compressed);
                    }
                    return null;
                }
            }
        }

        public class Certificate
        {
            public X509Certificate2 cert;

            public void loadCert(string certificate)
            {
                string file = "\\CT_DOL_Cert_for_SSO.crt";
                cert = new X509Certificate2(X509Certificate2.CreateFromCertFile(file));
               // cert.Import(StringToByteArray(certificate));

                // Load the certificate into an X509Certificate object.
                // X509Certificate cert = new X509Certificate();
                //cert.Import(Certificate);

                // Get the value.
                string resultsTrue = cert.ToString(true);

                // Display the value to the console.
                Console.WriteLine(resultsTrue);

                // Get the value.
                string resultsFalse = cert.ToString(false);

                // Display the value to the console.
                Console.WriteLine(resultsFalse);
            }

                private byte[] StringToByteArray(string st)
                {
                    byte[] bytes = new byte[st.Length];
                    for (int i = 0; i < st.Length; i++)
                    {
                        bytes[i] = (byte)st[i];
                    }
                    return bytes;
                }
            }
   