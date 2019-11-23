using System;

//Referenciar y Usar
using LibConexionBD;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace LibLlenarCombos
{
    public class clsLlenarCombos
    {
    #region "Atributos/Propiedades"
        private string strApp;
        public string SQL        { private get; set; }
        public string CampoID    { private get; set; }
        public string CampoTexto { private get; set; }
        public string Error      { get; private set; }
    #endregion

    #region "Constructor"
        public clsLlenarCombos( string NombreAplicacion )
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
            if  ( string.IsNullOrEmpty( SQL ) )
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
        public bool LlenarCombo_Windows( ComboBox Generico )
        {
            if ( ! Validar() )
                return false;
            try
            {
                clsConexionBD objConexionBd = new clsConexionBD( strApp );
                objConexionBd.SQL = SQL;
                if ( ! objConexionBd.LlenarDataSet( false ) )
                {
                    Error = objConexionBd.Error;
                    objConexionBd.CerrarCnx();
                    objConexionBd = null;
                    return false;
                }
                Generico.DataSource = objConexionBd.DataSet_Lleno.Tables[0];
                Generico.ValueMember = CampoID;
                Generico.DisplayMember = CampoTexto;
                Generico.Refresh();
                objConexionBd.CerrarCnx();
                objConexionBd = null;
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        public bool LlenarCombo_Web    ( DropDownList Generico )
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
                Generico.DataTextField  = CampoTexto;
                Generico.DataBind();
                objConexionBd.CerrarCnx();
                objConexionBd = null;
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
    #endregion
    }
}
