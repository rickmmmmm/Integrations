using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTasks;
using Model;
using DataAccess;

namespace IntegrationPlayground_v_1_0_1
{
    class Program
    {

        private static IRepository _repo;
        private List<RejectedRecord> _rejections;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hayes Integration Console Application. Please select from the below options:");
            Console.WriteLine("What kind of integration are you looking to do? (P)urchase Order, (M)obile Device Management");

            ReadOption();

            //Remove "bad" data
            //log actions to console
            //Map file import objects to model objects
            //Submit changes to db
                //upsert to Db
            //Send notifications
            //Dispose of remaining objects
        }

        public static void ReadOption()
        {
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "p":
                    List<string> options = GetOptions();
                    PurchaseOrderMenu(options);
                    break;
                case "m":
                    MobileDeviceManagementMenu();
                    break;
                default:
                    Console.WriteLine("Not a recognized option. Please select a valid option");
                    ReadOption();
                    break;
            }
        }

        public static List<string> GetOptions()
        {
            List<string> options = new List<string>();

            Console.WriteLine("Would you like to add items to the TIPWEB-IT Catalog from this file? (Y)es (N)o");
            string response = Console.ReadLine().ToLower();

            switch(response)
            {
                case "y":
                    options.Add("AddItems");
                    break;
                case "n":
                    break;
                default:
                    break;
            }

            Console.WriteLine("Would you like to add vendors to the TIPWEB-IT Vendor list from file? (Y)es (N)o");
            response = Console.ReadLine().ToLower();

            switch (response)
            {
                case "y":
                    options.Add("AddVendors");
                    break;
                case "n":
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

        public static void PurchaseOrderMenu(List<string> options)
        {
            Console.WriteLine("Paste Import File Name below:");
            string file = Console.ReadLine();
            FileTasks ft = new FileTasks();
            Repository rep = new Repository();
            _repo = rep;

            DataIntegrity di = new DataIntegrity(_repo);
            Logging log = new Logging(_repo);
            PurchaseOrderMapping map = new PurchaseOrderMapping(_repo);

            di.Action += OnAction;
            di.Reject += OnRejecting;
            di.Error += OnError;

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
                    var fileData = ft.convertCsvFileToObject(file, FileTasks.ImportType.PurchaseOrder);

                    fileData = di.removeBadElements(fileData);

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

                        if (!di.invalidLineNumber(item))
                        {
                            continue;
                        }

                        if (!di.missingFundingSource(item))
                        {
                            continue;
                        }

                        outData.Add(item);
                    }

                    if (outData.Count > 0)
                    {
                        var mappedItems = map.mapPurchaseOrderHeaders(outData);
                        //rep.addOrderHeaders(mappedItems);
                        foreach (var item in mappedItems)
                        {
                            Console.WriteLine(item.PurchaseOrderNumber);
                        }
                        Console.ReadLine();

                        _repo.addOrderHeaders(mappedItems);
                    }
                    else
                    {
                        Console.WriteLine("No valid data uploaded. Please fix issues in file and re-upload.");
                    }
                }

                catch (Exception e)
                {

                    Console.WriteLine("An error occurred while parsing file " + file + " to .NET object. Error Message:" + e.Message);
                }
            }

        }


        static void OnAction(object sender, SystemTasks.ErrorEventArgs args)
        {
            //Logging log = new Logging(_repo);
            Console.WriteLine("Here.");
            //log.log(args.message, args.actionName, Logging.ChangeType.Activity);
        }
        static void OnRejecting(object sender, SystemTasks.ErrorEventArgs args)
        {
            _repo.logRejectRecord(new RejectedRecord { orderNumber = args.Data.Reference,
                                                        rejectReason = args.Data.Reason,
                                                        rejectValue = args.Data.RejectedValue,
                                                        exceptionMessage = args.Data.ExceptionMessage });
        }

        static void OnError(object sender, SystemTasks.ErrorEventArgs args)
        {
            Logging log = new Logging(_repo);

            log.log(args.message, args.actionName, Logging.ChangeType.Error);
        }
    }
}
