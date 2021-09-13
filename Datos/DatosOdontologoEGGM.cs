﻿using Model;
using Proyecto_Dentalig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Datos
{
    public class DatosOdontologoEGGM
    {
        ConexionHLBV con2 = new ConexionHLBV();
       

        List<Odontologo> odontologo = new List<Odontologo>();
        ConexionEGGM con = new ConexionEGGM();
        SqlCommand cmd = new SqlCommand();
        DatosHorarioEGGM datosHorario = null;
        DatosEspecialidadEGGM especialida = null;
        public int A()
        {
            string stmt = "SELECT COUNT(*) FROM Odontologo";
            int count = 0;

            Console.WriteLine(stmt);
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {


                    cmd.Connection = con.Cn;
                    cmd.CommandText = stmt;


                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }



                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologo" + ex.Message);

                }
            }
            con.cerrar();
            
            return count;
        }

        

        public List<Odontologo> ConsultarOdontologos(string dia, DateTime hora)
        {
            List<Odontologo> odontologos = new List<Odontologo>();
            Odontologo o = null;
            string sql = "SELECT PE.id_persona, PE.cedula, PE.id_sexo, PE.nombre, PE.fecha_nacimiento," +
            "O.id_horario, O.consultorio \n" +
            "FROM Persona PE \n" +
            "INNER JOIN Odontologo O ON PE.id_persona = O.id_persona \n" +
            "INNER JOIN Horario H ON O.id_horario = H.id_horario \n" +
            "INNER JOIN HorarioDias HD ON H.id_horario = HD.id_horario \n" +
            "INNER JOIN Dias D ON HD.id_dias = D.id_dias \n" +
            "WHERE D.dia = '" + dia + "' \n" +
            "AND '" + hora + "'  BETWEEN D.hora_entrada AND D.hora_salida; ";
            SqlDataReader dr = null;
            Console.WriteLine(sql);
            string mensaje = "";
            mensaje = con2.Conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        o = new Odontologo();
                        o.Id_persona = Convert.ToInt32(dr["id_persona"]);
                        o.Cedula = dr["cedula"].ToString();
                        o.Sexo = Convert.ToChar(dr["id_sexo"]);
                        o.Nombre = dr["nombre"].ToString();
                        o.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
                        o.Consultorio = Convert.ToInt32(dr["consultorio"]);
                        odontologos.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla paciente " + ex.Message);
                }
            }
            con2.Cerrar();
            return odontologos;
        }

        public Odontologo ConsultarOdontologo(string nombre)
        {
            Odontologo o = null;
            string sql = "SELECT PE.id_persona, PE.cedula, PE.id_sexo, PE.nombre, PE.fecha_nacimiento," +
            "O.consultorio \n" +
            "FROM Persona PE \n" +
            "INNER JOIN Odontologo O ON PE.id_persona = O.id_persona \n" +
            "WHERE PE.nombre = '" + nombre + "' ;";
            SqlDataReader dr = null;
            Console.WriteLine(sql);
            string mensaje = "";
            mensaje = con2.Conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        o = new Odontologo();
                        o.Id_persona = Convert.ToInt32(dr["id_persona"]);
                        o.Cedula = dr["cedula"].ToString();
                        o.Sexo = Convert.ToChar(dr["id_sexo"]);
                        o.Nombre = dr["nombre"].ToString();
                        o.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
                        o.Consultorio = Convert.ToInt32(dr["consultorio"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla paciente " + ex.Message);
                }
            }
            con2.Cerrar();
            return o;
        }
        public Odontologo ConsultarPersonaOdont(string cedula)
        {
            List<Persona> p = new List<Persona>();
            
            List<Dias> dia = new List<Dias>();
            Odontologo o = null;
            string sql = "Select * from Odontologo where cedula = '" + cedula + "'";
            string cossultaprueba = "Select * from Persona where cedula = " + cedula ;
            SqlDataReader dr = null;
            SqlDataReader dr2 = null;
            Console.WriteLine(cossultaprueba); 
            o = new Odontologo(0, "", 0, null, "", 'F',"", DateTime.Now, "", "");
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = cossultaprueba;
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        
                        o.Nombre = dr["nombre"].ToString();

                        string sexo = dr["sexo"].ToString();
                        o.Sexo = sexo[0];

                            o.FechaNacimiento = Convert.ToDateTime(dr["fechaNac"].ToString());

                            o.Correo = dr["correo"].ToString();
                            o.Telefono = dr["telefono"].ToString();
                             
                            p.Add(o);
                  }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona "+ex.Message);

                }


            }
            con.cerrar();
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {  
                    
                    cmd.Connection = con.Cn;
                    cmd.CommandText = cossultaprueba;
                   
                    cmd.CommandText = sql;
                    dr2 = cmd.ExecuteReader();
                    if (dr2.Read())
                    {
                        DatosEspecialidadEGGM espec = new DatosEspecialidadEGGM();
                        DatosHorarioEGGM h = new DatosHorarioEGGM();

                        int especi= Convert.ToInt32(dr2["id_especialidad"]);
                        int horario = Convert.ToInt32(dr2["id_horario"]);
                        o.Id_Odontologo = Convert.ToInt32(dr2["id_odontologo"]);

                        o.Cedula = dr2["cedula"].ToString();
                        o.Consultorio = Convert.ToInt32(dr2["consultorio"].ToString());
                        o.Horario = h.consultarTipodeHoraario(horario);
                        o.Especialidad = espec.consultarEspecialidad(especi);
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologp " + ex.Message);

                }
            }
            con.cerrar();

        
            return o;
        }

        public string eliminar(int id)
        {
            string sql1 = "DELETE FROM Odontologo WHERE id_odontologo = " + id;

            //string sql = "INSERT INTO Odontologo(id_odontologo,consultorio,cedula) VALUES(" + odo.Id_Odontologo + ",'" + odo.Consultorio + "," + odo.Cedula + ")";
            Console.WriteLine(sql1);
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();

                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologo" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;
        }

        public String insertarPersonas(Odontologo odo)
        {

            string sql1 = "INSERT INTO Persona(cedula, nombre, sexo, fechaNac, correo, telefono) VALUES("
            + odo.Cedula + ",'" + odo.Nombre + "','" + odo.Sexo + "','" + odo.FechaNacimiento.ToString("yyyy-MM-dd") + 
            "','" + odo.Telefono + "','" + odo.Correo + "')";

            //string sql = "INSERT INTO Odontologo(id_odontologo,consultorio,cedula) VALUES(" + odo.Id_Odontologo + ",'" + odo.Consultorio + "," + odo.Cedula + ")";
            Console.WriteLine(sql1);
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();

                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologo" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;

        }

        public String insertarOdontologo(Odontologo odo, int especialidad, int horario)
        {



            string sql = "INSERT INTO Odontologo(id_odontologo, id_horario, id_especialidad, consultorio, cedula) VALUES(" + odo.Id_Odontologo +
                 ", '" + horario + "', '" + especialidad + "','" + odo.Consultorio + "'," + odo.Cedula + ")";
            Console.WriteLine(sql);
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologo" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;

        }
        

        public List<Odontologo> ConsultarPersonaOdontTodos()
        {
            List<Odontologo> p = new List<Odontologo>();

            List<Dias> dia = new List<Dias>();
            Odontologo o = null, o2 = null;

            string cossultaprueba = "Select * from Odontologo";
            SqlDataReader dr = null;

            Console.WriteLine(cossultaprueba);

            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = cossultaprueba;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        o = new Odontologo(0, "", 0, null, "", 'F', "", DateTime.Now, "", "");
                        DatosEspecialidadEGGM espec = new DatosEspecialidadEGGM();
                        DatosHorarioEGGM datosHorario = new DatosHorarioEGGM();
                        o.Cedula = dr["cedula"].ToString();
                        o2 = consultarodontologp(o.Cedula);

                        int especilidad, horario;

                        especilidad = Convert.ToInt32(dr["id_especialidad"].ToString());
                        string es = espec.consultarEspecialidad(especilidad);
                        horario = Convert.ToInt32(dr["id_horario"].ToString());
                        o.Id_Odontologo = Convert.ToInt32(dr["id_odontologo"]);
                        o.Especialidad = es;
                        o.Horario = datosHorario.consultarTipodeHoraario(horario);
                        o.Cedula = dr["cedula"].ToString();
                        o.Consultorio = Convert.ToInt32(dr["consultorio"].ToString());
                        o.Sexo = o2.Sexo;
                        o.Correo = o2.Correo;
                        o.FechaNacimiento = o2.FechaNacimiento;
                        o.Telefono = o2.Telefono;
                        o.Nombre = o2.Nombre;
                        p.Add(o);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona " + ex.Message);

                }

            }
            con.cerrar();


            return p;
        }


        public Odontologo ConsultaparaEditar(string cedula, string nombre)
        {
            List<Persona> p = new List<Persona>();
            Odontologo o = null;
            string sql = "Select * from Odontologo where cedula ='" + cedula + "'";
            string consulta = "SELECT Odontologo.id_odontologo, Persona.cedula, Persona.nombre, Horario.tipo, Especialidad2.especialidad, Persona.sexo, Persona.fechaNac, Persona.correo, Persona.telefono, Odontologo.consultorio, Odontologo.cedula" +
           " FROM Odontologo INNER JOIN" +
           " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
           " Horario ON Odontologo.id_odontologo = Horario.id_Horario INNER JOIN" +
           " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
           " WHERE (Persona.cedula ='" + cedula + "')  AND (Persona.nombre ='" + nombre + "')";
            SqlDataReader dr = null;
            Console.WriteLine(consulta);
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = consulta;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        o = new Odontologo(0, "", 0, null, "", 'F', "", DateTime.Now, "", "");

                        o.Id_Odontologo = Convert.ToInt32(dr["id_odontologo"]);

                        o.Cedula = dr["cedula"].ToString();
                        o.Nombre = dr["nombre"].ToString();
                        o.Horario.Tipo = dr["tipo"].ToString();
                        o.Especialidad = dr["especialidad"].ToString();
                        o.Sexo = Convert.ToChar(dr["sexo"].ToString());
                        o.FechaNacimiento = Convert.ToDateTime(dr["fechaNac"].ToString());
                        o.Correo = dr["correo"].ToString();
                        o.Telefono = dr["telefono"].ToString();
                        o.Consultorio = Convert.ToInt32(dr["consultorio"].ToString());
                        p.Add(o);
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologp" + ex.Message);

                }
            }
            con.cerrar();

            return o;
        }

        public Odontologo consultarodontologp(string cedula)
        {
            List<Persona> p = new List<Persona>();

            List<Dias> dia = new List<Dias>();
            Odontologo o = null;
            string sql = "Select * from Persona where cedula = '" + cedula + "'";

            SqlDataReader dr = null;

            Console.WriteLine(odontologo);
            o = new Odontologo(0, "", 0, null, "", 'F', "", DateTime.Now, "", "");
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {


                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;


                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {


                        o.Nombre = dr["nombre"].ToString();

                        string sexo = dr["sexo"].ToString();
                        o.Sexo = sexo[0];

                        o.FechaNacimiento = Convert.ToDateTime(dr["fechaNac"].ToString());

                        o.Correo = dr["correo"].ToString();
                        o.Telefono = dr["telefono"].ToString();

                        p.Add(o);


                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologp " + ex.Message);

                }
            }
            con.cerrar();


            return o;
        }
        public string eliminarOdontologoPersona(string cedula)
        {
            string sql1 = "DELETE FROM Persona WHERE cedula = " + cedula;

         
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();

                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;
        }

        public string ACtualizar(Odontologo odo)
        {
            string sql1 = "UPDATE Persona set  cedula = '" + odo.Cedula + "',nombre = '" + odo.Nombre + "', sexo = '" + odo.Sexo + "', fechaNac = '" + odo.FechaNacimiento + "', correo = '" + odo.Telefono + "', telefono = '" + odo.Correo +
            "' WHERE cedula = " + odo.Cedula;


            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();


                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;
        }
        public string ACtualizar2(Odontologo odo, int tipo, int espe)
        {
            DatosHorarioEGGM datosHorario = new DatosHorarioEGGM();
            DatosEspecialidadEGGM espec = new DatosEspecialidadEGGM();



            string sql1 = "UPDATE Odontologo set  id_odontologo = '" + odo.Id_Odontologo + "', id_especialidad = '" + espe + "', id_horario = '" + tipo + "', consultorio = '" + odo.Consultorio + "', cedula = '" + odo.Cedula +
                "' WHERE cedula = " + odo.Cedula;

            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();

                    return "1";
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona" + ex.Message);
                    return "0" + ex.Message;
                }

            }
            con.cerrar();
            return mensaje;
        }

        public List<Odontologo> ConsultarPersonaOdontTodosF(string jornada, string especialidad, int consultorio)
        {
            List<Odontologo> p = new List<Odontologo>();


            List<Dias> dia = new List<Dias>();
            Odontologo o = null, o2 = null;
            string sql = filtro(jornada, especialidad, consultorio);
  
            SqlDataReader dr = null;

            Console.WriteLine(sql);

            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {
                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        o = new Odontologo(0, "", 0, null, "", 'F', "", DateTime.Now, "", "");

                        DatosEspecialidadEGGM espec = new DatosEspecialidadEGGM();
                        DatosHorarioEGGM datosHorario = new DatosHorarioEGGM();
                        o.Cedula = dr["cedula"].ToString();
                        o2 = consultarodontologp(o.Cedula);
                        int especilidad, horario;
                        especilidad = Convert.ToInt32(dr["id_especialidad"].ToString());
                        string es = espec.consultarEspecialidad(especilidad);
                        horario = Convert.ToInt32(dr["id_horario"].ToString());
                        o.Id_Odontologo = Convert.ToInt32(dr["id_odontologo"]);
                        o.Especialidad = es;
                        o.Horario = datosHorario.consultarTipodeHoraario(horario);
                        o.Cedula = dr["cedula"].ToString();
                        o.Consultorio = Convert.ToInt32(dr["consultorio"].ToString());
                        o.Sexo = o2.Sexo;
                        o.Correo = o2.Correo;
                        o.FechaNacimiento = o2.FechaNacimiento;
                        o.Telefono = o2.Telefono;
                        o.Nombre = o2.Nombre;
                        p.Add(o);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla persona " + ex.Message);

                }


            }
            con.cerrar();


            return p;
        }

        private string filtro(string jornada, string especialidad, int consultorio)
        {
            Console.WriteLine(jornada);
            Console.WriteLine(especialidad);
            Console.WriteLine(consultorio);
            string sql="";
            if ((especialidad == "") & (String.IsNullOrEmpty(jornada) == false & jornada != "Fines de semana") & (consultorio == 0))
            {
                Console.WriteLine("-----Jornada");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                  " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                 " FROM  Horario INNER JOIN" +
                  " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                  " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                 " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
                " WHERE (Horario.tipo = '" + jornada + " I') OR" +
                " (Horario.tipo = '" + jornada + " II')";
            }
            else if ((especialidad == "") & (String.IsNullOrEmpty(jornada) == false & jornada == "Fines de semana") & (consultorio == 0))
            {
                Console.WriteLine("-----Jornada(Fin de semana)");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                  " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                 " FROM  Horario INNER JOIN" +
                  " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                  " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                 " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
                " WHERE (Horario.tipo = '" + jornada + "')";
            }
            else if ((jornada == "") & (String.IsNullOrEmpty(especialidad) == false) & (consultorio == 0))
            {
                Console.WriteLine("-----Especialidad");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                  " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                 " FROM  Horario INNER JOIN" +
                  " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                  " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                 " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
                " WHERE (Especialidad2.especialidad = '" + especialidad + "')";
            }
            else if ((String.IsNullOrEmpty(especialidad) == false) & (String.IsNullOrEmpty(jornada) == false & jornada != "Fines de semana") & (consultorio == 0))
            {
                Console.WriteLine("-----Jornada & Especialidad");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Especialidad2.especialidad = '" + especialidad + "') AND (Horario.tipo = '" + jornada + " I' OR" +
                " Horario.tipo = '" + jornada + " II')";
            }
            else if ((consultorio > 0) & (String.IsNullOrEmpty(jornada) == false & jornada != "Fines de semana") & (String.IsNullOrEmpty(especialidad) == false))
            {
                Console.WriteLine("-----Jornada & Especialidad & consultorio");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Especialidad2.especialidad = '" + especialidad + "') AND (Odontologo.consultorio = " + consultorio + ") AND (Horario.tipo = '" + jornada + " I' OR" +
                " Horario.tipo = '" + jornada + " II')";
            }
            else if ((consultorio > 0) & (jornada == "") & (especialidad == ""))
            {
                Console.WriteLine("-----Consultorio");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Odontologo.consultorio = " + consultorio + ")";
            }
            else if ((consultorio > 0) & (jornada == "Fines de semana") & (String.IsNullOrEmpty(especialidad) == false))
            {
                Console.WriteLine("-----Jornada(Fin de semana) & Especialidad & consultorio");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Especialidad2.especialidad = '" + especialidad + "') AND (Odontologo.consultorio = " + consultorio + ") AND (Horario.tipo = '" + jornada + "')";
            }
            else if ((consultorio > 0) & (jornada == "") & (String.IsNullOrEmpty(especialidad) == false))
            {
                Console.WriteLine("----- Especialidad & consultorio");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Especialidad2.especialidad = '" + especialidad + "') AND (Odontologo.consultorio = " + consultorio + ")";
            }
            else if ((consultorio > 0) & (String.IsNullOrEmpty(jornada) == false & jornada != "Fines de semana") & (especialidad == "")) { 
                Console.WriteLine("-----Jornada & consultorio");
                sql = "SELECT Horario.tipo, Odontologo.consultorio, Especialidad2.especialidad, Persona.cedula, Odontologo.id_horario, Odontologo.id_especialidad, Persona.nombre, Persona.sexo, Persona.fechaNac, " +
                 " Persona.correo, Persona.telefono, Odontologo.id_odontologo" +
                " FROM  Horario INNER JOIN" +
                 " Odontologo ON Horario.id_Horario = Odontologo.id_horario INNER JOIN" +
                 " Persona ON Odontologo.cedula = Persona.cedula INNER JOIN" +
                " Especialidad2 ON Odontologo.id_especialidad = Especialidad2.id_especialidad" +
               " WHERE (Odontologo.consultorio = " + consultorio + ") AND (Horario.tipo = '" + jornada + " I' OR" +
                " Horario.tipo = '" + jornada + " II')";
            }
            else
            {

                sql = "Select* from Odontologo";
            }

            return sql;
        }



        public List<Odontologo> consultarodontologpFiltro2(string cedula)
        {
            List<Odontologo> p = new List<Odontologo>();

            List<Dias> dia = new List<Dias>();
            Odontologo o = null;
            string sql = "Select * from Odontologo where cedula = '" + cedula + "'";

            SqlDataReader dr = null;

            Console.WriteLine(odontologo);
            o = new Odontologo(0, "", 0, null, "", 'F', "", DateTime.Now, "", "");
            string mensaje = "";
            mensaje = con.conectar();
            if (mensaje[0] == '1')
            {
                try
                {


                    cmd.Connection = con.Cn;
                    cmd.CommandText = sql;


                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        DatosEspecialidadEGGM espec = new DatosEspecialidadEGGM();
                        DatosHorarioEGGM datosHorario = new DatosHorarioEGGM();
                        int especilidad, horario;

                        especilidad = Convert.ToInt32(dr["id_especialidad"].ToString());
                        string es = espec.consultarEspecialidad(especilidad);
                        horario = Convert.ToInt32(dr["id_horario"].ToString());
                        o.Id_Odontologo = Convert.ToInt32(dr["id_odontologo"]);
                        o.Especialidad = es;
                        o.Horario = datosHorario.consultarTipodeHoraario(horario);
                        o.Cedula = dr["cedula"].ToString();
                        o.Consultorio = Convert.ToInt32(dr["consultorio"].ToString());
                        p.Add(o);
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar en la tabla odontologp " + ex.Message);

                }
            }
            con.cerrar();


            return p;
        }
    }
}
