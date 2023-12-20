﻿using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Repository;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TareaService : ITareaService
    {
        private readonly ToDoContext _todoContext;

        public TareaService(ToDoContext context)
        {
            _todoContext = context;
        }
        public async Task<List<Tarea>> GetAllTareasActivasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo)
                                      .ToListAsync();
        }

        public async Task<List<Tarea>> GetAllTareasBajasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo == false)
                                      .ToListAsync();
        }

        public async Task<bool> AddTareaAsync(TareaDTO tarea)
        {
            var newTask = new Tarea();

            if (tarea.Titulo != null && tarea.Descripcion != null)
            {
                newTask.Titulo = tarea.Titulo;
                newTask.Descripcion = tarea.Descripcion;
                newTask.FechaAlta = DateTime.Now;
                newTask.FechaModificacion = DateTime.Now;
                newTask.Activo = true;

                if (tarea.Estado.ToLower().Trim() == "pendiente" || tarea.Estado?.ToLower().Trim() == "en curso" || tarea.Estado?.ToLower().Trim() == "finalizada")
                {
                    newTask.Estado = tarea.Estado.ToUpper();
                }
                else
                {
                    return false;
                }

                await _todoContext.Tareas.AddAsync(newTask);
            }

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> UpdateTareaAsync(int id, string changes)
        {
            var updatedTask = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);

            if (updatedTask == null) return false;
            if(changes != null && (changes.Trim() == "pendiente" || changes.Trim() == "en curso" || changes.Trim() == "finalizada"))
            {
                updatedTask.Estado = updatedTask.Estado;

                updatedTask.Descripcion = updatedTask.Descripcion;

                updatedTask.FechaModificacion = updatedTask.FechaModificacion;

                updatedTask.Titulo = updatedTask.Titulo;
            }

            

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteTareasAsync(int id)
        {
            var tareaDeleted = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
            if (tareaDeleted == null) return false;
            tareaDeleted.Activo = false;

            int rows = await _todoContext.SaveChangesAsync();
        
            return rows > 0; 
        }


    }
}