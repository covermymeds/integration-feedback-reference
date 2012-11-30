using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    public class FeedbackEndpoint : IFeedbackService
    {
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
