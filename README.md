# Reference Visual Studio C# implementation for an integration partner feedback loop endpoint.
CoverMyMeds offers many options for integration with other systems in order to provide a seamless user experience. Often users will create and edit requests within the CoverMyMeds application, but have a 'dashboard' within another system. CMM can offer a feedback loop to send updates for prior authorization requests back to the integration partner over a SOAP or similar web interface. 

This basic reference implementation is a Visual Studio 2010 solution with a WCF 4.0 service as a SOAP based Integration Partner Endpoint to receive updates and a simple console app to send over sample data. Information from the PA request is encapsulated in an NCPDP Script 10.6 RxChangeRequest and packaged alon with information considering the status of the request with CMM.

## Important Notes
This code is provided only for reference. Any production implementation would need to include provisions for securing communications between CoverMyMeds and the integration partner.
## Visual Studio 2010 Solution Project Breakdown
### IntegrationPartnerEndpoint
<<<<<<< HEAD
A simple WCF 4.0 service exposes a data contract that contains fields and enumerations for describing the status of a prior authorization request within CoverMyMeds. Also included is a data contract object based on the NCPDP.org Script 10.6 schema. Using the RxChangeRequest definition, information contained in the Prior Authorization Request is included in the SOAP service definition set by the WCF project.
=======
A simple WCF 4.0 service exposing a data contract that contains fields and enumerations for describing the status of a prior authorization request within CoverMyMeds. Also included is a data contract based on the NCPDP.org Script 10.6 schema. Using the RxChangeRequest definition, information contained in the PA Request is included in the SOAP service definition set by the WCF project.
>>>>>>> 62f9b8ff81eda71f679b666f39b37ff1895f45ed

The service defines a single operation `UpdatePriorAuthorizationRequest` for accepting the `FeedbackUpdate` data object. New requests within CoverMyMeds would trigger sending the same format data object as updates to existing PA requests. Typically to prevent difficulties with maintaining a historical listing of the request updates in the integration partner system that would require extra work in ensuring that feedback updates have arrived in the proper sequence, the update is sent as a simple snapshot of the request at the moment the message is built for submission. The `FeedbackUpdate.UpdateDateField` will have a datetime value that can be used to determine which message is the most recent and therefore has the most up-to-date information.

For this reference, any incoming FeedbackUpdates are serialized to XML and logged to the Application Event Log of the Windows machine the solution is being debugged on. An Integration Partner would take the data sent in to the WCF service in the `UpdatePriorAuthorizationRequest` function interface implementation in the `IFeedbackEndpoint.svc.cs` file and store the data appropriately for their system users to view.

Typical integrations include establishing Single Sign On connectivity between CoverMyMeds and the integration partner system. The feedback update contains a URL for direct linking to a view of the Prior Authorization Request. Using this URL via an SSO launch from a PA Request dashboard into CoverMyMeds provides the integration partner users with seamless system functionality.
### Script106
A C# class project used by the WCF service to create the RxChangeRequest data object for storing relevant data from a prior authorization request. Using the Visual Studio command prompt tool `XSD.exe` on the NCPDP.org SCRIPT 10.6 schema generates a class file with all the appropriate serializable data elements needed.
### TestClient
<<<<<<< HEAD
This project is intended to serve as a test harness for creating the Integration Partner feedback loop endpoint. When finalizing development and production CoverMyMeds performs this piece of the integration project . The test client console application includes a Project Service Reference to the IntegrationPartnerEndpoint WCF project. Using the data structure provided by the WCF interface, a sample PA Request update is packaged and sent to the WCF service project (hosted by VS2010 built-in webserver). 
=======
This project is intended to serve as a test harness for creating the Integration Partner feedback loop endpoint. When finalizing development and production CoverMyMeds performs this piece of the integration project . The test client console application includes a Project Service Reference to the IntegrationPartnerEndpoint WCF project. Using the data structure provided by the WCF interface, a sample PA Request update is packaged and sent to the WCF service project (hosted by VS2010 built-in webserver). 
>>>>>>> 62f9b8ff81eda71f679b666f39b37ff1895f45ed
