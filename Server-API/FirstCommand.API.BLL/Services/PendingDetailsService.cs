using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FirstCommand.API.BLL.Services
{
    public class PendingDetailsService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public PendingDetailsService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<PendPolicyInfoResponse>> GetPendingDetailsService(string PolicyNumber)
        {
            try
            {
                SqlParameter pNo = new SqlParameter("@PolNo", PolicyNumber);
                var res = await this._firstCommandDBContext.PendPolicyInfoResponse.FromSqlRaw("EXECUTE Select_PendingInf_AWS @PolNo", pNo).ToListAsync();

                //PendPolicyInfoResponse pendPolicyInfoResponse = new();

                //if (res != null)
                //{
                //    foreach (var item in res)
                //    {
                //        pendPolicyInfoResponse.PolicyNumber = item.PolicyNumber;
                //        pendPolicyInfoResponse.Owner = item.Owner;
                //        pendPolicyInfoResponse.Status = item.Status;
                //        pendPolicyInfoResponse.Company = item.Company;
                //        pendPolicyInfoResponse.Issue_Date = item.Issue_Date;
                //        pendPolicyInfoResponse.Last_Payment_Date = item.Last_Payment_Date;
                //        pendPolicyInfoResponse.Allotment_Payor = item.Allotment_Payor;
                //        pendPolicyInfoResponse.Insured = item.Insured;
                //        pendPolicyInfoResponse.DOB = item.DOB;
                //        pendPolicyInfoResponse.Issue_Age = item.Issue_Age;
                //        pendPolicyInfoResponse.Address_1 = item.Address_1;
                //        pendPolicyInfoResponse.BillTypeDescription = item.BillTypeDescription;
                //        pendPolicyInfoResponse.FrequencyDescription = item.FrequencyDescription;
                //        pendPolicyInfoResponse.Modal_Premium = item.Modal_Premium;
                //    }
                //}
                return res != null ? res : new List<PendPolicyInfoResponse>();
            }
            catch (Exception ex)
            {
                return new List<PendPolicyInfoResponse>
                {

                };
            }
        }

        public async Task<IEnumerable<PendingNotesResponse>> GetPendingNotesService(string PolicyNumber)
        {
            try
            {
                SqlParameter pNo = new SqlParameter("@PolNo", PolicyNumber);
                var res = await this._firstCommandDBContext.PendingNotesResponse.FromSqlRaw("EXECUTE Select_PendingNotes_AWS @PolNo", pNo).ToListAsync();

                //PendingNotesResponse pendingNotesResponse = new();
                //if (res != null)
                //{
                //    foreach (var item in res)
                //    {
                //        pendingNotesResponse.NoteMessage = item.NoteMessage;
                //        pendingNotesResponse.NoteSatisfied = item.NoteSatisfied;
                //        pendingNotesResponse.DateRequest = item.DateRequest;
                //    }
                //}
                return res != null ? res : new List<PendingNotesResponse>();
            }
            catch (Exception ex)
            {
                return new List<PendingNotesResponse>
                {
                };
            }
        }
    }
}
