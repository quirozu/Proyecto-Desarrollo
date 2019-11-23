using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCineclub
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        #region "Variables Globales"
            static private string strApp;
            string strRpta, strCodigo, strTitulo, strFecha;
            static private int intOpcion, intNac, intPro, intCodEmpl;
        #endregion

        #region "Métodos Propios"
        private void Mensaje(string Msj)
        {
            this.lblMsj.Text = Msj;
        }

        private void LimpiarDatos()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtFecha.Text = string.Empty;
            this.txtTitulo.Text = string.Empty;
            this.txtEmpl.Text = string.Empty;

            Mensaje(string.Empty);
        }

        private void Limpiar()
        {
            this.ddlNac.SelectedIndex = 0;
            intNac = Convert.ToInt16(this.ddlNac.SelectedValue);
            this.ddlProductora.SelectedIndex = 0;
            intPro = Convert.ToInt16(this.ddlProductora.SelectedValue);
            LimpiarDatos();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strApp = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                LlenarComboNacionalidad();
                LlenarComboProductora();
                LlenarGrid();
            }
        }

        private void LlenarComboNacionalidad()
        {
            Clases.clsNacionalidad objBuscar = new Clases.clsNacionalidad(strApp);
            if (!objBuscar.LlenarCombo(this.ddlNac))
            {
                Mensaje(objBuscar.Error);
                objBuscar = null;
                return;
            }
            objBuscar = null;
            intNac = Convert.ToInt32(this.ddlNac.SelectedValue);

        }

        protected void ddlNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            intNac = Convert.ToInt32(this.ddlNac.SelectedValue);
            LlenarGrid();
            LimpiarDatos();
        }

        private void LlenarComboProductora()
        {
            Clases.clsProductora objBuscar = new Clases.clsProductora(strApp);
            if (!objBuscar.LlenarCombo(this.ddlProductora))
            {
                Mensaje(objBuscar.Error);
                objBuscar = null;
                return;
            }
            objBuscar = null;
            intPro = Convert.ToInt32(this.ddlProductora.SelectedValue);

        }

        protected void grvDatos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string strOpcion = e.CommandName.ToLower();
            int index = Convert.ToInt32(e.CommandArgument);
            if (index >= 0)
            {
                strCodigo = grvDatos.Rows[index].Cells[1].Text;
                switch (strOpcion)
                {
                    case "select":
                        Buscar();
                        break;
                }
            }
        }

        protected void ddlProductora_SelectedIndexChanged(object sender, EventArgs e)
        {
            intPro = Convert.ToInt32(this.ddlProductora.SelectedValue);
            LimpiarDatos();
        }


        private void Buscar()
        {
            Clases.clsPelicula objBuscar = new Clases.clsPelicula(strApp);
            if (!objBuscar.BuscarMaestro(strCodigo))
            {
                Limpiar();
                Mensaje(objBuscar.Error);
                objBuscar = null;
                return;
            }

            this.ddlNac.SelectedValue = Convert.ToString(objBuscar.Nacionalidad);
            this.ddlNac_SelectedIndexChanged(null, null);
            this.ddlProductora.SelectedValue = Convert.ToString(objBuscar.Productora);
            this.ddlProductora_SelectedIndexChanged(null, null);
            this.txtCodigo.Text = strCodigo;
            this.txtTitulo.Text = objBuscar.Titulo.ToString();
            this.txtFecha.Text = objBuscar.FechaE.ToString();
            this.txtEmpl.Text = Convert.ToString(objBuscar.Empleado);
            return;
        }

        private bool Grabar()
        {
            try
            {
                strCodigo = this.txtCodigo.Text.Trim().ToUpper();
                strTitulo = this.txtTitulo.Text.Trim();
                strFecha = this.txtFecha.Text.Trim();
                intNac = Convert.ToInt16(this.ddlNac.SelectedValue);
                intPro = Convert.ToInt16(this.ddlProductora.SelectedValue);
                intCodEmpl = Convert.ToInt16(this.txtEmpl.Text);

                if (string.IsNullOrEmpty(strCodigo))
                {
                    Mensaje("Código de Película no válido");
                    this.txtCodigo.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(strTitulo))
                {
                    Mensaje("Título de la película no válido");
                    this.txtTitulo.Focus();
                    return false;
                }
                if (intCodEmpl <= 0)
                {
                    Mensaje("Id del empleado no válido");
                    this.txtEmpl.Focus();
                }
                if (string.IsNullOrEmpty(strFecha))
                {
                    Mensaje("Genero no válido");
                    this.txtFecha.Focus();
                    return false;
                }
                if (intPro <= 0)
                {
                    Mensaje("Seleccione una Productora");
                    this.ddlProductora.Focus();
                    return false;
                }

                if (intNac <= 0)
                {
                    Mensaje("Seleccione una Nacionalidad");
                    this.ddlProductora.Focus();
                    return false;
                }
                Clases.clsPelicula objX = new Clases.clsPelicula(strApp, strCodigo,
                    strTitulo, strFecha, intNac, intPro, intCodEmpl);
                if (intOpcion == 1)
                {
                    if (!objX.GrabarMaestro())
                    {
                        Mensaje(objX.Error);
                        objX = null;
                        this.txtCodigo.Focus();
                        return false;
                    }
                }
                else if (intOpcion == 2)
                {
                    if (!objX.ModificarMaestro())
                    {
                        Mensaje(objX.Error);
                        objX = null;
                        this.txtCodigo.Focus();
                        return false;
                    }
                }
                else
                {
                    Mensaje("Error no clasificado -> Consultar con admón. del sistema");
                    objX = null;
                    this.txtCodigo.Focus();
                    return false;
                }
                strRpta = objX.Respuesta;
                if (strRpta == "-1")
                {
                    Mensaje("Ya existe un registro con la misma matricula");
                    objX = null;
                    this.txtCodigo.Focus();
                    return false;
                }
                if (strRpta == "0")
                {
                    Mensaje("Error al realizar el procedimiento");
                    objX = null;
                    this.txtCodigo.Focus();
                    return false;
                }
                objX = null;
                this.txtCodigo.ReadOnly = true;
                this.txtTitulo.Focus();
                Mensaje("Registro grabado con éxito");
                Buscar();
                return true;
            }
            catch
            {
                Mensaje("Error en el grabar");
                return false;

            }
        }

        private void LlenarGrid()
        {
            Clases.clsPelicula objBuscar = new Clases.clsPelicula(strApp);
            if (!objBuscar.LlenarGrid(this.grvDatos, intNac))
            {
                Mensaje(objBuscar.Error);
                return;
            }
        }

        protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                strCodigo = this.txtCodigo.Text.Trim();
                if (string.IsNullOrEmpty(strCodigo))
                {
                    Mensaje("Código de la pelicula no válido");
                    this.txtCodigo.ReadOnly = false;
                    this.txtCodigo.Focus();
                    return;
                }
                this.txtCodigo.ReadOnly = true;
                this.ibtnBuscar.Visible = false;
                Buscar();
            }
            catch (Exception ex)
            {
                Mensaje("Error -> " + ex.Message);
            }
        }



        protected void mnuOpciones_MenuItemClick(object sender, MenuEventArgs e)
        {
            Mensaje(string.Empty);
            this.ibtnBuscar.Visible = false;
            this.txtCodigo.ReadOnly = true;
            switch (this.mnuOpciones.SelectedValue)
            {
                case "opcBuscar":
                    {
                        intOpcion = 0;
                        Limpiar();
                        this.ibtnBuscar.Visible = true;
                        this.txtCodigo.ReadOnly = false;
                        this.txtCodigo.Focus();
                        break;
                    }
                case "opcAgregar":
                    {
                        intOpcion = 1;
                        Limpiar();
                        this.txtCodigo.ReadOnly = false;
                        this.txtCodigo.Focus();
                        break;
                    }
                case "opcModificar":
                    {
                        intOpcion = 2;
                        this.txtCodigo.ReadOnly = true;
                        this.txtTitulo.Focus();
                        break;
                    }
                case "opcGrabar":
                    {
                        if (intOpcion == 1 || intOpcion == 2)
                        {
                            if (Grabar())
                                intOpcion = 0;
                        }
                        else
                            Mensaje("Opción no válida, agregar primero");
                        break;
                    }
                case "opcCancelar":
                    {
                        Limpiar();
                        intOpcion = 0;
                        break;
                    }
                case "opcImprimir":
                    {
                        intOpcion = 0;
                        break;
                    }
                default:
                    Mensaje("Opción no válida");
                    break;
            }
        }
    }
}