using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
                    //options.Add("AddVendors");
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

                if (options[3] == "--batch")
                {

                    _repo.logAction("Initial File Check", "No file found. Process is stopping.");

                    Environment.Exit(0);
                }

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
                        if (!di.badQuantity(item))
                        {
                            continue;
                        }

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

                        //2 more checks
                        //Check to see if order number has existing detail lines with a 0 line number value.
                        if (di.zeroLineNumber(item))
                        {
                            Console.WriteLine("Has Zero line Item " + item.OrderNumber);
                            continue;
                        }
                        //Check to see if line has existing tags. Return itemUID from that detail and populate value as that.
                        if (di.hasTags(item))
                        {
                            Console.WriteLine("Has Tags already... removing Product Name update...");
                            item.ProductName = _repo.getItemIfHasTags(item.OrderNumber, item.LineNumber);
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
                        List<PurchaseOrderFile> finalRejections = ft.getRejectionRecords(rejects, fileData);
                        _repo.completeIntegration();

                        _repo.logAction("Completed.", "Process completed successfully. Press Any Key to Continue...");

                        string readBody = "<!DOCTYPE html>  <html> <body>     <div>         <h1>Hayes Software Systems</h1>         <h4 style=\"padding-bottom:20px;\">Automatic Notification from Hayes Software Systems</h4>     </div>     <div style=\"margin-left:5%;\">         <p>Data integration successful!</p>         <ul style=\"list-style:none;\">               <li>Records Processed: {0}</li>             <li>Records Accepted: {1}</li>    <li>Records with Warnings: {3}</li>         <li>Records Rejected: {2}</li>         </ul>     </div>     <div style=\"margin-left:3%;\">  <p> Please do not reply to this email.If you have any questions or concerns, please contact Dan Cathcart at dcathcart@hayessoft.com </p>          <p> Have a wonderful day,</p>         <p> The Hayes Software Team </p> </div> </body> </html> ";

                        var recordsProcessed = fileData.Count.ToString();
                        var recordsAccepted = finalRejections.Where(u => u.Accepted == "Accepted").ToList().Count.ToString();
                        var recordsRejected = finalRejections.Where(u => u.Accepted != "Warning" && u.Accepted != "Accepted").ToList().Count.ToString();
                        var recordsWarned = finalRejections.Where(u => u.Accepted == "Warning").ToList().Count.ToString();

                        string body = string.Format(readBody, recordsProcessed, recordsAccepted, recordsRejected, recordsWarned);

                        ISender mailer = new SqlDbMailService(_repo);

                        // Zip file because it is over 5 mgs 
                        string ZipDirectory;
                        string FileName;
                        string ZipPathAndFile;
                        FileName = Path.GetFileName(rejectFile);
                        ZipDirectory = rejectFile.Replace(FileName, "");
                        ZipPathAndFile = ZipDirectory + "GarlandRejectionFile.Zip";
                        ZipDirectory = ZipDirectory + "ZipTemp";


                        // Create Directory if not exists
                        bool exists = System.IO.Directory.Exists(ZipDirectory);

                        if (!exists)
                            System.IO.Directory.CreateDirectory(ZipDirectory);

                        // Remove files if they already eixsts
                        if (File.Exists(ZipDirectory + "\\" + FileName))
                        {
                            File.Delete(ZipDirectory + "\\" + FileName);
                        }

                        if (File.Exists(ZipPathAndFile))
                        {
                            File.Delete(ZipPathAndFile);
                        }

                        // Copy file to Zip Directory
                        File.Copy(rejectFile, ZipDirectory + "\\" + FileName);

                        // Zip All Files in Directory
                        ZipFile.CreateFromDirectory(ZipDirectory, ZipPathAndFile);


                        IMessage notification = new EmailMessage
                        {
                            Body = body,
                            Receivers = ConfigurationManager.AppSettings["notificationSentTo"].Split(',').ToList(),
                            Sender = ConfigurationManager.AppSettings["notificationFrom"],
                            Subject = "Garland ISD Purchase Order Integration",
                            SentDate = DateTime.Now,
                            //fileAttachment = rejectFile
                            fileAttachment = ZipPathAndFile
                        };

                        mailer.send(notification);
                        File.Delete(ZipPathAndFile);

                        try
                        {
                            // Remove All files in ZipTemp Directory
                            DirectoryInfo dir = new DirectoryInfo(ZipDirectory);

                            foreach (FileInfo fi in dir.GetFiles())
                            {
                                fi.Delete();
                            }

                            ft.archiveFile(file);
                            ft.archiveFile(rejectFile);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error cleaning up data file info. Exception Message: " + e.Message);
                        }

                        Console.WriteLine("Integration Completed. Press Any Key To Exit Application...");
                        if (options[3] == "--batch")
                        {
                            Environment.Exit(0);
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No valid data uploaded. Please fix issues in file and re-upload.");
                        Console.WriteLine("Press Any Key To Continue...");
                        if (options[3] == "--batch")
                        {
                            Environment.Exit(0);
                        }
                        Console.ReadLine();
                    }
                }

                catch (Exception e)
                {

                    Console.WriteLine("An error occurred while parsing file " + file + " to .NET object. Error Message:" + e.Message);
                    Console.WriteLine("Press Any Key To Continue...");

                    if(options[3]=="--batch")
                    {
                        Environment.Exit(0);
                    }

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
