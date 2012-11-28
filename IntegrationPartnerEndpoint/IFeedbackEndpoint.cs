using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IntegrationPartnerEndpoint
{
    [ServiceContract]
    public interface IFeedbackService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        void UpdatePriorAuthorizationRequest(NCPDP.org.schema.SCRIPT.RxChangeResponse rxChangeResponse);
    }

    // Need to build a data contract that holds RxChangeResponse as well as our status, user and group information
    [DataContract]
    public class FeedbackUpdate
    {
        NCPDP.org.schema.SCRIPT.RxChangeRequest rxChangeRequest;
        // Creator information

        // Concerned user information
        // IntegrationPartner Identifier
        NCPDP.org.schema.SCRIPT.SecurityType Recipient;

        // Concerned Group information
        [DataMember]
        public NCPDP.org.schema.SCRIPT.RxChangeRequest FeedbackData
        {
            get { return rxChangeRequest; }
            set { rxChangeRequest = value; }
        }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
