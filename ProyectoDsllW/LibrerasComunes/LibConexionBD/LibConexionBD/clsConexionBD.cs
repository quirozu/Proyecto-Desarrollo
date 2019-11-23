using System;


//Referenciar y usar
using System.Data;
using System.Data.SqlClient;

//Referenciar y usar
using LibParametros;

namespace LibConexionBD
{
    public class clsConexionBD
    {
    #region "Atríbutos/Propiedades"
        private string strApp;         //Para el nombre de la aplicación
        private string strCadenaCnx;   //Para la cadena de conexón a la BD
        private bool   blnHayCnx;      //Para saber si hay o no Cnx a la BD

        public string SQL     { private get; set; } //Para la sentencia SQL a ejecutarse en la BD
        public object VrUnico { get; private set; } //Para la captura y retorno del Vr. único(método: ConsultarValorUnico)) 
        public string Error   { get; private set; } //Para el mensaje de error
        public SqlDataReader  DataReader_Lleno { get; private set; } //Para el objeto DataReader (contenedor de info)
        public DataSet        DataSet_Lleno    { get; private set; } //Para el objeto DataSet (contenedor de info)

        private SqlConnection  objCnx;      //Para el objeto Conexión
        private SqlCommand     objCmd;         //Para el objeto Command (realiza la transacción)
        private SqlDataAdapter objAdapter; //Para el objeto DataAdapter (para llenar el DataSet, entre otros)
    #endregion
        
    #region "Constructor"
        public clsConexionBD( string NombreAplicacion )
        {
            strApp = NombreAplicacion;
            blnHayCnx = false;
            objCnx = new SqlConnection();      //Para la Conexión
            objCmd = new SqlCommand();         //Para la Transacción
            objAdapter = new SqlDataAdapter(); //Para la llenar el DataSet
            DataSet_Lleno = new DataSet();     //Para el DataSet
        }
    #endregion

    #region "Métodos Privados"
        private bool GenerarCadenaCnx()
        {
            try
            {
                if ( string.IsNullOrEmpty( strApp ) )
                {
                    Error = "Sin Nombre de la aplicación";
                    return false;
                }
                clsParametros objParams = new clsParametros();
                if ( ! objParams.GenerarCadenaCnx( strApp ) )
                {
                    Error = objParams.Error;
                    objParams = null;
                    return false;
                }
                strCadenaCnx = objParams.CadenaCnx;
                objParams = null;
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                return false;
            }
        }
        private bool AbrirCnx()
        {
            if ( ! GenerarCadenaCnx() )
                return false;
            objCnx.ConnectionString = strCadenaCnx;
            try
            {
                objCnx.Open();
                blnHayCnx = true;
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                blnHayCnx = false;
                return false;
            }
        }
    #endregion

    #region "Métodos Públicos"
        public void CerrarCnx()
        {
            try
            {
                objCnx.Close();
                objCnx = null;
                blnHayCnx = false;
            }
            catch ( Exception ex )
            {
                Error = "No se cerró o liberó la conexión, " + ex.Message;
            }
        }
        public bool Consultar( bool blnParametros )
        {
            try
            {
                if ( string.IsNullOrEmpty( SQL ) )
                {
                    Error = "No definió la instrucción SQL";
                    return false;
                }
                if ( ! blnHayCnx )
                    if ( ! AbrirCnx() )
                        return false;
                //Preparar el Comando para ejecutar la transacción SQL en la BD
                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if ( blnParametros )
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;
                DataReader_Lleno = objCmd.ExecuteReader();  //Realizar la transacción en la BD
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                return false;
            }
        }
        public bool ConsultarValorUnico( bool blnParametros )
        {
            try
            {
                if ( string.IsNullOrEmpty( SQL ) )
                {
                    Error = "No definió la instrucción SQL";
                    return false;
                }
                if ( ! blnHayCnx )
                    if ( ! AbrirCnx() )
                        return false;
                //Preparar el Comando para ejecutar la transacción SQL en la BD
                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if ( blnParametros )
                    objCmd.CommandType = CommandType.StoredProcedure
                        ;
                else
                    objCmd.CommandType = CommandType.Text;
                VrUnico = objCmd.ExecuteScalar();  //Realizar la transacción en la BD
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                return false;
            }
        }
        public bool EjecutarSentencia( bool blnParametros )
        {
            try
            {
                if ( string.IsNullOrEmpty( SQL ) )
                {
                    Error = "No definió la instrucción SQL";
                    return false;
                }
                if ( ! blnHayCnx )
                    if ( ! AbrirCnx() )
                        return false;
                //Preparar el Comando para ejecutar la transacción SQL en la BD
                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if ( blnParametros )
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;
                objCmd.ExecuteNonQuery();   //Realizar la transacción en la BD
                return true;
            }
            catch ( Exception ex )
            {
                Error = ex.Message;
                return false;
            }
        }
        public bool LlenarDataSet( bool blnParametros )
        {
            try
            {
                if ( string.IsNullOrEmpty( SQL ) )
                {
                    Error = "No definió la instrucción SQL";
                    return false;
                }
                if ( ! blnHayCnx )
                    if ( ! AbrirCnx() )
                        return false;
                //Preparar el Comando para el DataAdapter
                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if ( blnParametros )
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;
                //Preparar el DataAdapter para el uso del comando en la BD
                //El DataAdapter Utiliza el Command para la transacción
                objAdapter.SelectCommand = objCmd; 
                //Realizar la transacción en la BD y el llenado del DataSet/Datatable
                objAdapter.Fill(DataSet_Lleno);
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
