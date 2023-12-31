﻿using Control;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visual
{
    public partial class Frm_Odontologo_Horario : Form
    {
        Adm_Horario admhorario = Adm_Horario.GetAdm();

        


        public Frm_Odontologo_Horario()
        {
            InitializeComponent();
            admhorario.LlenarGrid(dgvHorario);
        }

        private void cmbDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvHorario.Rows.Clear();
            string dia = cmbDia.Text, jornada = cmbTipo.Text;
            admhorario.LlenarGridHorarioFiltrar(dgvHorario, dia, jornada);
        }


        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvHorario.Rows.Clear();
            string dia = cmbDia.Text, jornada = cmbTipo.Text;
            admhorario.LlenarGridHorarioFiltrar(dgvHorario, dia, jornada);
        }

        private void btnSeleccionarHorario_Click(object sender, EventArgs e)
        {
           
            this.Close();

        }
    

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           admhorario.llenarGridOdo(dgvHorarioOdontologo, dgvHorario);

        }
       
       
    }

       
    }

