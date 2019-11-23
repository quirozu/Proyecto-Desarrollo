using System;

//Referenciar y Usar
using LibConexionBD;
using System.Web.UI.WebControls;

namespace LibLlenarCBList
{
    public class clsLlenarCBList
    {
    #region "Atributos/Propiedades"
        private string strApp;
        public string SQL        { private get; set; }
        public string CampoID    { private get; set; }
        public string CampoTexto { private get; set; }
        public string Error      { get; private set; }
    #endregion

    #region "Constructor"
        public clsLlenarCBList( string NombreAplicacion )
        {
            strApp = NombreAplicacion;
            SQL = string.Empty;
            CampoID = string.Empty;
            CampoTexto = string.Empty;
            Error = string.Empty;
        }
    #endregion

    #region "Métodos Privados"
        private bool Validar()
        {
            if ( string.IsNullOrEmpty( SQL ) )
            {
                Error = "Debe definir una instrucción SQL";
                return false;
            }
            if ( string.IsNullOrEmpty( CampoID ) )
            {
                Error = "Debe definir el nombre del Campo con la PK(Id)";
                return false;
            }
            if ( string.IsNullOrEmpty( CampoTexto ) )
            {
                Error = "Debe definir el nombre del Campo con valores Texto";
                return false;
            }
            return true;
        }
    #endregion

    #region "Métodos Públicos"
        public bool LlenarCheckBL_Web( CheckBoxList Generico )
        {
            if ( ! Validar() )
                return false;
            clsConexionBD objConexionBd = new clsConexionBD( strApp );
            try
            {
                objConexionBd.SQL = SQL;
                if ( ! objConexionBd.LlenarDataSet( false ) )
                {
                    Error = objConexionBd.Error;
                    objConexionBd.CerrarCnx();
                    objConexionBd = null;
                    return false;
                }
                Generico.DataSource = objConexionBd.DataSet_Lleno.Tables[0];
                Generico.DataValueField = CampoID;
                Generico.DataTextField = CampoTexto;
                Generico.DataBind();
                objConexionBd.CerrarCnx();
                objConexionBd = null;
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                return false;
            }
        }
    #endregion
    }
}
