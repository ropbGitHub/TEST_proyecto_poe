﻿using Control;
using System;
using System.Windows.Forms;

namespace Visual {
    public partial class Frm_Cita_Editar : Form {
        Adm_Odontologo admO = Adm_Odontologo.GetAdm ();
        Adm_Cita admCita = Adm_Cita.GetAdm ();
        public Frm_Cita_Editar () {
            InitializeComponent ();
            admCita.BloquearCampos (txtCedula, dtpFecha, cmbHora, cmbOdontologo);

        }

        private void txtCedula_KeyPress (object sender, KeyPressEventArgs e) {
            char c = e.KeyChar;
            if (char.IsLetter (c) && (e.KeyChar != Convert.ToChar (Keys.Back))) {
                e.Handled = true;
                return;
            }
        }

        private void btnBuscar_Click (object sender, EventArgs e) {
            string cedula = txtCedula.Text;
            DateTime fecha = dtpFecha.Value.Date;
            admCita.LlenarComboH (cmbHora);
            admCita.BuscarDatos (dgvCitas, cedula, txtCedula, dtpFecha, cmbHora, cmbOdontologo);

        }

        private void btnLimpiar_Click (object sender, EventArgs e) {
            admCita.LimpiarCampos (lblId, txtCedula, lblPaciente, dgvCitas, dtpFecha, cmbHora, cmbOdontologo, lblConsultorio, txtRegistro);
            //admCita.BloquearCampos(dgvCitas, txtCedula, dtpFecha, cmbHora, cmbOdontologo);
        }

        private void btnActualizar_Click (object sender, EventArgs e) {
            string cedula = txtCedula.Text, hora = cmbHora.Text, odontologo = cmbOdontologo.Text;
            int id = Convert.ToInt32 (lblId.Text);
            DateTime fecha = dtpFecha.Value.Date;
            DateTime dHora = DateTime.Parse (hora, System.Globalization.CultureInfo.CurrentCulture);
            errorP.Clear ();
            if (admCita.Validar (txtCedula, cmbHora, dtpFecha, cmbOdontologo, errorP)) {
                errorP.Clear ();
                admCita.Editar (id, cedula, odontologo, fecha, dHora, txtRegistro);
            }
        }

        private void cmbOdontologo_SelectedValueChanged (object sender, EventArgs e) {
            string odontologo = (string)cmbOdontologo.SelectedItem;
            admO.LabelConsultorio (odontologo, cmbOdontologo, lblConsultorio);
        }

        private void cmbHora_SelectedValueChanged (object sender, EventArgs e) {
            string hora = cmbHora.Text;
            DateTime fecha = dtpFecha.Value.Date;
            DateTime dHora = DateTime.Parse (hora, System.Globalization.CultureInfo.CurrentCulture);
            cmbOdontologo.Items.Clear();
            admO.llenarComboO (fecha, dHora, cmbHora, cmbOdontologo);
        }

        private void btnModificar_Click (object sender, EventArgs e) {
            int posicion = dgvCitas.CurrentRow.Index, id = Convert.ToInt32 (dgvCitas.Rows [posicion].Cells ["col_id"].Value);
            if (posicion >= 0) {
                if (admCita.AtencionExistente (dgvCitas, posicion) == false) {
                    admCita.ActualizarDatos (posicion, id, lblId, txtCedula, lblPaciente, dtpFecha, cmbHora, cmbOdontologo, lblConsultorio);
                } else {
                    MessageBox.Show ("Este paciente ya fue atendido", "Error al editar");
                }
            } else {
                MessageBox.Show ("No ha realizado la consulta");
            }
        }
    }
}
