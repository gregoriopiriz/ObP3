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
    public class H_DocumentType
    {
        private static H_DocumentType _instance;
        public static H_DocumentType getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_DocumentType();
            }

            return _instance;
        }

        public List<DTO_DocumentType> GetDocumentTypes()
        {
            P_DocumentType ptd = new P_DocumentType();
            return ptd.getDocumentTypes();
        }

        public bool IsValidData(DTO_DocumentType _DT)
        {
            if (_DT.Name.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.DocumentTypes.Any(a => a.Name == _DT.Name))
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

        public void AddDocumentType(DTO_DocumentType _DT)
        {
            if (IsValidData(_DT))
            {
                P_DocumentType.AddDocumentType(_DT);
            }
        }

        public void DeleteDocumentType(string N)
        {
            P_DocumentType.DeleteDocumentType(N);
        }

    }
}
