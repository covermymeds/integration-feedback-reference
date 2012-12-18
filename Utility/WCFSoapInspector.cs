//-----------------------------------------------------------------------
// <copyright file="WcfSoapInspector.cs" company="CoverMyMeds">
//     Copyright (c) 2012 CoverMyMeds.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace CoverMyMeds.Feedback.Utility
{
    public class WcfSoapInspector : IClientMessageInspector, IEndpointBehavior
    {
        public List<string> sentMessages = new List<string>();
        public List<string> receivedMessages = new List<string>();

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            receivedMessages.Add(reply.ToString());
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            sentMessages.Add(request.ToString());
            return null;
        }

        #endregion

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
        //public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        //{

        //}
        #endregion
    }
}
