using System;
using System.Collections.Generic;
using System.Text;
using Personas.DAL;
using Personas.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Personas.BLL
{
    public class PersonaBll
    {
        public static bool Guardar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Personas.Add(persona) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Persona Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Persona persona = new Persona();

            try
            {
                persona = contexto.Personas.Find(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return persona;
        }

        public static bool Modificar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                
                contexto.Entry(persona).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }

        public static List<Persona> GetList(Expression<Func<Persona, bool>> persona)           
        {
            List<Persona> lista = new List<Persona>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Personas.Where(persona).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.Personas.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;

                paso = (contexto.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        

     

    }
}
