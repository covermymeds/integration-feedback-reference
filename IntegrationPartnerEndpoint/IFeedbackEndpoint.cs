using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    [ServiceContract]
    public interface IFeedbackService
    {
        [OperationContract]
        void UpdatePriorAuthorizationRequest(FeedbackUpdate FeedbackData);
    }

    [DataContract]
    public class FeedbackUpdate
    {
        private string IdField;
        private string ReturnURLField;
        private RequestSourceType RequestSourceField;
        private string CreatedByField;       // Created by user display name
        private DateTime CreationDateField;
        private DateTime UpdateDateField;
        private DateTime? AppealExpirationDateField;
        private string FormNameField;
        private List<RecipientType> RecipientsField;
        private DispositionType DispositionField;
        private PlanOutcomeType PlanOutcomeField;
        private NCPDP.org.schema.SCRIPT.RxChangeRequest _PARequestData;

        [DataMember(IsRequired=true)]
        public string Id
        {
            get { return IdField; }
            set { IdField = value; }
        }

        [DataMember(IsRequired = true)]
        public string ReturnURL
        {
            get { return ReturnURLField; }
            set { ReturnURLField = value; }
        }

        [DataMember(IsRequired = true)]
        public RequestSourceType RequestSource
        {
            get { return RequestSourceField; }
            set { RequestSourceField = value; }
        }

        [DataMember(IsRequired = true)]
        public string CreatedBy
        {
            get { return CreatedByField; }
            set { CreatedByField = value; }
        }

        [DataMember(IsRequired = true)]
        public DateTime CreationDate
        {
            get { return CreationDateField; }
            set { CreationDateField = value; }
        }

        [DataMember(IsRequired = true)]
        public DateTime UpdateDate
        {
            get { return UpdateDateField; }
            set { UpdateDateField = value; }
        }

        [DataMember]
        public DateTime? AppealExpirationDate
        {
            get { return AppealExpirationDateField; }
            set { AppealExpirationDateField = value; }
        }

        [DataMember(IsRequired = true)]
        public string FormName
        {
            get { return FormNameField; }
            set { FormNameField = value; }
        }

        [DataMember(IsRequired = true)]
        public List<RecipientType> Recipients
        {
            get { return RecipientsField; }
            set
            {
                RecipientsField = value;
            }
        }

        [DataMember(IsRequired = true)]
        public DispositionType Disposition
        {
            get { return DispositionField; }
            set { DispositionField = value; }
        }

        [DataMember(IsRequired = true)]
        public PlanOutcomeType PlanOutcome
        {
            get { return PlanOutcomeField; }
            set { PlanOutcomeField = value; }
        }

        [DataMember(IsRequired = true)]
        public NCPDP.org.schema.SCRIPT.RxChangeRequest PARequestData
        {
            get { return _PARequestData; }
            set { _PARequestData = value; }
        }
    }
}
