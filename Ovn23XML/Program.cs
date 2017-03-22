using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ovn23XML
{
    class Program
    {
        static string[] ignore = new string[] { "Contact", "Contacts", "Addresses", "Emails", "PhoneNumbers" };
        static XmlDocument doc;
        static void Main(string[] args)
        {
            doc = new XmlDocument();
            doc.Load(@"C:\Users\Administrator\xmlbugg\Ovn23XML\Contacts.xml");
            XmlNode contacts = doc.LastChild;
            Menu(contacts, doc);
        }

        static void Menu(XmlNode contacts, XmlDocument doc)
        {
            bool run = true;
            string name;
            do
            {
                Console.WriteLine("[A]dd contact");
                Console.WriteLine("[P]rint contacts");
                Console.WriteLine("[F]ind contact");
                Console.WriteLine("[R]emove contact");
                Console.WriteLine("[E]dit contact");
                Console.WriteLine("[Q]uit");

                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "p":
                        PrintContacts(contacts);
                        break;
                    case "a":
                        AddContact(contacts, doc);
                        break;
                    case "r":
                        Console.Write("Input name: ");
                        name = Console.ReadLine();
                        FindNode(contacts.ChildNodes, name, 1);
                        break;
                    case "f":
                        Console.Write("Input name: ");
                        name = Console.ReadLine();
                        FindNode(contacts.ChildNodes, name, 0);
                        break;
                    case "e":
                        EditContact(contacts);
                        break;
                    case "q":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input.");
                        break;
                }
            } while (run);

        }

        private static void EditContact(XmlNode contacts)
        {
            throw new NotImplementedException();
        }

        private static void RemoveContact(XmlNode contacts)
        {

            //nodes[i].ParentNode.RemoveChild(nodes[i]);
        }

        //Add contact to XML-file
        static void AddContact(XmlNode contacts, XmlDocument doc)
        {
            //XmlNode contact = contacts[contacts.Count - 1];
            XmlNode root = contacts;

            XmlElement newContact = doc.CreateElement("Contact");
            Console.Write("Input first name: ");
            XmlElement newFirstName = doc.CreateElement("Firstname");
            XmlText firstName = doc.CreateTextNode(Console.ReadLine());

            Console.Write("Input last name: ");
            XmlElement newLastName = doc.CreateElement("Lastname");
            XmlText lastName = doc.CreateTextNode(Console.ReadLine());

            Console.Write("Input social security number (YYMMDDXXXX): ");
            string ssn = Console.ReadLine();

            newContact.SetAttribute("ssn", ssn);
            newContact.AppendChild(newFirstName);
            newContact.AppendChild(newFirstName);
            newLastName.AppendChild(lastName);
            newFirstName.AppendChild(firstName);

            contacts.InsertAfter(newContact, contacts.LastChild);

            doc.Save(@"C:\Users\Administrator\xmlbugg\Ovn23XML\Contacts.xml");


        }

        static void PrintContacts(XmlNode contacts)
        {
            XmlNodeList contactList = contacts.ChildNodes;
            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine($"\nContact {i + 1} ------------------------------------------------\n");
                if (contactList[i].HasChildNodes)
                {
                    TraverseChildNodes(contactList[i].ChildNodes);
                }
            }
        }

        private static void TraverseChildNodes(XmlNodeList childNodes)
        {
            if (childNodes[0] != null && !childNodes[0].HasChildNodes) //&& childNodes[0].Name != "Contact" && childNodes[0].Name != "Addresses" && childNodes[0].Name != "PhoneNumbers" && childNodes[0].Name != "Emails"
            {
                Console.Write($"{childNodes[0].Name,-20} ");
            }
            foreach (XmlNode node in childNodes)
            {

                if (!node.HasChildNodes)
                {
                    Console.WriteLine(node.InnerText);
                }
                else
                {
                    TraverseChildNodes(node.ChildNodes);
                }
            }
        }

        static void FindNode(XmlNodeList nodes, string name, int type)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.HasChildNodes)
                {
                    FindNode(node.ChildNodes, name, type);
                }
                else if (node.InnerText.ToLower() == name.ToLower())
                {
                    Console.WriteLine(node.Value);
                    if (type == 1)
                    {
                        node.ParentNode.ParentNode.RemoveAll();
                        doc.Save(@"C:\Users\Administrator\xmlbugg\Ovn23XML\Contacts.xml");
                    }
                }
            }
        }
    }
}


