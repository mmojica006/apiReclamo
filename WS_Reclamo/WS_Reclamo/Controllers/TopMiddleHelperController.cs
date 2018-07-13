using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;

namespace WS_Reclamo.Controllers
{
    public class TopMiddleHelperController : ApiController
    {

        public String usuario { get; set; }
        public String clave { get; set; }
        public String serviceName { get; set; }
        public Dictionary<String, String> parametros { get; set; }
        public String salida { get; set; }


        public TopMiddleHelperController()
        {

        }

        public TopMiddleHelperController(String usuario, String clave, String serviceName, Dictionary<String, String> parametros)
        {
            this.usuario = usuario;
            this.clave = clave;
            this.serviceName = serviceName;
            this.parametros = parametros;
        }


        private String autenticar()
        {

            String Autenticacion;
            StringBuilder DocumentoXML = null;
            XmlWriter EscritorXML = null;

            DocumentoXML = new StringBuilder();
            EscritorXML = XmlWriter.Create(DocumentoXML);

            EscritorXML.WriteStartDocument();
            EscritorXML.WriteStartElement("xmlJBankExecutionParameters");
            EscritorXML.WriteStartElement("authentication");
            EscritorXML.WriteStartElement("type");
            EscritorXML.WriteEndElement();
            EscritorXML.WriteStartElement("userName");
            EscritorXML.WriteString(this.usuario);
            EscritorXML.WriteEndElement();
            EscritorXML.WriteStartElement("password");
            EscritorXML.WriteString(this.clave);
            EscritorXML.WriteEndElement();
            EscritorXML.WriteStartElement("sessionID");
            EscritorXML.WriteString("0");
            EscritorXML.WriteEndElement();
            EscritorXML.WriteEndElement();
            EscritorXML.WriteEndElement();

            EscritorXML.WriteEndDocument();
            EscritorXML.Close();

            Autenticacion = DocumentoXML.ToString();

            try
            {
                DocumentoXML = new StringBuilder();
                EscritorXML = XmlWriter.Create(DocumentoXML);
            }
            catch (Exception ex)
            {
                String err = ex.Message.ToString();
                Autenticacion = "";
            }
            finally
            {
                DocumentoXML = null;
                EscritorXML = null;
            }
            return Autenticacion;

        }

        /// <summary>
        /// Ejecuta un servicio Topaz MiddleWare a partir de los parámetros solicitados
        /// </summary>
        /// <param name="serviceName">Nombre del Servicio</param>
        /// <param name="structure">Pares de String [Llave, Valor] con los nombres de parámetros y sus valores solicitados por el servicio</param>
        /// <returns>String con respuesta desde el servicio</returns>
        public String execute(params String[] SalidaParam)
        {

            TOPAZMID.TopazMiddleWareWSService ClienteTMW;
            TOPAZMID.execute Entrada;
            TOPAZMID.executeResponse Salida;
            StringBuilder CadenaXML;
            XmlWriter xmlWriter;
            XmlDocument documentoXml = null;
            String strRespuesta = "";
            bool success = false;

            try
            {
                Entrada = new TOPAZMID.execute();
                Entrada.executionInfo = autenticar();

                //Encabezado XML de entrada
                CadenaXML = new StringBuilder();
                xmlWriter = XmlWriter.Create(CadenaXML);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("xmlJBankRequest");
                xmlWriter.WriteStartElement("xmlJBankService");

                //NOMBRE DEL SERVICIO WEB
                xmlWriter.WriteStartElement("serviceName");
                xmlWriter.WriteString(serviceName);
                xmlWriter.WriteEndElement();

                //DEFINICION DE LOS PARAMETROS DE ENTRADA
                xmlWriter.WriteStartElement("xmlJBankFields");

                foreach (KeyValuePair<String, String> key in this.parametros)
                {
                    xmlWriter.WriteStartElement("xmlJBankField");
                    xmlWriter.WriteStartElement("fieldName");
                    xmlWriter.WriteString(key.Key);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("fieldValue");
                    xmlWriter.WriteString(key.Value);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                }

                xmlWriter.WriteEndElement(); //CIERRA xmlJBankFields
                xmlWriter.WriteEndElement(); //CIERRA xmlJBankService
                xmlWriter.WriteEndElement(); //CIERRA xmlJBankRequest
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();

                Entrada.request = CadenaXML.ToString();

                ClienteTMW = new TOPAZMID.TopazMiddleWareWSService();
                Salida = ClienteTMW.execute(Entrada);

                if (Salida != null)
                {
                    String r1, r2;
                    documentoXml = new XmlDocument();
                    documentoXml.LoadXml(Salida.executeResult);
                    documentoXml.SelectSingleNode("TopazMiddleWareResponse/errorCode");

                    if (documentoXml.SelectSingleNode("TopazMiddleWareResponse/errorCode").InnerText != "0")
                    {
                        strRespuesta = "Error HTTP-400 BAD REQUEST: " + documentoXml.SelectSingleNode("TopazMiddleWareResponse/error").InnerText;
                    }
                    else
                    {
                        r1 = documentoXml.SelectSingleNode("TopazMiddleWareResponse//" +
                            serviceName + "//" + SalidaParam[0]).InnerText.ToString();

                        r2 = documentoXml.SelectSingleNode("TopazMiddleWareResponse//" +
                            serviceName + "//" + SalidaParam[1]).InnerText.ToString();

                        if (r1 == "1")
                        {
                            strRespuesta = "Error HTTP-400 Validación Topaz " + r1 + " : " + r2;
                        }
                        else
                        {
                            strRespuesta = "HTTP-200: Successfull Operation";
                            success = true;
                        }
                    }
                }
                else
                {
                    strRespuesta = "Error HTTP-500 INTERNAL SERVER ERROR: Ocurrió un error inesperado al procesar la solicitud";
                }
            }
            catch (Exception ex)
            {
                strRespuesta = ex.Message.ToString();
            }
            finally
            {
                ClienteTMW = null;
                Entrada = null;
                Salida = null;
                CadenaXML = null;
                xmlWriter = null;
                documentoXml = null;
            }
            return strRespuesta;

        }
    }
}
