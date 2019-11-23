using System;
//Referenciar y usar
using System.Web.UI.WebControls;
using LibConexionBD;
using LibLlenarCombos;
using LibLlenarGrids;
using LibLlenarRBList;

namespace WebCineclub.Clases
{
    public class clsGenerales
    {
        #region "Constructor"
        public clsGenerales(string nombreApp)
        {
            strApp = nombreApp;
            Error = string.Empty;
        }
        #endregion

        #region "Atributos/Propiedades"
        private string strApp;
        public string Error { get; private set; }
        #endregion

        #region "Métodos Privados"
        private bool validar()
        {
            if (string.IsNullOrEmpty(strApp))
            {
                Error = "Faltan datos importantes";
                return false;
            }
            return true;
        }

        private bool validar2(string campo1, string campo2)
        {
            if (string.IsNullOrEmpty(campo1))
            {
                Error = "Faltan datos importantes de campo";
                return false;
            }
            if (string.IsNullOrEmpty(campo2))
            {
                Error = "Faltan datos importantes de campo";
                return false;
            }
            return true;
        }
        #endregion

        #region "Métodos Públicos"
        public bool LlenarGrid(GridView Grid, string SQL)
        {
            try
            {
                if (!validar())
                    return false;

                if (Grid == null || string.IsNullOrEmpty(SQL))
                {
                    Error = "Error en el llenado de Grid(1)";
                    return false;
                }

                clsLlenarGrids objXX = new clsLlenarGrids(strApp);
                objXX.SQL = SQL;

                if (!objXX.LlenarGrid_Web(Grid))
                {
                    Error = "Error en el llenado de Grid(10)";
                    objXX = null;
                    return false;
                }
                //Modificar el grid para un mejor aspecto
                Grid.GridLines = GridLines.Both;
                Grid.CellPadding = 2;
                Grid.ForeColor = System.Drawing.Color.Black;
                Grid.BackColor = System.Drawing.Color.Beige;
                Grid.AlternatingRowStyle.BackColor = System.Drawing.Color.Gainsboro;
                Grid.HeaderStyle.BackColor = System.Drawing.Color.Aqua;
                objXX = null;

                return true;
            }
            catch
            {
                Error = "Error en el llenado de Grid(2)";
                return false;
            }
        }

        public bool LlenarCombo(DropDownList Combo, string SQL, string campoPK, string campoTexto)
        {
            try
            {
                if (!validar() || !validar2(campoPK, campoTexto))
                    return false;

                if (Combo == null || string.IsNullOrEmpty(SQL))
                {
                    Error = "Error en el llenado de Combo(1)";
                    return false;
                }

                clsLlenarCombos objXY = new clsLlenarCombos(strApp);
                objXY.SQL = SQL;
                objXY.CampoID = campoPK;
                objXY.CampoTexto = campoTexto;

                if (!objXY.LlenarCombo_Web(Combo))
                {
                    Error = "Error en el llenado de Combo(10)";
                    objXY = null;
                    return false;
                }
                objXY = null;

                return true;
            }
            catch
            {
                Error = "Error en el llenado de Combo(2)";
                return false;
            }
        }

        public bool LlenarRadioBL(RadioButtonList RadioBL, string SQL, string campoPK, string campoTexto)
        {
            try
            {
                if (!validar() || !validar2(campoPK, campoTexto))
                    return false;

                if (RadioBL == null || string.IsNullOrEmpty(SQL))
                {
                    Error = "Error en el llenado de Radio(1)";
                    return false;
                }

                clsLlenarRBList objXZ = new clsLlenarRBList(strApp);
                objXZ.SQL = SQL;
                objXZ.CampoID = campoPK;
                objXZ.CampoTexto = campoTexto;

                if (!objXZ.LlenarRadioBL_Web(RadioBL))
                {
                    Error = "Error en el llenado de Radio(10)";
                    objXZ = null;
                    return false;
                }
                objXZ = null;

                return true;
            }
            catch
            {
                Error = "Error en el llenado de Radio(2)";
                return false;
            }
        }
        #endregion
    }
}