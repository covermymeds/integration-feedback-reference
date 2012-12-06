using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoverMyMeds.Feedback.Utility;

namespace CoverMyMeds.Feedback.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Establishing Feedback Loop Endpoint
            Console.WriteLine("Beginning Client Test");
            try
            {
                IntegrationPartner.FeedbackServiceClient IPFeedbackService = new IntegrationPartner.FeedbackServiceClient();

                // Creating a SOAP inspector to see the XML as it is sent out and attaching it to the endpoint
                WcfSoapInspector wcfOut = new WcfSoapInspector();
                IPFeedbackService.Endpoint.Behaviors.Add(wcfOut);

                IPFeedbackService.UpdatePriorAuthorizationRequest(GetSampleFeedbackUpdate());

                Console.WriteLine(wcfOut.sentMessages[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                //throw;
            }
            Console.WriteLine("Test Complete");
            Console.ReadLine();
        }

        private static IntegrationPartner.FeedbackUpdate GetSampleFeedbackUpdate()
        {
            IntegrationPartner.FeedbackUpdate PARequest = new IntegrationPartner.FeedbackUpdate();

            PARequest.Id = "A12B34";
            PARequest.ReturnURL = "https://www.covermymeds.com/request/view/";
            PARequest.RequestSource = IntegrationPartner.RequestSourceType.Prescriber;
            PARequest.CreatedBy = "Test CMM Presciber User";
            PARequest.CreationDate = new DateTime(2012, 11, 29, 13, 15, 17);
            PARequest.UpdateDate = DateTime.Now;
            PARequest.AppealExpirationDate = null;
            PARequest.FormName = "Fancy Test Form";
            PARequest.Recipients = GetSampleRecipientList();
            PARequest.Disposition = IntegrationPartner.DispositionType.New;
            PARequest.PlanOutcome = IntegrationPartner.PlanOutcomeType.Unknown;
            PARequest.PARequestData = GetSampleRequestFeedbackData();

            return PARequest;
        }

        private static IntegrationPartner.RecipientType[] GetSampleRecipientList()
        {
            List<IntegrationPartner.RecipientType> lsRet = new List<IntegrationPartner.RecipientType>();

            // User with active access to the PA request
            lsRet.Add(new IntegrationPartner.RecipientType()
            {
                Identifier = "UserXYZ123",
                RecipientClass = IntegrationPartner.RecipientClassType.User,
                PresentOnDashboard = true
            });
            // A group that a user with membership in will have access to a PA request
            lsRet.Add(new IntegrationPartner.RecipientType()
            {
                Identifier = "GroupID456",
                RecipientClass = IntegrationPartner.RecipientClassType.Group,
                PresentOnDashboard = true
            });
            // A user who no longer wishes to see or be alerted about the PA. Archived request
            lsRet.Add(new IntegrationPartner.RecipientType()
            {
                Identifier = "UserXYZ123",
                RecipientClass = IntegrationPartner.RecipientClassType.User,
                PresentOnDashboard = false
            });
            return lsRet.ToArray();
        }

        private static IntegrationPartner.RxChangeRequest GetSampleRequestFeedbackData()
        {
            IntegrationPartner.RxChangeRequest rxRet = new IntegrationPartner.RxChangeRequest();
            rxRet.patientField = GetSamplePatientData();
            rxRet.prescriberField = GetSamplePrescriberData();
            rxRet.pharmacyField = GetSamplePharmacyData();
            rxRet.medicationPrescribedField = GetSampleMedicationData();

            return rxRet;
        }

        #region RxChange Message Sample Components

        private static IntegrationPartner.PatientType GetSamplePatientData()
        {
            IntegrationPartner.PatientType PatientData = new IntegrationPartner.PatientType();

            PatientData.nameField = new IntegrationPartner.MandatoryPatientNameType()
            {
                firstNameField = "Test",
                middleNameField = "X",
                lastNameField = "Patient",
                prefixField = "Mr."
            };

            PatientData.dateOfBirthField = new IntegrationPartner.DateType()
            {
                itemElementNameField = IntegrationPartner.ItemChoiceType.Date,
                itemField = new DateTime(1980, 1, 1)
            };

            PatientData.genderField = "M";

            // Various Communication Numbers BN|CP|FX|HP|NP|TE|WP|EM
            List<IntegrationPartner.CommunicationType> PatientNumbers = new List<IntegrationPartner.CommunicationType>();
            // Adding a Patient Work Phone
            PatientNumbers.Add(new IntegrationPartner.CommunicationType()
            {
                numberField = "9875551234",
                qualifierField = "WP"
            });
            // Adding a Patient Home Phone
            PatientNumbers.Add(new IntegrationPartner.CommunicationType()
            {
                numberField = "9875554321",
                qualifierField = "HP"
            });
            PatientData.communicationNumbersField = PatientNumbers.ToArray();

            PatientData.addressField = new IntegrationPartner.AddressType()
            {
                addressLine1Field = "123 Strong Street",
                addressLine2Field = "Apt. 1010",
                cityField = "Springfield",
                stateField = "MN",
                zipCodeField = "12345",
                placeLocationQualifierField = "" // What should this be?
            };

            // Adding two Identifiers here, one SSN, one Patient Account Number
            IntegrationPartner.PatientIDType PatientIdentifiers = new IntegrationPartner.PatientIDType();
            PatientIdentifiers.itemsElementNameField = new IntegrationPartner.ItemsChoiceType1[2];
            PatientIdentifiers.itemsField = new string[2];
            PatientIdentifiers.itemsElementNameField[0] = IntegrationPartner.ItemsChoiceType1.SocialSecurity;
            PatientIdentifiers.itemsField[0] = "123456789";
            PatientIdentifiers.itemsElementNameField[1] = IntegrationPartner.ItemsChoiceType1.PatientAccountNumber;
            PatientIdentifiers.itemsField[1] = "3Z123456";
            PatientData.identificationField = PatientIdentifiers;

            return PatientData;
        }

        private static IntegrationPartner.PrescriberType GetSamplePrescriberData()
        {
            IntegrationPartner.PrescriberType PrescriberData = new IntegrationPartner.PrescriberType();

            // Prescriber Identifiers, an NPI and a DEA number here
            IntegrationPartner.MandatoryProviderIDType PrescriberIdentifiers = new IntegrationPartner.MandatoryProviderIDType();
            PrescriberIdentifiers.itemsElementNameField = new IntegrationPartner.ItemsChoiceType[2];
            PrescriberIdentifiers.itemsField = new string[2];
            PrescriberIdentifiers.itemsElementNameField[0] = IntegrationPartner.ItemsChoiceType.NPI;
            PrescriberIdentifiers.itemsField[0] = "1212121212";
            PrescriberIdentifiers.itemsElementNameField[1] = IntegrationPartner.ItemsChoiceType.DEANumber;
            PrescriberIdentifiers.itemsField[1] = "35123456";
            PrescriberData.identificationField = PrescriberIdentifiers;

            PrescriberData.nameField = new IntegrationPartner.MandatoryNameType()
            {
                firstNameField = "John",
                middleNameField = "H",
                lastNameField = "Smith",
                suffixField = "MD",
                prefixField = "Dr."
            };

            // Adding a fax and office phone number
            List<IntegrationPartner.CommunicationType> PrescriberContactNumbers = new List<IntegrationPartner.CommunicationType>();
            PrescriberContactNumbers.Add(new IntegrationPartner.CommunicationType()
            {
                qualifierField = "FX",
                numberField = "1235554567"
            });
            PrescriberContactNumbers.Add(new IntegrationPartner.CommunicationType()
            {
                qualifierField = "WP",
                numberField = "9875554321"
            });
            PrescriberData.communicationNumbersField = PrescriberContactNumbers.ToArray();

            PrescriberData.addressField = new IntegrationPartner.AddressType()
            {
                addressLine1Field = "123 Main St.",
                cityField = "Springfield",
                stateField = "OR",
                zipCodeField = "97477"
            };

            PrescriberData.specialtyField = "2085R0001X";
            return PrescriberData;
        }

        private static IntegrationPartner.MandatoryPharmacyType GetSamplePharmacyData()
        {
            IntegrationPartner.MandatoryPharmacyType PharmacyData = new IntegrationPartner.MandatoryPharmacyType();

            PharmacyData.storeNameField = "TestRx";

            List<IntegrationPartner.CommunicationType> PharmacyNumbers = new List<IntegrationPartner.CommunicationType>();
            PharmacyNumbers.Add(new IntegrationPartner.CommunicationType()
            {
                qualifierField = "FX",
                numberField = "3215556789"
            });
            PharmacyData.communicationNumbersField = PharmacyNumbers.ToArray();

            IntegrationPartner.MandatoryProviderIDType PharmacyIdentifiers = new IntegrationPartner.MandatoryProviderIDType();
            PharmacyIdentifiers.itemsElementNameField = new IntegrationPartner.ItemsChoiceType[2];
            PharmacyIdentifiers.itemsField = new string[2];
            PharmacyIdentifiers.itemsElementNameField[0] = IntegrationPartner.ItemsChoiceType.NCPDPID;
            PharmacyIdentifiers.itemsField[0] = "1234567";
            PharmacyIdentifiers.itemsElementNameField[1] = IntegrationPartner.ItemsChoiceType.NPI;
            PharmacyIdentifiers.itemsField[1] = "312345677";
            PharmacyData.identificationField = PharmacyIdentifiers;

            PharmacyData.addressField = new IntegrationPartner.AddressType()
            {
                addressLine1Field = "321 Bart Ave.",
                addressLine2Field = "Suite 145",
                cityField = "Springfield",
                stateField = "OR",
                zipCodeField = "97477"
            };

            PharmacyData.pharmacistField = new IntegrationPartner.NameType()
            {
                firstNameField = "John",
                lastNameField = "Matrix",
                suffixField = "DP"
            };

            return PharmacyData;
        }

        private static IntegrationPartner.RxChangePrescribedMedicationType GetSampleMedicationData()
        {
            //DRU+P:TEMAZEPAM 15MG::::3Ø:::::::AA:C25158:AB:C28253+::3Ø:38:AC:C4848Ø+:TAKE ONE CAPSULE AT BEDTIME AS
            //NEEDED FOR SLEEP+85:2ØØ41ØØ1:1Ø2*ZDS:3Ø:8Ø4++R:Ø+++PATIENT'S INSURANCE
            //REQUIRES A PRIOR AUTHORIZATION'
            IntegrationPartner.RxChangePrescribedMedicationType MedicationData = new IntegrationPartner.RxChangePrescribedMedicationType();

            MedicationData.drugDescriptionField = "TEMAZEPAM 15MG";
            MedicationData.drugCodedField = new IntegrationPartner.DrugCodedType()
            {
                strengthField = "30",
                formSourceCodeField = "AA",
                formCodeField = "C25158",
                strengthSourceCodeField = "AB",
                strengthCodeField = "C28253"
            };
            MedicationData.quantityField = new IntegrationPartner.QuantityType[1];
            MedicationData.quantityField[0] = new IntegrationPartner.QuantityType()
            {
                valueField = "30",
                codeListQualifierField = "38",
                unitSourceCodeField = "AC",
                potencyUnitCodeField = "C48480"
            };

            MedicationData.directionsField = "TAKE ONE CAPSULE AT BEDTIME AS NEEDED FOR SLEEP";
            MedicationData.writtenDateField = new IntegrationPartner.DateType()
            {
                itemElementNameField = IntegrationPartner.ItemChoiceType.DateTime,
                itemField = new DateTime(2012, 11, 29)
            };

            MedicationData.daysSupplyField = "30";

            MedicationData.refillsField = new IntegrationPartner.RxChangePrescribedMedicationTypeRefills[1];
            MedicationData.refillsField[0] = new IntegrationPartner.RxChangePrescribedMedicationTypeRefills()
            {
                qualifierField = "R",
                valueField = "0"
            };

            MedicationData.priorAuthorizationStatusField = "PATIENT'S INSURANCE REQUIRES A PRIOR AUTHORIZATION";
            return MedicationData;
        }
        #endregion
    }
}
