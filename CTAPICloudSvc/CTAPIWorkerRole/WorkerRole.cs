using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.WindowsAzure.ServiceRuntime;
using DelphiCTCombinaiton.Models;
using DelphiCTCombinaiton;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

namespace CTAPIWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        static List<string> deviceIDLst = new List<string>();

        //public static event EventHandler<RoleEnvironmentChangedEventArgs> Changed;

        static System.Timers.Timer DelphiTimer = new System.Timers.Timer();

        static System.Timers.Timer CTTimer = new System.Timers.Timer();

        static System.Timers.Timer WorkTimer = new System.Timers.Timer();


        static string OBUCsvFile;
        static int CTTimmerIntervalMinute;
        static int PostIntervalSec;
        //static string CTBaseEndpoint;
        static string CTUserName;
        static string CTPassword;
        //static string DelphiBaseEndPoint;
        static string DelphiUserName;
        static string DelphiPassword;
        static string DelphiClientGuid;
        static string CsvfileStorageConnectionString;
        static string ContainerName;

        public WorkerRole()
        {
            InitOBUparameters();
        }

        private static void InitOBUparameters()
        {
            CTTimmerIntervalMinute = int.Parse(RoleEnvironment.GetConfigurationSettingValue("CTTimmerIntervalMinute"));
            PostIntervalSec = int.Parse(RoleEnvironment.GetConfigurationSettingValue("PostIntervalSec"));
            //CTBaseEndpoint = RoleEnvironment.GetConfigurationSettingValue("CTBaseEndpoint");
            CTUserName = RoleEnvironment.GetConfigurationSettingValue("CTUserName");
            CTPassword = RoleEnvironment.GetConfigurationSettingValue("CTPassword");
            //DelphiBaseEndPoint = RoleEnvironment.GetConfigurationSettingValue("DelphiBaseEndPoint");
            DelphiUserName = RoleEnvironment.GetConfigurationSettingValue("DelphiUserName");
            DelphiPassword = RoleEnvironment.GetConfigurationSettingValue("DelphiPassword");
            DelphiClientGuid = RoleEnvironment.GetConfigurationSettingValue("DelphiClientGuid");
            CsvfileStorageConnectionString = RoleEnvironment.GetConfigurationSettingValue("CsvfileStorageConnectionString");
            OBUCsvFile = RoleEnvironment.GetConfigurationSettingValue("OBUCsvFile");
            ContainerName = RoleEnvironment.GetConfigurationSettingValue("ContainerName");
            deviceIDLst = GetOBULabelIDList(OBUCsvFile);
        }

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("CTAPIWorkerRole entry point called");

            CTTokenSeeker.CurrentToken = CTAuthTokenHelper.GetToken(CTUserName,CTPassword).Result;

            #region SetUp
            Console.ForegroundColor = ConsoleColor.Green;

            TokenSeeker.CurrentToken = TokenHelper.GetToken(DelphiClientGuid,DelphiUserName,DelphiPassword);
            Trace.WriteLine("Token Created");
            Trace.WriteLine(TokenSeeker.CurrentToken.access_token);

            DelphiTimer.Interval = 1000 * (TokenSeeker.CurrentToken.expires_in - 100);
            DelphiTimer.Elapsed += timer_Elapsed;
            DelphiTimer.Start();
            /*
            VehicleFeature VehicleTest = new VehicleFeature();
            DeviceFeature deviceTest = new DeviceFeature();
            GeoFenceFeature geofenceTest = new GeoFenceFeature();
            CustomerFeature customerTest = new CustomerFeature();
            DTCFeature dtctest = new DTCFeature();
            new HTTPHelper(new HttpItem() { RelatedAddress = @"/api/v1/system", RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" }).HttpHelperMethod().GetAwaiter();

            List<Vehicle> VehilceLst = new List<Vehicle>();
            VehilceLst.Add(new Vehicle() { Id = new Guid("08f3faa3-a308-4051-bbb5-a1bc6b348775") });
            VehilceLst.Add(new Vehicle() { Id = new Guid("0ed17636-9058-4a30-985d-60cbafa62415") });
            VehilceLst.Add(new Vehicle() { Id = new Guid("b265106e-614e-48b1-a8a9-8c67226669d5") });
            VehilceLst.Add(new Vehicle() { Id = new Guid("5958cbaa-3251-4827-b6ca-521937aabfb9") });

            List<Customer> customerList = new List<Customer>();
            */
            #endregion

            /*
            CTTCodeFeature ctcodefeature = new CTTCodeFeature();
            CTTroubleCodeInfo ctcodes = new CTTroubleCodeInfo() { deviceId = "65437321", authToken = CTTokenSeeker.CurrentToken.authToken, reportTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), troubleCodeAll = new CTTroubleCode[] { new CTTroubleCode { troubleCode = "C0074" }, new CTTroubleCode() { troubleCode = "B0005" }, new CTTroubleCode { troubleCode = "P1105" }, new CTTroubleCode { troubleCode = "B2000" } } };
            var asdfj = JsonHelper.ToJson(ctcodes);
            var feedback=ctcodefeature.PostDTC(asdfj);
            Trace.WriteLine(feedback.CTResult);
            */

            //get vehicle guid by device id, if the obu has not been plug-in the vehicle? Online vehicle



            /*
            /api/v1/vehicles/fc4e9855-0775-4354-b9c8-dd5e0bf97537/diagnostic-codes
            /api/v1/vehicles/3a68fe82-cb74-4d85-a9cb-c097f4d7720f/diagnostic-codes
            /api/v1/vehicles/ff50c8ec-9aca-4cdf-a743-eef8b0f3483a/diagnostic-codes
            /api/v1/vehicles/db680bce-8ba6-4c01-a21f-10e20202f57e/diagnostic-codes
            /api/v1/vehicles/08f3faa3-a308-4051-bbb5-a1bc6b348775/diagnostic-codes
            /api/v1/vehicles/b3f9b64c-2059-47ef-8438-56261f67fdcb/diagnostic-codes
            /api/v1/vehicles/399ad4a9-e0c8-451d-8879-af136c3ef73d/diagnostic-codes
            /api/v1/vehicles/a3bb8a9b-7939-4db2-abca-5b2635b0572b/diagnostic-codes
            /api/v1/vehicles/6d3be116-9adf-42ca-a42e-22adbc90ee48/diagnostic-codes
            /api/v1/vehicles/a2d19187-018d-4fe8-977c-9159a4bc8b8a/diagnostic-codes
            /api/v1/vehicles/608cb684-7404-456d-a46b-8cdf670249f1/diagnostic-codes
            /api/v1/vehicles/3aaeddeb-d57c-448b-9a56-53b14f767650/diagnostic-codes
            /api/v1/vehicles/bdfe3184-70c1-4a28-88bc-d7b23c4a8c63/diagnostic-codes
             */

            while (true)
            {
                CTMsgHelper ctmsg = new CTMsgHelper();
                var devicelst = ctmsg.GetAvailableDeviceList(deviceIDLst);
                if (deviceIDLst != null)
                {
                    var dvcVhicles = ctmsg.GetAvailableVehicleInfoList(devicelst);
                    if (dvcVhicles != null)
                    {
                        CTGpsInfoFeature.PostCTVehicleGPSInfoList(dvcVhicles);
                        CTGANInfoFeature.PostCTVehicleCANInfoList(dvcVhicles);
                        CTTroubleCodeFeature cttcode = new CTTroubleCodeFeature();
                        cttcode.PostCTTroubleCodeInfoList(dvcVhicles);
                    }
                }

            }

            //foreach (var item in dvcVhicles.Values)
            //{
            //    dtctest.GetDTCbyVehicleID(item.Id).GetAwaiter();
            //}

            #region DeviceFeature
#if DeviceFeature
            string address = @"/api/v1/devices?page={0}";
            for (int i = 1; i <= 3; i++)
            {
                var devicesinfo = deviceTest.GetDeviceLstInfo(string.Format(address, i)).Result;
                foreach (var item in devicesinfo)
                {
                    deviceTest.GetDeviceByID(item);
                }
            }

            List<string> deviceIDlst = new List<string>();
            deviceIDlst.Add("1791ea63-4500-4c9e-9899-4a36a728b3fd");
            deviceIDlst.Add("4a8f7668-27e1-4be9-860b-555032546304");
            deviceIDlst.Add("550b06d1-840b-40d1-a4c8-d26bf2d4996a");
            deviceIDlst.Add("55e3beba-335b-4716-8a42-e7c5a09a5526");
            foreach (var item in deviceIDlst)
            {
                deviceTest.GetDevice(DeviceFeature.Device_ID, item).Wait();
                deviceTest.GetDevice(DeviceFeature.Device_Guid, item).Wait();
            }

            List<string> deviceCarrierIDlst = new List<string>();
            deviceCarrierIDlst.Add("80F0578C");
            deviceCarrierIDlst.Add("6C4C72C3");
            deviceCarrierIDlst.Add("6C4C76C1");
            deviceCarrierIDlst.Add("6C4C766E");
            foreach (var item in deviceCarrierIDlst)
            {
                deviceTest.GetDevice(DeviceFeature.Device_CarrID, item).Wait();
            }

            List<string> deviceRegKeylst = new List<string>();
            deviceRegKeylst.Add("336b-ea69");
            deviceRegKeylst.Add("87fa-20d6");
            deviceRegKeylst.Add("af25-f56b");
            deviceRegKeylst.Add("7685-46ec");
            foreach (var item in deviceRegKeylst)
            {
                deviceTest.GetDevice(DeviceFeature.Device_Regkey, item).Wait();
            }

#endif
            #endregion

            #region GeofencesFeature
#if GeofencesFeature
            //string geofenceaddress = @"/api/v1/geo-fences?page={0}";
            //for (int i = 1; i <= 3; i++)
            //{
            //    var geofencelst = geofenceTest.GetDeviceLstInfo(string.Format(geofenceaddress, i)).Result;
            //    foreach (var item in geofencelst)
            //    {
            //        geofenceTest.TestOtherInfo(item);
            //    }
            //}

            List<string> GeofenceIDLst = new List<string>();
            GeofenceIDLst.Add("3a89ac0d-fd80-42db-8219-b7d0c27c2d9e");
            GeofenceIDLst.Add("0fa6cbaf-bd6e-4131-bb4b-890122ae7aeb");
            GeofenceIDLst.Add("4d066aea-a39d-491f-83fc-acc4874a2e0f");
            GeofenceIDLst.Add("a91efe26-c29e-413d-ae57-698a36edf22f");
            GeofenceIDLst.Add("62b1fb75-0daa-4a77-9656-ab9b4182d5f2");

            //foreach (var item in GeofenceIDLst)
            //{
            //    var tmp = geofenceTest.GeofenceByGeoID(item, RequestMethod.Get, null).Result;
            //    Trace.WriteLine(tmp.Radius);
            //    Trace.WriteLine(tmp._links.vehicle.href);

            //    PostOrPutGeofence gf = new PostOrPutGeofence() { Name = tmp.Name, Radius = 123, GeoFenceType = GeoFenceType.CIRCLE.ToString(), Enabled = true, CoordinateType = "WGS", Coordinates = new Location[] { new Location() { Latitude = rd.NextDouble(), Longitude =rd.NextDouble() } } };
            //    string geofencestr = JsonHelper.ToJson(gf);
            //    var updgeo = geofenceTest.GeofenceByGeoID(item, RequestMethod.Put, geofencestr).Result;
            //    if (updgeo != null)
            //        Trace.WriteLine(updgeo.Name);
            //}

            foreach (var item in VehilceLst)
            {
                PostOrPutGeofence gf = new PostOrPutGeofence() { Name = "TstName" + rd.Next(rd.Next(), int.MaxValue), Radius = rd.Next(100, 10000), GeoFenceType = GeoFenceType.CIRCLE.ToString(), Enabled = true, CoordinateType = "WGS", Coordinates = new Location[] { new Location() { Latitude = rd.NextDouble(), Longitude = rd.NextDouble() } } };
                var content = JsonHelper.ToJson(gf);
                var tmpgeofenc = geofenceTest.GeofenceListByVehicleID(item.Id, RequestMethod.Post, content).Result;
#warning:delete Geofence
                //geofenceTest.GeofenceByGeoID(tmpgeofenc.Id,RequestMethod.Delete,null).GetAwaiter();
            }

#endif
            #endregion

            #region vehiclesFeature
#if vehiclesFeature
            string vehicleaddress = @"/api/v1/vehicles?page={0}";
            for (int i = 1; i <= 1; i++)
            {
                VehilceLst = VehicleTest.GetVehicleLstInfo(string.Format(vehicleaddress, i)).Result;
                foreach (var item in VehilceLst)
                {
                    VehicleTest.TestOtherInfor(item);
                }
            }
#endif
            #endregion

            #region TripTestSimple
#if TripTestSimple
            List<string> TripLst = new List<string>();
            TripLst.Add(@"/api/v1/vehicles/399ad4a9-e0c8-451d-8879-af136c3ef73d/trips");
            TripLst.Add(@"/api/v1/trips/093b2e2c-f178-41e8-86ca-111e10bb9636");
            TripLst.Add(@"/api/v1/trips/4b2574ed-72c7-44f2-a349-09cb93cbc75c");
            TripLst.Add(@"/api/v1/trips/45529786-2873-4434-937a-6a395d8857f3");
            TripLst.Add(@"/api/v1/trips/382273e4-0ff7-41de-af88-66b4de87d0bb");
            TripLst.Add(@"/api/v1/trips/ce500d23-9d4b-4bf6-8427-bbd2f6cfede1");
            TripLst.Add(@"/api/v1/trips/da66baff-c00b-47dd-8de4-d3ec82a0e822");
            TripLst.Add(@"/api/v1/trips/ca495abf-523b-4005-9b8e-c9a1294bbf25");
            foreach (var item in TripLst)
            {
                var hi = new HttpItem() { RelatedAddress = item, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
                var tat = new HTTPHelper(hi).HttpHelperMethod().GetAwaiter().GetResult();
                Trace.WriteLine(tat);
            }
#endif
            #endregion

            #region  DTCFeature
#if DTCFeature
#warning Need 5 Vehicles GUID: E404024:Vehicle {GUID} has no connected device.
            /*
            20fde398-e3f7-49ce-a8b4-2cab6fbb04ab  6C500267
            0ed17636-9058-4a30-985d-60cbafa62415  6C500264
            b265106e-614e-48b1-a8a9-8c67226669d5  6C500262
            5958cbaa-3251-4827-b6ca-521937aabfb9  6C4C74A7
            */



            //Diagnostic d1 = new Diagnostic() { DiagnosticType = DTCFeature.VEHICLE_DTC_SCAN };
            //Diagnostic d2 = new Diagnostic() { DiagnosticType = DTCFeature.VEHICLE_DTC_CLEAR };
            //Diagnostic d3 = new Diagnostic() { DiagnosticType = DTCFeature.SERVER_DTC_CLEAR };
            //var VEHICLE_DTC_SCANJson = JsonHelper.ToJson(d1);
            //var VEHICLE_DTC_CLEARJson = JsonHelper.ToJson(d2);
            //var SERVER_DTC_CLEARJson = JsonHelper.ToJson(d3);

            foreach (var item in VehilceLst)
            {
                var GetDTC = dtctest.DTCDiagnostics(item.Id, RequestMethod.Get, null).Result;
                //var VEHICLE_DTC_SCANResult = dtctest.DTCDiagnostics(item.Id, RequestMethod.Post, VEHICLE_DTC_SCANJson).Result;
                //var VEHICLE_DTC_CLEARResult=  dtctest.DTCDiagnostics(item.ID, "Post",VEHICLE_DTC_CLEARJson).Result;
                //var SERVER_DTC_CLEARResult=  dtctest.DTCDiagnostics(item.ID, "Post", SERVER_DTC_CLEARJson).Result;
            }
#endif
            #endregion

            #region CustomerFeature
#if CustomerFeature
            List<string> tenantIDLst = new List<string>() { "f6d3a82b-1234-417b-bd86-8c9c9f63a7e7" };
            foreach (var tenantID in tenantIDLst)
            {
                customerList = customerTest.GetCustomerInfo(tenantID).Result;
                if (customerList.Count != 0)
                {
                    foreach (var item in customerList)
                    {
                        var devicesinfo2 = deviceTest.GetDeviceInfoString(item._links.customer_device_list.href).Result;

                        //var te = JsonHelper.JsonDeserialize<DeviceJson>(devicesinfo2);
                        //Trace.WriteLine(te._embedded.device);
                        //if (te._embedded.device != null)
                        //    deviceTest.GetDeviceByID(te._embedded.device);

                        customerTest.GetOrChangeCustomerClaims(item.Id, RequestMethod.Get, string.Empty).GetAwaiter();
                        CustCred cred = new CustCred() { UserName = item.UserName, Email = new Random().Next(1, 9) + "apitest@delphi.com", Password = "Neusoft2" };
                        var getcustomer = customerTest.GetACustomer(item.Id).Result;
                        var updcustomer = customerTest.ChangeCustomerInfo(item.Id, cred, RequestMethod.Put).Result;
                        Trace.WriteLine(updcustomer);

#warning this need to verify
                        var vehiclelst2 = VehicleTest.GetVehicleInfoString(item._links.customer_vehicle_list.href).Result;
                        /*
                        var me = JsonHelper.JsonDeserialize<VehicleJson>(vehiclelst2);
                        if (me!= null)
                            foreach (var ve in me._embedded.vehicle)
                            {
                                VehicleTest.TestOtherInfor(ve);
                            }
                          
                        Trace.WriteLine(item._links.customer_vehicle_list);
                        */
                    }
                }
            }
#endif

            #endregion

            #region  CatalogsFeature
#if CatalogsFeature
#warning this need to verify: https://jira.lixar.net/browse/DELAPI-647
            CatalogsFeature catest = new CatalogsFeature();
            catest.CatalogsMethods();
#endif
            #endregion

            Console.ReadKey();


        }

        void RoleEnvironment_Changed(object sender, RoleEnvironmentChangedEventArgs e)
        {
            // Get the list of configuration changes
            var settingChanges = e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>();

            foreach (var settingChange in settingChanges)
            {
                Trace.WriteLine(string.Format("Setting Name: {0}--Information Value: {1} ", settingChange.ConfigurationSettingName, RoleEnvironment.GetConfigurationSettingValue(settingChange.ConfigurationSettingName)));
            }

            InitOBUparameters();
        }

        private static List<string> GetOBULabelIDList(string fileName)
        {
            List<string> obuidlist = new List<string>();
            //TODO:
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(CsvfileStorageConnectionString);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);
            var csvfileblock = container.GetBlockBlobReference(fileName);
            var csvfile= csvfileblock.DownloadTextAsync().Result;
            var  obuchararr= csvfile.Split(new char[]{','});
            for (int i = 1; i < obuchararr.Length; i+= 10)
            {
                obuidlist.Add(obuchararr[i].ToString());
                Trace.WriteLine(obuchararr[i].ToString());
            }

            return obuidlist;
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;


            RoleEnvironment.Changed += RoleEnvironment_Changed;
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //TODO:set the time to match the CT expired
            CTTimer.Interval = 1000 * 60 * CTTimmerIntervalMinute;
            CTTimer.Elapsed += CTtimer_Elapsed;
            CTTimer.Start();

            WorkTimer.Interval = 1000 * PostIntervalSec;
            WorkTimer.Elapsed += WorkTimer_Elapsed;
            WorkTimer.Start();

            return base.OnStart();
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TokenRefresh();
        }

        static void TokenRefresh()
        {
            if (TokenSeeker.CurrentToken == null)
            {
                TokenSeeker.CurrentToken = TokenHelper.GetToken(DelphiClientGuid, DelphiUserName, DelphiPassword);
                Trace.WriteLine("Token Created");
                Trace.WriteLine(TokenSeeker.CurrentToken.access_token);
                DelphiTimer.Interval = 1000 * (TokenSeeker.CurrentToken.expires_in - 100);
            }
            else
            {
                TokenSeeker.CurrentToken = TokenHelper.GetToken(TokenSeeker.CurrentToken.refresh_token,DelphiUserName,DelphiPassword);
                Trace.WriteLine("Token Refreshed");
                Trace.WriteLine(TokenSeeker.CurrentToken.access_token);
                DelphiTimer.Interval = 1000 * (TokenSeeker.CurrentToken.expires_in - 100); //1000 * 
            }

        }

        static void CTtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CTTokenSeeker.CurrentToken = CTAuthTokenHelper.GetToken(CTUserName,CTPassword).Result;
            Trace.WriteLine("CTToken Refreshed");
            Trace.WriteLine(CTTokenSeeker.CurrentToken.authToken);
        }

        static void WorkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CTMsgHelper ctmsg = new CTMsgHelper();
            var devicelst = ctmsg.GetAvailableDeviceList(deviceIDLst);
            var dvcVhicles = ctmsg.GetAvailableVehicleInfoList(devicelst);
            CTGpsInfoFeature.PostCTVehicleGPSInfoList(dvcVhicles);
            CTGANInfoFeature.PostCTVehicleCANInfoList(dvcVhicles);
            CTTroubleCodeFeature cttcode = new CTTroubleCodeFeature();
            cttcode.PostCTTroubleCodeInfoList(dvcVhicles);
        }

    }
}
