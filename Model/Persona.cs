﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model {
    public abstract class Persona {

        // Variables
        private string cedula;
        private char sexo;
        private string nombre;
        private DateTime fechaNacimiento;
        private string telefono;
        private string correo;
        private long id_persona;



        // Constructor: defautl
        public Persona()
        {
            this.cedula = "";
            this.sexo = ' ';
            this.nombre = "";
            this.fechaNacimiento = DateTime.Now;
            this.telefono = "";
            this.correo = "";
        }

        // Constructor: parameterized
        public Persona(string cedula, char sexo, string nombre, DateTime fechaNacimiento, string telefono, string correo)
        {
            this.cedula = cedula;
            this.sexo = sexo;
            this.nombre = nombre;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.correo = correo;
        }

        public string Cedula { get => cedula; set => cedula = value; }                              // Getter & Setter: cedula
        public char Sexo { get => sexo; set => sexo = value; }                                      // Getter & Setter: sexo
        public string Nombre { get => nombre; set => nombre = value; }                              // Getter & Setter: nombre
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; } // Getter & Setter: fechaNacimiento
        public string Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
        public long Id_persona { get => id_persona; set => id_persona = value; }


        // Method: LeerEdad
        public int LeerEdad()
        {
            int output = 0;
            // ¿?
            output = (int)Math.Round((DateTime.Now.Date - fechaNacimiento.Date).TotalDays, MidpointRounding.AwayFromZero);
            output = output / 365;
            return output;
        }

        // Method: Leersexo
        public string LeerSexo()
        {
            String output = "";
            if (sexo == 'F')
            {
                output = "Femenino";
            }
            else if (sexo == 'M')
            {
                output = "Masculino";
            }
            return output;
        }

        // Method: ToString
        public override string ToString()
        {
            return
                "\r\nNombre: " + nombre +
                "\r\nCedula: " + cedula +
                "\r\nFecha de nacimiento: " + fechaNacimiento.ToShortDateString() +
                "\r\nEdad: " + LeerEdad() +
                "\r\nSexo: " + LeerSexo() +
                "\r\nTeléfono: " + telefono +
                "\r\nCorreo: " + correo; ;
        }

    }
}
