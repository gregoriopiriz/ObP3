using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Position
    {
        public static void AddPosition(DTO_Position DTO_P)
        {
            Position P = Mappers.M_Position.MapToEntity(DTO_P);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Positions.Add(P);

                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }
        public static void DeletePosition(string _Name)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Position P = context.Positions.FirstOrDefault(f => f.Name == _Name);
                        context.Positions.Remove(P);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }

        public static List<DTO_Position> GetPositions()
        {
            List<DTO_Position> _Pos = new List<DTO_Position>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<Position> Pos = new List<Position>();
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Pos = context.Positions.ToList();
                    }

                    foreach(Position P in Pos)
                    {
                        _Pos.Add(Mappers.M_Position.MapToDTO(P));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _Pos;
        }
    }
}
