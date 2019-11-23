using System;
//Referenciar y usar
using System.Web.UI.WebControls;

namespace WebCineclub.Clases
{
    public class clsNacionalidad
    {
        #region "Constructor"
        public clsNacionalidad(string nombreApp)
        {
            strApp = nombreApp;
            strSQL = string.Empty;
            Error = string.Empty;
        }
        #endregion

        #region "Atributos/Propiedades"
        private string strApp;
        private string strSQL;
        public string Error { get; private set; }
        #endregion

        #region "Métodos Públicos"
        public bool LlenarCombo(DropDownList Combo)
        {
            try
            {
                if (Combo == null)
                {
                    Error = "Combo Nulo";
                    return false;
                }

                clsGenerales objLlenar = new clsGenerales(strApp);
                strSQL = "EXEC USP_Nacionalidad_Pelicula;";

                if (!objLlenar.LlenarCombo(Combo, strSQL, "Codigo", "Nombre"))
                {
                    Error = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;

                return true;
            }
            catch
            {
                Error = "Error en el llenado del combo(2)";
                return false;
            }
        }
        #endregion
    }
}