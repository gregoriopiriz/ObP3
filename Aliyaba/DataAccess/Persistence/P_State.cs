using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_State
    {
        public static void AddState(DTO_State DTO_S)
        {
            State S = Mappers.M_State.MapToEntity(DTO_S);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.States.Add(S);

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

        public static string GetCurrentStateNameByOrderID(int orderID)
        {
            string Name = "";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Name = context.States.Where(w => w.OrderID == orderID).OrderByDescending(o => o.Date).FirstOrDefault().Name;
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return Name;
        }

        public static List<DTO_State> GetStatesByOrderID(int iD)
        {
            List<State> S = new List<State>();
            List<DTO_State> _S = new List<DTO_State>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        S = context.States.Where(w => w.OrderID == iD).ToList();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            foreach(State s in S)
            {
                _S.Add(Mappers.M_State.MapToDTO(s));
            }

            return _S;
        }

        public static void DeleteState(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        State S = context.States.FirstOrDefault(f => f.ID == _ID);

                        context.States.Remove(S);

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
    }
}
