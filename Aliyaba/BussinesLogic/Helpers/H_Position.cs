using Common.DTOs;
using DataAccess;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class H_Position
    {
        private static H_Position _instance;
        public static H_Position getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Position();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Position _P)
        {
            if (_P.Name.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Positions.Any(a => a.Name == _P.Name))
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return true;
        }

        public List<DTO_Position> GetPositions()
        {
            return P_Position.GetPositions();
        }

        public void AddPosition(DTO_Position _P)
        {
            if (IsValidData(_P))
            {
                P_Position.AddPosition(_P);
            }
        }

        public void DeletePosition(string Name)
        {
            P_Position.DeletePosition(Name);
        }

    }
}
