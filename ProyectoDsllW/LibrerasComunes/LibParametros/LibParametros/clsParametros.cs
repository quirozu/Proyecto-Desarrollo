using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referenciar y usar
using System.Xml;
using System.Windows.Forms;

namespace LibParametros
{
    public class clsParametros
    {
    #region "Atributos/Propiedades"
        private string strServidor;
        private string strBaseDatos;
        private string strUsuario;
        private string strClave;
        private string strSegInt;
        private string strPathXml;

        public string CadenaCnx { get; private set; }
        public string Error     { get; private set; }

        private XmlDocument objDoc = new XmlDocument();
        private XmlNode objNodo;
    #endregion

    #region "Constructor"
        public clsParametros()
        {
            strServidor = string.Empty;
            strBaseDatos = string.Empty;
            strUsuario = string.Empty;
            strClave = string.Empty;
            strSegInt = string.Empty;
            strPathXml = string.Empty;
            CadenaCnx = string.Empty;
            Error = string.Empty;
        }
    #endregion

    #region "Métodos Públicos"
        public bool GenerarCadenaCnx( string strNombreAplicacion )
        {
            try
            {
                if ( string.IsNullOrEmpty( strNombreAplicacion ) )
                {
                    Error = "Sin Nombre de la aplicación";
                    return false;
                }
                strPathXml = Application.StartupPath + "\\CON_" + strNombreAplicacion + ".xml";
                objDoc.Load( strPathXml );
                objNodo = objDoc.SelectSingleNode("//Servidor");
                strServidor = objNodo.InnerText;
                objNodo = objDoc.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNodo.InnerText;
                objNodo = objDoc.SelectSingleNode("//Usuario");
                strUsuario = objNodo.InnerText;
                objNodo = objDoc.SelectSingleNode("//Clave");
                strClave = objNodo.InnerText;
                objNodo = objDoc.SelectSingleNode("//SeguridadIntegrada");
                strSegInt = objNodo.InnerText;

                if ( strSegInt.ToLower() == "no" ) //Autenticación SQL SERVER   
                    CadenaCnx = "Data Source=" + strServidor + 
                                "; Initial Catalog=" + strBaseDatos +
                                "; User Id =" + strUsuario + 
                                "; Password=" + strClave + ";";
                else if ( strSegInt.ToLower() == "si" )   //Autenticación  Windows
                    CadenaCnx = "Data Source=" + strServidor + 
                                "; Initial Catalog=" + strBaseDatos +
                                "; Integrated Security=SSPI;";
                else
                {
                    Error = "Proyecto no válido";
                    objDoc = null;
                    return false;
                }
                objDoc = null;
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                objDoc = null;
                return false;
            }
        }
    #endregion
    }
}
