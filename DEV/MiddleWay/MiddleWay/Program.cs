using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiddleWay_BLL.Services;
using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Repositories;
using MiddleWay_Controller.Services;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_DTO.ServiceInterfaces.TIPWeb;
using MiddleWay_EDS.Services;
using MiddleWay_Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.Repositories;
using TIPWeb_Controller.Services;

namespace MiddleWay
{
    class Program
    {
        protected static ServiceProvider serviceProvider;

        /// <summary>
        /// This program has 3 required parameters
        ///     -Client "ClientName"
        ///     -ProcessName "ProcessName"
        ///     -Task -Must be one of the following values p (Purchase Order), e (Export File), c (Charges), m (Mobile Device Management), a (Assets)
        /// After these 3 parameters, valid optional parameters are as follows
        ///     MergeProducts for a
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();
                var commands = args.ToList();

                //var inputProcess = new ProcessInput(commands);
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Begining MiddleWay Main Process");

                if (ConfigureServices(services, commands))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Services Configured. Building Service Provider");
                    serviceProvider = services.BuildServiceProvider();

                    RunProcess(commands);
#if DEBUG
                    Console.Write("Press Enter to quit");
                    Console.Read();
#endif
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Error Caught");
                PrintToConsole(Utilities.ParseException(ex));
#if DEBUG
                Console.Write("Press Enter to quit");
                Console.Read();
#endif
            }
            finally
            {
                Environment.Exit(0);
            }

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static bool ConfigureServices(IServiceCollection services, List<string> commands)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Configuring Injection Services");
                //        //setup our DI
                //        var serviceProvider = new ServiceCollection()
                //            .AddLogging()
                //            .AddSingleton<IConfigurationService, ConfigurationService>()
                //            .AddDbContext
                //            .BuildServiceProvider();

                //        //configure console logging
                //        //serviceProvider
                //        //    .GetService<ILoggerFactory>();
                //        //.AddConsole(LogLevel.Debug);

                //        var logger = serviceProvider.GetService<ILoggerFactory>()
                //            .CreateLogger<Program>();
                //        //logger.LogDebug("Starting application");


                //        //logger.LogDebug("All done!");

                if (InjectDataDependencies(services, commands))
                {
                    InjectRepositories(services);
                    InjectServices(services);
                    InjectDependencies(services);
                    return true;
                }
                else
                {
#if DEBUG
                    Console.Write("Press Enter to quit");
                    Console.Read();
#else
                Environment.Exit(0);
#endif
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception caught in ConfigureServices...", ex);
            }
        }

        public static bool InjectDataDependencies(IServiceCollection services, List<string> commands)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AdoConnectionString"].ConnectionString;
                //var tipwebConnectionString = ConfigurationManager.ConnectionStrings["TIPWebConnectionString"].ConnectionString;

                var client = "";// ConfigurationManager.AppSettings["Client"];
                var processName = "";//ConfigurationManager.AppSettings["ProcessName"];

                if (ProcessInput.HasParameter(commands, "Client"))//(!string.IsNullOrEmpty(client))
                {
                    if (ProcessInput.HasParameter(commands, "ProcessName"))//(!string.IsNullOrEmpty(processName))
                    {
                        if (!string.IsNullOrEmpty(connectionString))
                        {
                            client = ProcessInput.ReadParameterValue(commands, "Client");
                            processName = ProcessInput.ReadParameterValue(commands, "ProcessName");

                            //Create and configure the DB Context to use
                            services.AddTransient<IClientConfiguration>(c => new ClientConfiguration(client, processName));
                            services.AddDbContext<IntegrationMiddleWayContext>(options => options.UseSqlServer(connectionString));

                            //var configurationService = serviceProvider.GetService<IConfigurationService>();
                            //if (configurationService.HasConfiguration)
                            //{
                            //    //Get the TIPWebConnectionString
                            //    var tipwebConnectionString = configurationService.TIPWebConnection;

                            //    if (!string.IsNullOrEmpty(tipwebConnectionString))
                            //    {
                            //        //services.Configure<DatabaseConfigurationOptions>(connectionString);
                            //        services.AddDbContext<TIPWebContext>(options => options.UseSqlServer(tipwebConnectionString));
                            //services.AddDbContext<TIPWebContext>();
                            services.AddSingleton<IDataProviderFactory, DataProviderFactory>();  // AddSingleton Works but is too broad

                            services.AddSingleton<ExternalDataSourceService>(); //TODO: Figure out how to configure the external data source and inject

                            return true;
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - The TIPWeb database connection is Not configured");
                            //        //throw new ArgumentNullException("The TIPWeb database connection is Not configured");
                            //        return false;
                            //    }

                            //}
                            //else
                            //{
                            //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Configuration not setup for Client \"" + client + "\" and Process Name \"" + processName + "\"");
                            //    return false;
                            //}
                        }
                        else
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - The Integration database connection is Not configured");
                            //throw new ArgumentNullException("The Integration database connection is Not configured");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Process Name value is not configured");
                        //throw new ArgumentNullException("Process Name value is not configured");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Client value is not configured");
                    //throw new ArgumentNullException("Client value is not configured");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - An exception occurred Injecting Data Dependencies");
                Console.WriteLine(Utilities.ParseException(ex));
                //throw new Exception("Exception caught in InjectDataDependencies...", ex);
                return false;
            }
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            try
            {
                //MiddleWay_Controller Repositories
                services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
                services.AddScoped<IEtlInventoryRepository, EtlInventoryRepository>();
                services.AddScoped<IMappingsRepository, MappingsRepository>();
                services.AddScoped<IProcessesRepository, ProcessesRepository>();
                services.AddScoped<IProcessTaskErrorsRepository, ProcessTaskErrorsRepository>();
                services.AddScoped<IProcessTasksRepository, ProcessTasksRepository>();
                services.AddScoped<IProcessTaskStepsRepository, ProcessTaskStepsRepository>();
                services.AddScoped<ITransformationLookupRepository, TransformationLookupRepository>();
                services.AddScoped<ITransformationsRepository, TransformationsRepository>();
                services.AddScoped<IInventoryFlatDataRepository, InventoryFlatDataRepository>();
                //services.AddScoped<I Repository, Repository>();

                //TIPWeb Repositories
                services.AddScoped<IChargePaymentsRepository, ChargePaymentsRepository>();
                services.AddScoped<IChargesRepository, ChargesRepository>();
                services.AddScoped<IEmailRepository, EmailRepository>();
                services.AddScoped<IInventoryRepository, InventoryRepository>();
                services.AddScoped<IPurchasesRepository, PurchasesRepository>();
                //services.AddScoped<I Repository, Repository>();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception caught in InjectRepositories...", ex);
            }
        }

        public static void InjectServices(IServiceCollection services)
        {
            try
            {
                //MiddleWay_Controller Services
                services.AddScoped<IConfigurationService, ConfigurationService>();
                services.AddScoped<IEtlInventoryService, EtlInventoryService>();
                services.AddScoped<IInventoryFlatDataService, InventoryFlatDataService>();
                services.AddScoped<IMappingsService, MappingsService>();
                services.AddScoped<IProcessesService, ProcessesService>();
                services.AddScoped<IProcessTaskErrorsService, ProcessTaskErrorsService>();
                services.AddScoped<IProcessTasksService, ProcessTasksService>();
                services.AddScoped<IProcessTaskStepsService, ProcessTaskStepsService>();
                services.AddScoped<ITransformationLookupService, TransformationLookupService>();
                services.AddScoped<ITransformationsService, TransformationsService>();
                services.AddScoped<IInventoryFlatDataService, InventoryFlatDataService>();
                //services.AddScoped < I , > ();

                //MiddleWay_BLL Services
                services.AddScoped<IAssetsService, AssetsService>();
                services.AddScoped<IChargePaymentsService, ChargePaymentsService>();
                services.AddScoped<IChargesService, ChargesService>();
                services.AddScoped<IInputService, InputService>();
                services.AddScoped<INotificationsService, NotificationsService>();
                services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
                //services.AddScoped < I , > ();

                //TIPWeb Services
                services.AddScoped<IEmailService, EmailService>();
                services.AddScoped<IElasticMailService, ElasticMailService>();
                services.AddScoped<IInventoryService, InventoryService>();
                //services.AddScoped<Service, Service>();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception caught in InjectServices...", ex);
            }
        }

        public static void InjectDependencies(IServiceCollection services)
        {
            try
            {
                // Add Dependency Tracker for Application Insights
                //services.AddScoped<IDependencyTracker, DependencyTracker>();

                //Add AWS service Injection
                //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
                //services.AddAWSService<IAmazonSimpleEmailService>();
                //services.AddAWSService<IAmazonSimpleNotificationService>();
                //services.AddAWSService<IAmazonS3>();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception caught in InjectDependencies...", ex);
            }
        }

        protected static void RunProcess(List<string> commands)
        {
            //Get configuration service, if no configuration stop processing and log error
            var configurationService = serviceProvider.GetService<IConfigurationService>();
            var clientConfiguration = serviceProvider.GetService<IClientConfiguration>();
            var _processesService = serviceProvider.GetService<IProcessesService>();
            var processSource = ProcessSources.PrintConfiguration;

            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Running Process. Checking Configuration");
                if (!configurationService.HasConfiguration)
                {
                    // Configuration Not Loaded
                    Console.WriteLine($"Configuration Not Loaded or Invalid for Client: {clientConfiguration.Client} and ProcessName: {clientConfiguration.ProcessName}");
                    Environment.Exit(0);
                }
                else
                {
                    //TODO: Perform Cleanup of old data (ProcessTasks, ProcessTaskSteps, ProcessTaskErrors, etc...)
                    //if (commands.Count > 0)
                    //{
                    //ReadParameters(commands);
                    //string choice = args[0];
                    var processUid = _processesService.GetProcessUid();
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Determining Process to Run");
                    //TODO: Add a Break/Separator to Reject notes
                    var options = ProcessInput.ReadOptions(commands);
                    if (options != null && options.Count > 0 && !string.IsNullOrEmpty(options[0]))
                    {
                        switch (options[0])
                        {
                            case "-p":
                                processSource = ProcessSources.PurchaseOrder;
                                break;
                            case "-d":
                                processSource = ProcessSources.PurchaseDetails;
                                break;
                            case "-s":
                                processSource = ProcessSources.PurchaseShipments;
                                break;
                            case "-e":
                                processSource = ProcessSources.ExportFile;
                                break;
                            case "-c":
                                processSource = ProcessSources.Charges;
                                break;
                            case "-m":
                                processSource = ProcessSources.MobileDeviceManagement;
                                break;
                            case "-a":
                                processSource = ProcessSources.Assets;
                                break;
                            case "-pc":
                            default:
                                processSource = ProcessSources.PrintConfiguration;
                                break;
                        }
                    }
                    else
                    {
                        processSource = _processesService.GetProcessSource();
                    }

                    switch (processSource)
                    {
                        case ProcessSources.PurchaseOrder:
                            PurchaseOrderMenu(processUid, commands, processSource);
                            break;
                        case ProcessSources.ExportFile:
                            ExportFileOptions(processUid, commands, processSource);
                            break;
                        case ProcessSources.Charges:
                            ChargesMenu(processUid, commands, processSource);
                            break;
                        case ProcessSources.MobileDeviceManagement:
                            MobileDeviceManagementMenu();
                            break;
                        case ProcessSources.Assets:
                            AssetsMenu(processUid, commands, processSource);
                            break;
                        case ProcessSources.PrintConfiguration:
                            PrintConfiguration();
                            break;
                        default:
                            break;
                    }

                    //Environment.Exit(0);
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Welcome to Hayes Integration Console Application. Please select from the below options:");

                    //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Shall we play a game? (Y)es (N)o");
                    //    string gameplay = Console.ReadLine().ToLower();

                    //    if (gameplay == "n")
                    //    {
                    //        Environment.Exit(0);
                    //    }

                    //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - What kind of integration are you looking to do? (P)urchase Order, (M)obile Device Management, (E)xport, (C)harges, (Q)uit");

                    //    ReadInput();
                    //}
                }
                //Remove "bad" data
                //log actions to console
                //Map file import objects to model objects
                //Submit changes to db
                //upsert to Db
                //Send notifications
                //Dispose of remaining objects
            }
            catch (Exception ex)
            {
                Logging.WriteToLog(Utilities.ParseException(ex), clientConfiguration.Client + " - " + clientConfiguration.ProcessName);
                throw ex;
            }
        }

        //private static void ReadParameters(List<string> commands)
        //{
        //    //string choice = args[0];
        //    var options = ProcessInput.ReadOptions(commands);
        //    //foreach(var option in options)
        //    switch (options[0])
        //    {
        //        case "-p":
        //            PurchaseOrderMenu(options);
        //            break;
        //        case "-e":
        //            ExportFileOptions(options);
        //            break;
        //        case "-c":
        //            ChargesMenu(options);
        //            break;
        //        case "m":
        //            MobileDeviceManagementMenu();
        //            break;
        //        case "-a":
        //            AssetsMenu(commands, "Test"); // commands.GetAllArguments
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public static void ReadInput()
        //{
        //    string choice = Console.ReadLine().ToLower();

        //    List<string> options;

        //    switch (choice)
        //    {
        //        case "p":
        //            options = GetOptions();
        //            PurchaseOrderMenu(options);
        //            break;
        //        case "m":
        //            MobileDeviceManagementMenu();
        //            ReadInput();
        //            break;
        //        case "e":
        //            options = GetExportOptions();
        //            ExportFileOptions(options);
        //            break;
        //        case "c":
        //            options = new List<string>();
        //            ChargesMenu(options);
        //            break;
        //        case "a":
        //            options = new List<string>();
        //            AssetsMenu(options);
        //            break;
        //        case "q":
        //            Environment.Exit(0);
        //            break;
        //        case "quit":
        //            Environment.Exit(0);
        //            break;
        //        default:
        //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Not a recognized option. Please select a valid option. Enter (Q)uit to exit.");
        //            ReadInput();
        //            break;
        //    }
        //}

        //public static List<string> GetOptions()
        //{
        //    //string[] options = new string[10];
        //    var options = new List<string>();

        //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Would you like to add items to the TIPWEB-IT Catalog from this file? (Y)es (N)o");
        //    string response = Console.ReadLine().ToLower();

        //    switch (response)
        //    {
        //        case "y":
        //            //options[2] = "--add-items";
        //            options.Add("--add-items");
        //            break;
        //        //case "n":
        //        //    options[2] = "";
        //        //    break;
        //        default:
        //            break;
        //    }

        //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Would you like to add vendors to the TIPWEB-IT Vendor list from file? (Y)es (N)o");
        //    response = Console.ReadLine().ToLower();

        //    switch (response)
        //    {
        //        case "y":
        //            //options[3] = "--add-vendors";
        //            options.Add("--add-vendors");
        //            break;
        //        //case "n":
        //        //    break;
        //        default:
        //            break;
        //    }

        //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Would you like to add funding sources to the TIPWEB-IT Vendor list from file? (Y)es (N)o");
        //    response = Console.ReadLine().ToLower();

        //    switch (response)
        //    {
        //        case "y":
        //            //options[4] = "--add-funding";
        //            options.Add("--add-funding");
        //            break;
        //        //case "n":
        //        //    options[4] = "";
        //        //    break;
        //        default:
        //            break;
        //    }

        //    return options;
        //}

        private static void ChargesMenu(int processUid, List<string> commands, ProcessSources processSource)
        {
            //Is this an import or export?
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Are you wanting to (i)mport payments, (e)xport charge data, (im)port and export all charge data?");
            //string choice = string.IsNullOrEmpty(args[1]) ? Console.ReadLine().ToLower() : args[1];

            //var chargesService = serviceProvider.GetService<IChargesService>();
            //FileTasks fileTasks = new FileTasks();

            //FileTasks ft = new FileTasks();
            //Repository rep = new Repository();

            //_repo = rep;

            //DataIntegrity di = new DataIntegrity(_repo);
            //ChargesMapping map = new ChargesMapping(_repo);


            //di.Reject += OnRejecting;
            //di.Action += OnDataIntegrityAction;

            //_repo.Action += OnAction;
            //_repo.Error += OnError;


            //if (choice == "i")
            //{
            //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Paste import filename below:");
            //    string importFileName = string.IsNullOrEmpty(args[2]) ? Console.ReadLine() : args[2];

            //    if (!fileTasks.checkFile(importFileName))
            //    {
            //        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - File not valid.");

            //        if (args[3] == "--batch")
            //        {
            //            Environment.Exit(0);
            //        }

            //        ChargesMenu(args);
            //    }
            //    else
            //    {
            //        try
            //        {
            //            chargesService.ProcessCharges(importFileName);
            //            Environment.Exit(0);
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - There was an error processing the import. Exception: ");
            //            Console.WriteLine(e.Message);
            //            //_repo.logError("There was an error processing the import file " + importFileName, e.Message.Replace("'", "''"));

            //            if (args[3] == "--batch")
            //            {
            //                Environment.Exit(0);
            //            }
            //            Console.ReadLine();
            //        }

            //    }

            //    //map incoming file to file model
            //    //run import data integrity
            //    //map file model to data model
            //    //send email confirming import and showing rejects
            //}
            //else if (choice == "e")
            //{
            //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Paste export filename below:");
            //    string exportFileName = string.IsNullOrEmpty(args[2]) ? Console.ReadLine() : args[2];

            //    if (fileTasks.checkFile(exportFileName))
            //    {
            //        fileTasks.archiveFile(exportFileName);
            //    }

            //    //var outData = _repo.exportChargesToInTouch();
            //    //fileTasks.createExportFile(outData, exportFileName);

            //    string bodyMovement = "<!DOCTYPE html>  <html> <body>     <div>         <h1>Hayes Software Systems</h1>         <h4 style=\"padding-bottom:20px;\">Automatic Notification from Hayes Software Systems</h4>     </div>     <div style=\"margin-left:5%;\">         <p>Data export successful!</p>         <ul style=\"list-style:none;\">               <li>Records Processed: {0}</li>       </ul>     </div>     <div style=\"margin-left:3%;\">  <p> Please do not reply to this email.If you have any questions or concerns, please contact Dan Cathcart at dcathcart@hayessoft.com </p>          <p> Have a wonderful day,</p>         <p> The Hayes Software Team </p> </div> </body> </html> ";

            //    //string body = string.Format(bodyMovement, outData.Count);

            //    //SqlDbMailService mailer = new SqlDbMailService(_repo);
            //    var notification = new EmailMessageModel
            //    {
            //        //Body = body,
            //        Recipients = ConfigurationManager.AppSettings["notificationSentTo"].Split(',').ToList(),
            //        Sender = ConfigurationManager.AppSettings["notificationFrom"],
            //        Subject = "Automatic Notification from Hayes Software Systems",
            //        SentDate = DateTime.Now,
            //        FileAttachment = exportFileName
            //    };

            //    //mailer.send(notification);
            //    //_repo.completeIntegration();

            //    if (args[3] == "--batch")
            //    {
            //        Environment.Exit(0);
            //    }

            //}
            //else if (args[3] == "--batch")
            //{
            //    Environment.Exit(0);
            //}
            //else
            //{
            //    Console.WriteLine(choice + " is not a valid choice. Please make a valid selection from the menu...");
            //}

            //If Import what is file name to import?

            //if export, what is file name to export?
            //run query which automatically maps to output type
            //save file with data   
        }

        public static List<string> GetExportOptions()
        {
            var options = new List<string>();
            return options;
        }

        public static void ExportFileOptions(int processUid, List<string> commands, ProcessSources processSource)
        {
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Which export option would you like to do? (R)eceived Tags");
            //string response = string.IsNullOrEmpty(options[1]) ? Console.ReadLine().ToLower() : options[1];

            //switch (response)
            //{
            //    case "r":
            //        ProcessReceivedTagsExport(options);
            //        break;
            //    case "q":
            //        Environment.Exit(0);
            //        break;
            //}
        }

        private static void ProcessReceivedTagsExport(string[] options)
        {
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Paste Export File Name below:");
            //string file = string.IsNullOrEmpty(options[2]) ? Console.ReadLine() : options[2];

            //var inventoryService = serviceProvider.GetService<IInventoryService>();
            //FileTasks fileTasks = new FileTasks();

            //Repository rep = new Repository();

            //_repo = rep;

            //inventoryService.updateFixedAssetIds();
            //List<ReceivedTagsExportFile> results = inventoryService.exportReceivedTags();
            //if (results.Count > 0)
            //{
            //    fileTasks.createExportFile(results, file);
            //}

            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Completed...");
            Environment.Exit(0);

        }

        public static void MobileDeviceManagementMenu()
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Mobile Device Management not implemented yet.");
            //throw new NotImplementedException();
        }

        public static void PurchaseOrderMenu(int processUid, List<string> commands, ProcessSources processSource)
        {
            //create necessary objects

            //FileTasks fileTasks = new FileTasks();
            //Repository rep = new Repository();

            //_repo = rep;

            //DataIntegrity di = new DataIntegrity(_repo);
            //Logging log = new Logging(_repo);
            //PurchaseOrderMapping map = new PurchaseOrderMapping(_repo);

            //di.Reject += OnRejecting;
            //di.Action += OnDataIntegrityAction;

            //_repo.Action += OnAction;
            //_repo.Error += OnError;

            //string file = string.Empty;
            //if (options.Contains("--file"))
            //{
            //    var fileIndex = options.IndexOf("--file") + 1;
            //    file = options[fileIndex];
            //}
            //else
            //{
            //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Paste Import File Name below:");
            //    file = Console.ReadLine();
            //}


            //if (file == "quit")
            //{
            //    ReadInput();
            //}
            //else if (!fileTasks.checkFile(file))
            //{
            //    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - File does not exist. Please provide a valid file url.");
            //    if (options[5] == "--batch")
            //    {
            //        Environment.Exit(0);
            //    }
            //    else
            //    {
            //        PurchaseOrderMenu(options);
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        Read input file into PurchaseOrderFile format
            //        var fileData = fileTasks.convertCsvFileToObject(file);

            //        fileData = di.removeBadElements(fileData);

            //        if (options[3] == "--add-vendors")
            //        {
            //            var vendors = fileData.GroupBy(u => u.VendorName);

            //            foreach (var item in vendors)
            //            {
            //                if (di.vendorNotFound(item.Key))
            //                {
            //                    if (item.Key.Length <= 100)
            //                    {
            //                        _repo.addVendor(item.Key.Replace("'", "''"));
            //                    }
            //                    else
            //                    {

            //                    }
            //                }
            //            }
            //        }

            //        if (options[2] == "--add-items")
            //        {
            //            var rand = new Random();

            //            foreach (var item in fileData)
            //            {
            //                if (di.productNotFound(item.ProductName))
            //                {
            //                    var itemNumber = "H" + _repo.getUniqueItemNumber();

            //                    Item itemToAdd = new Item
            //                    {
            //                        ItemNumber = itemNumber,
            //                        ItemName = item.ProductName.Replace("'", "''"),
            //                        ItemDescription = item.Description.Replace("'", "''"),
            //                        ItemType = 1,
            //                        ModelNumber = "None",
            //                        ManufacturerUid = 0,
            //                        ItemSuggestedPrice = 0,
            //                        AreaUid = 0,
            //                        ItemNotes = item.Description.Replace("'", "''"),
            //                        SKU = "",
            //                        SerialRequired = false,
            //                        ProjectedLife = 0,
            //                        Active = true,
            //                        CreatedByUserId = 0,
            //                        CreatedDate = DateTime.Now,
            //                        LastModifiedByUserID = 0,
            //                        LastModifiedDate = DateTime.Now,
            //                        AllowUntagged = true
            //                    };

            //                    _repo.addItems(itemToAdd);
            //                }
            //            }
            //        }

            //        if (options[4] == "--add-funding")
            //        {
            //            var fundingSources = fileData.GroupBy(u => u.FundingSource);

            //            foreach (var source in fundingSources)
            //            {
            //                if (di.missingFundingSource(source.Key))
            //                {
            //                    _repo.addFundingSource(source.Key);
            //                }
            //            }
            //        }

            //        var outData = new List<PurchaseOrderDto>();

            //        foreach (var item in fileData)
            //        {
            //            if (di.rejectLongRecord(item, true, true, true))
            //            {
            //                continue;
            //            }

            //            if (!di.siteNotFound(item))
            //            {
            //                continue;
            //            }

            //            if (!di.productNotFound(item))
            //            {
            //                continue;
            //            }

            //            if (!di.modelNotFound(item)) //need to add option for adding model numbers
            //            {
            //                continue;
            //            }

            //            if (!di.vendorNotFound(item)) //need to add option for adding vendors
            //            {
            //                continue;
            //            }

            //            if (!di.purchaseDateMissingOrInvalid(item))
            //            {
            //                continue;
            //            }

            //            if (di.invalidLineNumber(item))
            //            {
            //                continue;
            //            }

            //            if (di.missingFundingSource(item))
            //            {
            //                continue;
            //            }

            //            outData.Add(item);
            //        }

            //        if (outData.Count > 0)
            //        {
            //            var mappedItems = map.mapPurchaseOrderHeaders(outData);

            //            _repo.addOrderHeaders(mappedItems);

            //            if (options[6] == "--add-shipments")
            //            {
            //                _repo.addShipmentInfo();
            //            }

            //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Completed. Where would you like the rejected order file stored? Enter file name below:");
            //            string rejectFile = string.IsNullOrEmpty(options[7]) ? Console.ReadLine() : options[7];

            //            var rejects = _repo.getRejectionsFromLastImport();

            //            ft.createRejectFile(rejectFile, rejects, fileData);
            //            _repo.completeIntegration();

            //            _repo.logAction("Completed.", "Process completed successfully. Press Any Key to Continue...");

            //            string readBody = "<!DOCTYPE html>  <html> <body>     <div>         <h1>Hayes Software Systems</h1>         <h4 style=\"padding-bottom:20px;\">Automatic Notification from Hayes Software Systems</h4>     </div>     <div style=\"margin-left:5%;\">         <p>Data integration successful!</p>         <ul style=\"list-style:none;\">               <li>Records Processed: {0}</li>             <li>Records Accepted: {1}</li>             <li>Records Rejected: {2}</li>         </ul>     </div>     <div style=\"margin-left:3%;\">  <p> Please do not reply to this email.If you have any questions or concerns, please contact Dan Cathcart at dcathcart@hayessoft.com </p>          <p> Have a wonderful day,</p>         <p> The Hayes Software Team </p> </div> </body> </html> ";

            //            string body = string.Format(readBody, fileData.Count.ToString(), outData.Count.ToString(), rejects.Count.ToString());

            //            SqlDbMailService mailer = new SqlDbMailService(_repo);
            //            EmailMessage notification = new EmailMessage
            //            {
            //                Body = body,
            //                Receivers = ConfigurationManager.AppSettings["notificationSentTo"].Split(',').ToList(),
            //                Sender = ConfigurationManager.AppSettings["notificationFrom"],
            //                Subject = "Automatic Notification from Hayes Software Systems for College Station ISD",
            //                SentDate = DateTime.Now,
            //                FileAttachment = rejectFile
            //            };

            //            mailer.send(notification);

            //            try
            //            {
            //                ft.archiveFile(file);
            //                ft.archiveFile(rejectFile);
            //            }
            //            catch (Exception e)
            //            {
            //                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Error cleaning up data file info. Exception Message: " + e.Message);
            //            }

            //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Integration Completed. Press Any Key To Exit Application...");

            //            if (options[5] == "--batch")
            //            {
            //                Environment.Exit(0);
            //            }

            //            Console.ReadLine();
            //        }
            //        else
            //        {
            //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - No valid data uploaded. Please fix issues in file and re-upload.");
            //            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Press Any Key To Continue...");
            //            if (options[5] == "--batch")
            //            {
            //                Environment.Exit(0);
            //            }
            //            Console.ReadLine();
            //        }
            //    }

            //    catch (Exception e)
            //    {

            //        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - An error occurred while parsing file " + file + " to .NET object. Error Message:" + e.Message);
            //        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Press Any Key To Continue...");

            //        if (options[5] == "--batch")
            //        {
            //            Environment.Exit(0);
            //        }

            //        Console.ReadLine();
            //    }
            //}
        }

        public static void AssetsMenu(int processUid, List<string> commands, ProcessSources processSource) //List<string> options
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Starting Asset Process");
            var assetsService = serviceProvider.GetService<IAssetsService>();

            assetsService.ProcessAssets(processUid, commands, processSource);
        }

        public static void PrintConfiguration()
        {
            var configurationService = serviceProvider.GetService<IConfigurationService>();

            var config = configurationService.GetAllConfigurations();

            var configText = Utilities.ToStringObject(config);

            Console.WriteLine(configText);
#if DEBUG
            Console.Write("Press Enter to quit");
            Console.Read();
#endif
        }

        public static void PrintToConsole(string message)
        {
            var lines = message.Split(new string[] { "\\r\\n", "\\n" }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        /*
        static void OnAction(object sender, DbActivityEventArgs args)
        {
            _repo.logAction(args.ActivityStep, args.ActivityMessage);
        }
        */
        /*
        static void OnRejecting(object sender, ErrorEventArgs args)
        {
            _repo.logRejectRecord(new RejectedRecord
            {
                orderNumber = args.Data.Reference,
                rejectReason = args.Data.Reason,
                rejectValue = args.Data.RejectedValue,
                exceptionMessage = args.Data.ExceptionMessage,
                LineNumber = args.Data.LineNumber
            });
        }
        */
        /*
        static void OnError(object sender, DbErrorEventArgs args)
        {
            _repo.logError(args.InterfaceMessage, args.ExceptionMessage);
        }
        */
        /*
        static void OnDataIntegrityAction(object sender, ErrorEventArgs args)
        {
            _repo.logAction(args.actionName, args.message);
        }
        */
    }
}
