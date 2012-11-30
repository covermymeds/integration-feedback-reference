using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{

    [System.SerializableAttribute()]
    public enum DispositionType
    {
        New,
        /// <remarks>Includes status of PA Request, Question Request</remarks>
        SentToPlan,
        /// <remarks>Includes Appeal Response, PA Response, Question Response</remarks>
        ResponseFromPlan,
        AppealedToPlan,
        Failure,
        Cancelled,
        Expired,
        Deleted
    }

    [System.SerializableAttribute()]
    public enum PlanOutcomeType
    {
        Approved,
        Denied,
        Cancelled,
        Pending,
        Unsent,
        Unknown,
        NotApplicable
    }

    [System.SerializableAttribute()]
    public enum RecipientClassType
    {
        User,
        Group,
        Workqueue
    }

    //recipient enum for sharing with group, work queue or single user as the qualifier
    [System.SerializableAttribute()]
    public class RecipientType
    {
        public string Identifier;
        RecipientClassType RecipientClass;
        bool PresentOnDashboard;
    }

    [System.SerializableAttribute()]
    public enum RequestSourceType
    {
        Prescriber,
        Pharmacy,
        ElectronicPrescribing,
        RejectedPharmacyClaim
    }
    // Prescriber Manual Prescriber, 
    // Pharmacy - Manual Pharmacy, 
    // ElectronicPrescription - Electronic Prescribing, 
    // RejectedPharmacyClaim - Obvious
}