#Miscellaneous Notes

##Generating the Script 10.6 classes

Run the following script from a Visual Studio command prompt to generate classes for working with the NCPDP data structures.
The 'e:' argument will limit the classes to the RxChangeResponse and any support classes it needs.

Xsd "SCRIPT_XML_10_6.xsd" /c /n:"NCPDP.org.schema.SCRIPT" /e:"RxChangeResponse"