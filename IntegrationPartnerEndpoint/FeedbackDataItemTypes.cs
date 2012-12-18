//-----------------------------------------------------------------------
// <copyright file="FeedbackDataItemTypes.cs" company="CoverMyMeds">
//     Copyright (c) 2012 CoverMyMeds.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    /// <summary>
    /// Status of the Prior Authorization Request with the plan
    /// </summary>
    [System.SerializableAttribute()]
    public enum DispositionType
    {
        New,
        SentToPlan,
        ResponseFromPlan,
        AppealedToPlan,
        Failure,
        Cancelled,
        Expired,
        Deleted
    }

    /// <summary>
    /// Status of the PA Request in the CoverMyMeds application
    /// </summary>
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

    /// <summary>
    /// Enumeration for the different recipients avaliable for linking to a PA Request
    /// </summary>
    [System.SerializableAttribute()]
    public enum RecipientClassType
    {
        User,
        Group,
        Workqueue
    }

    /// <summary>
    /// Recipient type for sharing with group, work queue or single user as the qualifier
    /// </summary>
    [System.Serializable]
    public class RecipientType
    {
        public string Identifier;
        public RecipientClassType RecipientClass;
        public bool IsDeletedForRecipient;
    }

    /// <summary>
    /// Describes the method by which the PA Request was created
    /// </summary>
    [System.SerializableAttribute()]
    public enum RequestSourceType
    {
        Prescriber,
        Pharmacy,
        ElectronicPrescribing,
        RejectedPharmacyClaim
    }
}