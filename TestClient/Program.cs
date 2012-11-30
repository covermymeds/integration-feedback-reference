using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoverMyMeds.Feedback.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegrationPartner.FeedbackServiceClient IPFeedbackService = new IntegrationPartner.FeedbackServiceClient();
            
            IntegrationPartner.FeedbackUpdate PARequest = new IntegrationPartner.FeedbackUpdate();

            PARequest.Id = "A12B34";
            PARequest.ReturnURL = "https://www.covermymeds.com/request/view/";
            PARequest.RequestSource = IntegrationPartner.RequestSourceType.Prescriber;
            PARequest.CreatedBy = "Test CMM Presciber User";
            PARequest.CreationDate = new DateTime(2012, 11, 29, 13, 15, 17);
            PARequest.UpdateDate = DateTime.Now;
            PARequest.AppealExpirationDate = null;
            PARequest.FormName = "Fancy Test Form";
            PARequest.Recipients = GetSampleRecipientList();
            PARequest.Disposition = IntegrationPartner.DispositionType.New;
            PARequest.PlanOutcome = IntegrationPartner.PlanOutcomeType.Unknown;
            PARequest.PARequestData = GetSampleRequestFeedbackData();
        }

        private static IntegrationPartner.RecipientType[] GetSampleRecipientList()
        {
            List<IntegrationPartner.RecipientType> lsRet = new List<IntegrationPartner.RecipientType>();

            // User with active access to the PA request
            IntegrationPartner.RecipientType NormalUser = new IntegrationPartner.RecipientType()
            {
                Identifier = "UserXYZ123",
                RecipientClass = IntegrationPartner.RecipientClassType.User,
                PresentOnDashboard = true
            };
            // A group that a user with membership in will have access to a PA request
            IntegrationPartner.RecipientType GroupRecipient = new IntegrationPartner.RecipientType()
            {
                Identifier = "GroupID456",
                RecipientClass = IntegrationPartner.RecipientClassType.Group,
                PresentOnDashboard = true
            };
            // A user who no longer wishes to see or be alerted about the PA. Archived request
            IntegrationPartner.RecipientType PastUser = new IntegrationPartner.RecipientType()
            {
                Identifier = "UserXYZ123",
                RecipientClass = IntegrationPartner.RecipientClassType.User,
                PresentOnDashboard = false
            };
            return lsRet.ToArray();
        }

        private static IntegrationPartner.RxChangeRequest GetSampleRequestFeedbackData()
        {
            IntegrationPartner.RxChangeRequest rxRet = new IntegrationPartner.RxChangeRequest();

            return rxRet;
        }

    }
}
