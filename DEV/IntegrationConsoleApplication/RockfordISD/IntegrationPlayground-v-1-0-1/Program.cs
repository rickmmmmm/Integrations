using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTasks;
using Model;
using DataAccess;
using Services;
using System.Configuration;

namespace IntegrationPlayground_v_1_0_1
{
    class Program
    {

        private static IRepository _repo;
        //private List<PurchaseOrderFile> _returnFile;
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ReadOption(args);
                Environment.Exit(0);
            }

            Console.WriteLine("Welcome to Hayes Integration Console Application. Please select from the below options:");
            Console.WriteLine("Shall we play a game? (Y)es (N)o");
            string gameplay = Console.ReadLine().ToLower();

            if (gameplay == "n")
            {
                Environment.Exit(0);
            }

            Console.WriteLine("What kind of integration are you looking to do? (P)urchase Order, (M)obile Device Management, (Q)uit");

            ReadOption();

            //Remove "bad" data
            //log actions to console
            //Map file import objects to model objects
            //Submit changes to db
                //upsert to Db
            //Send notifications
            //Dispose of remaining objects
        }

        private static void ReadOption(string[] args)
        {
            string choice = args[0];

            switch (choice)
            {
                case "-p":
                    PurchaseOrderMenu(args);
                    break;
                case "-m":
                    Console.WriteLine("Mobile Device Management not implemented yet.");
                    Console.ReadLine();
                    break;
                default:
                    break;
            }
        }

        public static void ReadOption()
        {
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "p":
                    string[] options = GetOptions();
                    PurchaseOrderMenu(options);
                    break;
                case "m":
                    MobileDeviceManagementMenu();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                case "quit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Not a recognized option. Please select a valid option");
                    ReadOption();
                    break;
            }
        }

        public static string[] GetOptions()
        {
            string[] options = new string[10];

            Console.WriteLine("Would you like to add items to the TIPWEB-IT Catalog from this file? (Y)es (N)o");
            string response = Console.ReadLine().ToLower();

            switch(response)
            {
                case "y":
                    options[2] = "--add-items";
                    break;
                case "n":
                    options[2] = "";
                    break;
                default:
                    break;
            }

            Console.WriteLine("Would you like to add vendors to the TIPWEB-IT Vendor list from file? (Y)es (N)o");
            response = Console.ReadLine().ToLower();

            switch (response)
            {
                case "y":
                    options[3] = "--add-vendors";
                    break;
                case "n":
                    break;
                default:
                    break;
            }

            Console.WriteLine("Would you like to add funding sources to the TIPWEB-IT Vendor list from file? (Y)es (N)o");
            response = Console.ReadLine().ToLower();

            switch (response)
            {
                case "y":
                    options[4] = "--add-funding";
                    break;
                case "n":
                    options[4] = "";
                    break;
                default:
                    break;
            }

            return options;        
        }

        public static void MobileDeviceManagementMenu()
        {
            throw new NotImplementedException();
        }

        public static void PurchaseOrderMenu(string[] options)
        {
            Console.WriteLine("Paste Import File Name below:");
            string file = string.IsNullOrEmpty(options[1]) ? Console.ReadLine() : options[1] ;
            FileTasks ft = new FileTasks();
            Repository rep = new Repository();

            _repo = rep;

            DataIntegrity di = new DataIntegrity(_repo);
            Logging log = new Logging(_repo);
            PurchaseOrderMapping map = new PurchaseOrderMapping(_repo);

            di.Reject += OnRejecting;
            di.Action += OnDataIntegrityAction;

            _repo.Action += OnAction;
            _repo.Error += OnError;

            if (file == "quit")
            {
                ReadOption();
            }
            //create necessary objects
            
            else if(!ft.checkFile(file))
            {
                Console.WriteLine("File does not exist. Please provide a valid file url.");
                PurchaseOrderMenu(options);
            }
            else
            {
                try
                {
                    var fileData = ft.convertCsvFileToObject(file);

                    fileData = di.removeBadElements(fileData);

                    if (options[3] == "--add-vendors")
                    {
                        var vendors = fileData.GroupBy(u => u.VendorName);

                        foreach (var item in vendors)
                        {
                            if (di.vendorNotFound(item.Key))
                            {
                                _repo.addVendor(item.Key);
                            }
                        }
                    }

                    if (options[2] == "--add-items")
                    {
                        foreach (var item in fileData)
                        {
                            if (di.productNotFound(item.ProductName))
                            {
                                var rand = new Random(1897);

                                Item itemToAdd = new Item
                                {
                                    ItemNumber = "H" + rand.Next().ToString(),
                                    ItemName = item.ProductName,
                                    ItemDescription = item.Description.Replace("'",""),
                                    ItemType = 1,
                                    ModelNumber = "",
                                    ManufacturerUID = _repo.getManufacturerUIDFromName(item.Manufacturer),
                                    ItemSuggestedPrice = item.PurchasePrice,
                                    AreaUID = 0,
                                    ItemNotes = item.Description.Replace("'",""),
                                    SKU = "",
                                    SerialRequired = false,
                                    ProjectedLife = 0,
                                    Active = true,
                                    CreatedByUserId = 0,
                                    CreatedDate = DateTime.Now,
                                    LastModifiedByUserID = 0,
                                    LastModifiedDate = DateTime.Now,
                                    AllowUntagged = true
                                };

                                _repo.addItems(itemToAdd);
                            }
                        }
                    }

                    if (options[4] == "--add-funding")
                    {
                        var fundingSources = fileData.GroupBy(u => u.FundingSource);

                        foreach (var source in fundingSources)
                        {
                            if (di.missingFundingSource(source.Key))
                            {
                                _repo.addFundingSource(source.Key);
                            }
                        }
                    }

                    var outData = new List<PurchaseOrderFile>();

                    foreach (var item in fileData)
                    {
                        if (!di.siteNotFound(item))
                        {
                            continue;
                        }

                        if (!di.productNotFound(item))
                        {
                            continue;
                        }

                        if (!di.modelNotFound(item)) //need to add option for adding model numbers
                        {
                            continue;
                        }

                        if (!di.vendorNotFound(item)) //need to add option for adding vendors
                        {
                            continue;
                        }

                        if (!di.purchaseDateMissingOrInvalid(item))
                        {
                            continue;
                        }

                        if (di.invalidLineNumber(item))
                        {
                            continue;
                        }

                        if (di.missingFundingSource(item))
                        {
                            continue;
                        }

                        outData.Add(item);
                    }

                    if (outData.Count > 0)
                    {
                        var mappedItems = map.mapPurchaseOrderHeaders(outData);

                        _repo.addOrderHeaders(mappedItems);
                        _repo.addShipmentInfo();
                        Console.WriteLine("Completed. Where would you like the rejected order file stored? Enter file name below:");
                        string rejectFile = string.IsNullOrEmpty(options[2]) ? Console.ReadLine() : options[2];

                        var rejects = _repo.getRejectionsFromLastImport();

                        ft.createRejectFile(rejectFile, rejects, fileData);
                        _repo.completeIntegration();

                        _repo.logAction("Completed.", "Process completed successfully. Press Any Key to Continue...");

                        string body = string.Format("<h1>Hayes Software</h1><h4>Automatic Notifications</h4><br /> <p>Integration process successful.<br /> {0} Records uploaded from file.<br />{1} Records accepted.<br />{2} Records rejected.",fileData.Count.ToString(), outData.Count.ToString(), rejects.Count.ToString());

                        ISender mailer = new ElasticMailService();
                        IMessage notification = new EmailMessage
                                                                {
                                                                    Body = body,
                                                                    Receivers = ConfigurationManager.AppSettings["notificationSentTo"].Split(',').ToList(),
                                                                    Sender = ConfigurationManager.AppSettings["notificationFrom"],
                                                                    Subject = "Automatic Notification from Hayes Software Systems",
                                                                    SentDate = DateTime.Now
                                                                };

                        mailer.send(notification);

                        Console.WriteLine("Integration Completed. Press Any Key To Exit Application...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No valid data uploaded. Please fix issues in file and re-upload.");
                        Console.WriteLine("Press Any Key To Continue...");
                        Console.ReadLine();
                    }
                }

                catch (Exception e)
                {

                    Console.WriteLine("An error occurred while parsing file " + file + " to .NET object. Error Message:" + e.Message);
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadLine();
                }
            }

        }


        static void OnAction(object sender, DbActivityEventArgs args)
        {
            _repo.logAction(args.ActivityStep, args.ActivityMessage);
        }
        static void OnRejecting(object sender, SystemTasks.ErrorEventArgs args)
        {
            _repo.logRejectRecord(new RejectedRecord { orderNumber = args.Data.Reference,
                                                        rejectReason = args.Data.Reason,
                                                        rejectValue = args.Data.RejectedValue,
                                                        exceptionMessage = args.Data.ExceptionMessage,
                                                        LineNumber = args.Data.LineNumber });
        }

        static void OnError(object sender, DbErrorEventArgs args)
        {
            _repo.logError(args.InterfaceMessage, args.ExceptionMessage);
        }

        static void OnDataIntegrityAction(object sender, SystemTasks.ErrorEventArgs args)
        {
            _repo.logAction(args.actionName, args.message);
        }
    }
}
