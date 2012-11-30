using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Schema;

/// <summary>
/// Found this at http://thorarin.net/blog/post/2010/08/08/Controlling-WSDL-minOccurs-with-WCF.aspx
/// This WCF behavior changes metadata generation for service contracts, so that operation
/// parameters are required by default (XML schema minOccurs="1").
/// See http://thorarin.net/blog/post.aspx?id=5fe3b4b6-0e3e-463e-ac42-10c1c4808853 for
/// a more thorough explanation.
/// </summary>
/// <remarks>
/// Version 1.0 (2010-08-08):
/// - Original release
/// 
/// Version 1.1 (2011-04-14):
/// - Fixed a NullReferenceException that occurs when a service has two endpoints
///   configured that also use the same service contract. Thanks to Martin for reporting.
/// </remarks>
/// <example>
/// The OptionalAttribute can be used to mark individual parameters as optional.
/// <code>
/// [ServiceContract]
/// [RequiredParametersBehavior]
/// public interface IGreetingService
/// {
///     [OperationContract]
///     string Greet(string name, [Optional] string language);
/// }
/// </code>
/// </example>
/// 
namespace CoverMyMeds.Feedback.IntegrationPartnerEndpoint
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class RequiredParametersBehaviorAttribute : Attribute, IContractBehavior, IWsdlExportExtension
    {
        private List<RequiredMessagePart> _requiredParameter;

        #region IContractBehavior Members (nothing to be done)

        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
        }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IWsdlExportExtension Members

        /// <summary>
        /// When ExportContract is called to generate the necessary metadata, we inspect the service
        /// contract and build a list of parameters that we'll need to adjust the XSD for later.
        /// </summary>
        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
            _requiredParameter = new List<RequiredMessagePart>();

            foreach (var operation in context.Contract.Operations)
            {
                var inputMessage = operation.Messages.Where(m => m.Direction == MessageDirection.Input).First();
                var parameters = operation.SyncMethod.GetParameters();
                Debug.Assert(parameters.Length == inputMessage.Body.Parts.Count);

                for (int i = 0; i < parameters.Length; i++)
                {
                    object[] attributes = parameters[i].GetCustomAttributes(typeof(OptionalAttribute), false);
                    if (attributes.Length == 0)
                    {
                        // The parameter has no [Optional] attribute, add it to the list of parameters
                        // that we need to adjust the XML schema for later on.
                        _requiredParameter.Add(new RequiredMessagePart()
                        {
                            Namespace = inputMessage.Body.Parts[i].Namespace,
                            Message = operation.Name,
                            Name = inputMessage.Body.Parts[i].Name
                        });
                    }
                }
            }
        }

        /// <summary>
        /// When ExportEndpoint is called, the XML schemas have been generated. Now we can manipulate to
        /// our heart's content.
        /// </summary>
        void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            if (_requiredParameter == null)
            {
                // If we have defined two endpoints implementing the same contract within the same service,
                // this method will be called twice. We only need to modify the schema once however.
                return;
            }

            foreach (var p in _requiredParameter)
            {
                var schemas = exporter.GeneratedXmlSchemas.Schemas(p.Namespace);

                foreach (XmlSchema schema in schemas)
                {
                    var message = (XmlSchemaElement)schema.Elements[p.XmlQualifiedName];
                    var complexType = message.ElementSchemaType as XmlSchemaComplexType;
                    Debug.Assert(complexType != null, "Expected input message to be complex type.");
                    var sequence = complexType.Particle as XmlSchemaSequence;
                    Debug.Assert(sequence != null, "Expected a sequence.");

                    foreach (XmlSchemaElement item in sequence.Items)
                    {
                        if (item.Name == p.Name)
                        {
                            item.MinOccurs = 1;
                            item.MinOccursString = "1";
                            break;
                        }
                    }
                }
            }

            // Throw away the temporary list we generated
            _requiredParameter = null;
        }

        #endregion

        #region Nested types

        private class RequiredMessagePart
        {
            public string Namespace { get; set; }
            public string Message { get; set; }
            public string Name { get; set; }

            public XmlQualifiedName XmlQualifiedName
            {
                get
                {
                    return new XmlQualifiedName(Message, Namespace);
                }
            }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class OptionalAttribute : Attribute
    {
    }
}