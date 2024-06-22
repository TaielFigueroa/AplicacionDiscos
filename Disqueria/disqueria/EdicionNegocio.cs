﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace disqueria
{
    public class EdicionNegocio
    {
        public List<TipoEdicion> listar()
        {
            List<TipoEdicion> ediciones = new List<TipoEdicion>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, Descripcion from TIPOSEDICION");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    TipoEdicion aux = new TipoEdicion();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    ediciones.Add(aux);
                }
                return ediciones;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
