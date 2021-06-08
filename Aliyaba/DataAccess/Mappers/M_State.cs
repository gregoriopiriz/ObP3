using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_State
    {
        public static DTO_State MapToDTO(State entity)
        {
            DTO_State dto = new DTO_State();
            dto.ID = entity.ID;
            dto.State = GetStateByName(entity.Name);
            dto.Name = GetStateName(dto.State);
            dto.Date = entity.Date;
            dto.OrderID = entity.OrderID;
            dto.EmployeeID = entity.EmployeeID;

            return dto;
        }

        public static State MapToEntity(DTO_State dto)
        {
            State entity = new State();
            entity.ID = dto.ID;
            entity.Name = GetStateName(dto.State);
            entity.Date = dto.Date;
            entity.OrderID = dto.OrderID;
            entity.EmployeeID = dto.EmployeeID;

            return entity;
        }

        public static string GetStateName(States states)
        {
            if (states == States.Pending)
            {
                return "Pendiente";
            }
            if (states == States.Preparing)
            {
                return "En Preparación";
            }
            if (states == States.Prepared)
            {
                return "Preparado";
            }
            if (states == States.ReadyForDelivery)
            {
                return "Listo para Entregar";
            }
            if (states == States.Delivering)
            {
                return "En Reparto";
            }
            if (states == States.Delivered)
            {
                return "Entregado";
            }
            else
            {
                return "Cancelado";
            }
        }
        public static States GetStateByName(string name)
        {
            if (name == "Pendiente")
            {
                return States.Pending;
            }
            if (name == "En Preparación")
            {
                return States.Preparing;
            }
            if (name == "Preparado")
            {
                return States.Prepared;
            }
            if (name == "Listo para Entregar")
            {
                return States.ReadyForDelivery;
            }
            if (name == "En Reparto")
            {
                return States.Delivering;
            }
            if (name == "Entregado")
            {
                return States.Delivered;
            }
            else
            {
                return States.Cancelled;
            }
        }
    }
}
