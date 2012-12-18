//-----------------------------------------------------------------------
// <copyright file="IFeedbackEndpoint.svc.cs" company="CoverMyMeds">
//     Copyright (c) 2012 CoverMyMeds.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using System.Xml;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    public class FeedbackEndpoint : IFeedbackService
    {
        /// <summary>
        /// Simple interface implementation for receiving feedback updates from CoverMyMeds
        /// for prior authorization requests
        /// </summary>
        /// <remarks>
        /// This reference simply serializes the FeedbackData object to XML and writes it
        /// to the Windows Application Event Log. A full Integration Partner implementation
        /// would store the feedback data for users to view in a dashboard. The recipient 
        /// identifier sent with the feedback data would enable linking the update to the user
        /// who needs to see it
        /// </remarks>
        /// <param name="FeedbackData"></param>
        public void UpdatePriorAuthorizationRequest(FeedbackUpdate FeedbackData)
        {
            if (FeedbackData == null)
            {
                throw new ArgumentNullException("FeedbackData missing from service request");
            }

            // Serialize and log the feedback data to the Event Log - 
            System.IO.StringWriter sw = new System.IO.StringWriter(); ;
            new System.Xml.Serialization.XmlSerializer(typeof(FeedbackUpdate)).Serialize(sw, FeedbackData);

            EventLog el = new EventLog();
            el.Source = "IntegrationPartner.Feedback";
            el.WriteEntry(sw.ToString(), EventLogEntryType.Information);

        }
    }
}
