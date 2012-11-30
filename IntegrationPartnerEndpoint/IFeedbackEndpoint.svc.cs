using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class FeedbackEndpoint : IFeedbackService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void UpdatePriorAuthorizationRequest(FeedbackUpdate FeedbackData)
        {
            if (FeedbackData == null)
            {
                throw new ArgumentNullException("FeedbackData");
            }
            throw new NotImplementedException();
        }
    }
}
