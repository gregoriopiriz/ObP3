using Common.DTOs;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_DocumentType
    {
        public List<DTO_DocumentType> getDocumentTypes()
        {
            List<DTO_DocumentType> colDocumentTypes = new List<DTO_DocumentType>();

            using (AliyabaEntities context = new AliyabaEntities())
            {
                List<DocumentType> colTypes = context.DocumentTypes.Select(s => s).ToList();

                foreach (DocumentType item in colTypes)
                {
                    DTO_DocumentType dto = M_DocumentType.MapToDTO(item);
                    colDocumentTypes.Add(dto);
                }
            }

            return colDocumentTypes;
        }

        public static void AddDocumentType(DTO_DocumentType DTO_DC)
        {
            DocumentType DC = Mappers.M_DocumentType.MapToEntity(DTO_DC);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.DocumentTypes.Add(DC);

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

        public static void DeleteDocumentType(string _N)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        DocumentType D = context.DocumentTypes.FirstOrDefault(f => f.Name == _N);

                        context.DocumentTypes.Remove(D);

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
