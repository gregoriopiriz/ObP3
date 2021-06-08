using Common.DTOs;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class H_Photo
    {
        private static H_Photo _instance;
        public static H_Photo getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Photo();
            }

            return _instance;
        }
        public void AddPhoto(DTO_Photo _P)
        {
            P_Photo.AddPhoto(_P);
        }

        public DTO_Photo GetImageByProductCode(string ProductCode)
        {
            return P_Photo.GetImageByProductCode(ProductCode);
        }
    }
}
