using System;
//Referencias
using LibConexionBD;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebCineclub.Clases
{
    public class clsPelicula
    {
        #region "Atributos/Propiedades"
            private string strApp;
            private string strSQL;
            private clsConexionBD objCnx;
            private SqlDataReader Reader_Local;

            public string CodigoP { get; set; }
            public string Titulo   { get; set; }
            public string FechaE { get; set; }
            public int Nacionalidad { get; set; }
            public int Productora { get; set; }
            public int Empleado { get; set; }

            public string Respuesta { get; private set; }
            public string Error { get; private set; }
        #endregion

        #region "Constructores"
        public clsPelicula(string nombreApp)
        {
            strApp = nombreApp;
            CodigoP = string.Empty;
            Titulo = string.Empty;
            FechaE = string.Empty;
            Nacionalidad = 0;
            Productora = 0;
            Empleado = 0;
            strSQL = string.Empty;
            Respuesta = string.Empty;
            Error = string.Empty;
        }

        public clsPelicula(string App, string Cod, string Tit, string Fec, int Nac, int Pro, int Empl)
        {
            strApp = App;
            CodigoP = Cod;
            Titulo = Tit;
            FechaE = Fec;
            Productora = Pro;
            Nacionalidad = Nac;
            Empleado = Empl;
            strSQL = string.Empty;
            Respuesta = string.Empty;
            Error = string.Empty;
        }
        #endregion

        #region "Métodos Privados"
        private bool Grabar(string SQL)
        {
            try
            {
                objCnx = new clsConexionBD(strApp);
                objCnx.SQL = SQL;
                if (!objCnx.ConsultarValorUnico(false))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }
                Respuesta = objCnx.VrUnico.ToString().Trim();
                objCnx = null;

                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        #endregion

        #region "Metodos Públicos"
        public bool BuscarMaestro(string Codi)
        {
            try
            {
                if (string.IsNullOrEmpty(Codi))
                {
                    Error = "Carné no válido";
                    return false;
                }
                strSQL = "EXEC USP_PeliculaBuscarXCodigo '" + Codi + "';";
                objCnx = new clsConexionBD(strApp);
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))
                {
                    Error = objCnx.Error;
                    return false;
                }
                Reader_Local = objCnx.DataReader_Lleno;
                if (!Reader_Local.HasRows)
                {
                    Error = "No se encontró ningún registro: " + Codi;
                    Reader_Local.Close();
                    objCnx = null;
                    return false;
                }
                Reader_Local.Read();
                CodigoP = Reader_Local.GetString(0);
                Titulo = Reader_Local.GetString(1);
                FechaE = Reader_Local.GetString(2);
                Nacionalidad = Reader_Local.GetInt32(3);
                Productora = Reader_Local.GetInt32(4);
                Empleado = Reader_Local.GetInt32(5);

                Reader_Local.Close();
                objCnx = null;

                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }


        public bool GrabarMaestro()
        {
            try
            {
                strSQL = "EXEC USP_Pelicula_Grabar '" + CodigoP + "', '" + Titulo + "', '" + FechaE
                    + "', '" + Nacionalidad + "', '" + Productora + "','" + Empleado + "';";
                return (Grabar(strSQL));
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool ModificarMaestro()
        {
            try
            {
                strSQL = "EXEC USP_Pelicula_Modificar '" + CodigoP + "', '" + Titulo + "', '" + FechaE
                    + "', '" + Nacionalidad + "', '" + Productora + "','" + Empleado + "';";
                return (Grabar(strSQL));
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool LlenarGrid(GridView Grid, int idNaci)
        {
            try
            {
                if (Grid == null)
                {
                    Error = "Grid es nulo";
                    return false;
                }
                strSQL = "EXEC USP_PeliculaxNacion " + idNaci + ";";
                Clases.clsGenerales objLlenar = new Clases.clsGenerales(strApp);
                if (!objLlenar.LlenarGrid(Grid, strSQL))
                {
                    Error = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
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