﻿using Newtonsoft.Json.Linq;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PushSharp.Apple;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace P5GenralDL
{
    public class IOSMobilePush
    { /// <summary>
      /// 
      /// </summary>
      /// <param name="collection"></param>
      /// <param name="CertificatePath"></param>
      /// <param name="Password"></param>
      /// <param name="Production"></param>
      /// <param name="Uninstall"></param>
      /// <param name="getpayload"></param>
      /// <returns></returns>
        public Tuple<List<string>, List<string>> sendIoMsg(List<MobileIOSPush> collection, string CertificatePath, string Password, int Production = 0, bool Uninstall = false, string getpayload = "", int IsGeo = 0, DataSet Data2 = null, PushCampaignModel mlObj = null)
        {
            List<string> sToken = new List<string>();
            List<string> fToken = new List<string>();
            var result = "";

            using (LogError objError = new LogError())
            {
                objError.AddErrorInLog(1, "MobFileName", "ios start".ToString(), DateTime.Now.ToString(), "");
            }

            try
            {
                ApnsConfiguration config;
                if (Production == 1)
                {
                    config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, CertificatePath, Password);
                }
                else
                {
                    config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, CertificatePath, Password);
                }
                // Create a new brocker
                var apnsBroker = new ApnsServiceBroker(config);
                // wire up events
                apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
                {
                    aggregateEx.Handle(ex =>
                    {
                        fToken.Add(notification.DeviceToken);
                        if (ex is ApnsNotificationException)
                        {
                            var notificationException = (ApnsNotificationException)ex;
                            var apnsNotification = notificationException.Notification;
                            var statusCode = notificationException.ErrorStatusCode;
                            string desc = $"Apple Notification Faild: ID{apnsNotification.Identifier}, Code={statusCode}";
                            //Console.WriteLine(desc);
                            result = desc;
                        }
                        else
                        {
                            string desc = $"Apple Notification Faild for some unknown reason : {ex.InnerException}";
                            //Console.WriteLine(desc);
                            result = desc;
                        }
                        return true;
                    });
                };

                apnsBroker.OnNotificationSucceeded += (notification) =>
                {
                    sToken.Add(notification.DeviceToken);
                    result = "Push Notification Sent Successfully!";

                };

                var fbs = new FeedbackService(config);
                fbs.FeedbackReceived += (string devToken, DateTime timestamp) =>
                {
                    // Remove the devicetoken from your database
                    // timestamp is the time the token was reported as expired
                };
                fbs.Check();

                //Start Process
                apnsBroker.Start();
                //var totunreadmsg = 1;

                foreach (var msg in collection)
                {
                    if (Uninstall == true)
                    {
                        apnsBroker.QueueNotification(new ApnsNotification
                        {
                            DeviceToken = msg.DeviceToken.ToString(),
                        });
                    }
                    else if (IsGeo == 1)
                    {
                        var geodata = "";
                        if (Data2.Tables[0].Rows.Count > 0)
                        {
                            for (var i = 0; i > Data2.Tables[0].Rows.Count; i++)
                            {
                                geodata += "{ \"MobilePushId\":\"" + Data2.Tables[0].Rows[i]["PushId"].ToString() + "\",\"Latitude\":\"" + Data2.Tables[0].Rows[i]["Latitude"].ToString() + "\",\"Longitude\":\"" + Data2.Tables[0].Rows[i]["Longitude"].ToString() + "\",\"Radius\":\"" + Data2.Tables[0].Rows[i]["Radius"].ToString() + "\",\"EntryExist\":\"" + Data2.Tables[0].Rows[i]["EntryExist"].ToString() + "\",\"GeofenceName\":\"" + Data2.Tables[0].Rows[i]["GeofenceName"].ToString() + "\"}";
                            }
                        }

                        apnsBroker.QueueNotification(new ApnsNotification
                        {
                            DeviceToken = msg.DeviceToken.ToString(),
                            //Payload = JObject.Parse("{ \"aps\": {\"content-available\": 1 }, \"regions\" :[{ \"MobilePushId\":\"" + msg.MobilePushId + "\",\"Latitude\":\"" + msg.Latitude + "\",\"Longitude\":\"" + msg.Longitude + "\",\"Radius\":\"" + msg.Radius + "\",\"EntryExist\":\"" + msg.EntryExist + "\",\"GeofenceName\":\"" + msg.GeofenceName + "\"} ]}")});
                            Payload = JObject.Parse("{ \"aps\": {\"content-available\": 1 }, \"regions\" :[{ \"" + geodata + "\" ]}")
                        });
                    }
                    else if (getpayload.Length > 0)
                    {
                        apnsBroker.QueueNotification(new ApnsNotification
                        {
                            DeviceToken = msg.DeviceToken.ToString(),
                            Payload = JObject.Parse(getpayload)
                        });
                    }
                    else
                    {
                        var mType = "";
                        if (mlObj.ImageUrl.Contains(".jpg") || mlObj.ImageUrl.Contains(".png") || mlObj.ImageUrl.Contains(".jpeg"))
                        {
                            mType = "image";
                        }
                        else if (mlObj.ImageUrl.Contains(".gif"))
                        {
                            mType = "gif";
                        }
                        else if (mlObj.ImageUrl.Contains(".mp4") || mlObj.ImageUrl.Contains(".flv	") || mlObj.ImageUrl.Contains(".wmv") || mlObj.ImageUrl.Contains(".avi") || mlObj.ImageUrl.Contains(".mov"))
                        {
                            mType = "video";
                        }

                        string message = "";
                        if (mlObj.Type == 4 && mlObj.Message.Contains("\n"))
                            message = mlObj.Message.Replace("\n", ",");
                        else
                            message = mlObj.Message.Replace("\n", "");

                        if (mlObj.Message.Contains("&"))
                            message = mlObj.Message.Replace("&", "~A~");

                        var img = mlObj.ImageUrl == "" ? "NoImage" : mlObj.ImageUrl;
                        var img1 = "";
                        if (img.IndexOf("http://", StringComparison.Ordinal) > -1 || img.IndexOf("https://", StringComparison.Ordinal) > -1
                                || img.IndexOf("//", StringComparison.Ordinal) > -1)
                            img1 = img;
                        else if (img != "NoImage")
                        {
                            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
                            var get_cdnpath = Configuration.GetSection("ConnectionStrings:cdnpath").Value;
                            img1 = get_cdnpath + "MobileEngagementUploads/" + img;
                        }
                           
                        else if (img == "NoImage")
                            img1 = "";

                        var redirect = (mlObj.Redirection != "" ? "" : (mlObj.DeepLinkUrl == "" ? mlObj.ExternalUrl : mlObj.DeepLinkUrl));

                        //var chk = "{\"aps\":{\"alert\": {\"body\": \"" + message + "\",\"title\": \"" + mlObj.Title + "\",\"subtitle\": \"" + mlObj.SubText + "\"},\"mutable-content\":1,\"badge\":0,\"sound\":\"mailsent.wav\"},\"extra\": \"" + mlObj.ExtraButtons.ToString() + "\",\"attachment-url\": \"" + img1 + "\",\"media-type\": \"" + mType + "\"}";

                        var chkToken = msg.DeviceToken.ToString();

                        using (LogError objError = new LogError())
                        {
                            objError.AddErrorInLog(1, "MobFileName", chkToken.ToString(), DateTime.Now.ToString(), "");
                        }

                        apnsBroker.QueueNotification(new ApnsNotification
                        {
                            DeviceToken = chkToken.ToString(),
                            Payload = JObject.Parse("{\"aps\":{\"alert\": {\"body\": \"" + message + "\",\"title\": \"" + mlObj.Title + "\",\"subtitle\": \"" + mlObj.SubText + "\"},\"mutable-content\":1,\"badge\":0,\"sound\":\"mailsent.wav\"},\"campaignid\": \"" + mlObj.CampaignId + "\",\"redirect-url\": \"" + redirect + "\",\"extra\": \"" + mlObj.ExtraButtons.ToString() + "\",\"attachment-url\": \"" + img1 + "\",\"media-type\": \"" + mType + "\"}")
                            //Payload = JObject.Parse("{\"aps\":{\"alert\": {\"body\": \"" + message + "\",\"title\": \"" + mlObj.Title + "\",\"subtitle\": \"" + mlObj.SubText + "\"},\"sound\":\"mailsent.wav\"},\"extra\": \"" + mlObj.ExtraButtons.ToString() + "\",\"attachment-url\": \"" + img1 + "\",\"media-type\": \"" + mType + "\"}")

                            //Payload = JObject.Parse("{\"aps\":{\"alert\": {\"body\": \"Push notification body\",\"title\": \"Push notification title\"},\"badge\":20,\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42,\"media-url\": \"https://i.imgur.com/t4WGJQx.jpg\"}")
                            //Payload = JObject.Parse("{\"aps\":{\"alert\": {\"body\": \"" + message + "\",\"title\": \"" + mlObj.Title + "\",\"subtitle\": \"" + mlObj.SubText + "\"},\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42,\"media-url\": \"" + img1 + "\",\"redirect-url\": \"" + redirect + "\",\"extra\": \"" + mlObj.ExtraButtons.ToString() + "\"}")



                        });
                        //Thread.Sleep(2300);
                        Thread.Sleep(500);
                    }
                }
                apnsBroker.Stop();
            }
            catch (Exception ex)
            {
                using (LogError objError = new LogError())
                {
                    objError.AddErrorInLog(1, "MobFileName", ex.Message.ToString(), DateTime.Now.ToString(), ex.StackTrace);
                }

                throw;
            }

            if (Uninstall == true)
            {
                fToken = CheckFeedbackService(fToken, CertificatePath, Password, Production);
            }
            return Tuple.Create(sToken, fToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fToken"></param>
        /// <param name="CertificatePath"></param>
        /// <param name="Password"></param>
        /// <param name="Production"></param>
        /// <returns></returns>
        public List<string> CheckFeedbackService(List<string> fToken, string CertificatePath, string Password, int Production = 0)
        {
            int port = 2196;
            String hostname = "";
            if (Production == 1)
            {
                hostname = "feedback.push.apple.com";
            }
            else
            {
                hostname = "feedback.sandbox.push.apple.com";
            }
            string certificatePassword = Password;

            X509Certificate2 clientCertificate = new X509Certificate2(CertificatePath, certificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            //X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
            X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

            TcpClient client = new TcpClient(hostname, port);
            SslStream sslStream = new SslStream(
                            client.GetStream(),
                            false,
                            new RemoteCertificateValidationCallback(ValidateServerCertificate),
                            null
            );

            try
            {
                sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
            }
            catch
            {
                //Console.WriteLine("Authentication failed");
                client.Close();
            }

            byte[] buffer = new byte[38];
            int recd = 0;
            DateTime minTimestamp = DateTime.Now.AddYears(-1);
            string result = string.Empty;
            try
            {

                if (sslStream != null)
                {
                    recd = sslStream.Read(buffer, 0, buffer.Length);
                    while (recd > 0)
                    {
                        byte[] bSeconds = new byte[4];
                        byte[] bDeviceToken = new byte[32];
                        Array.Copy(buffer, 0, bSeconds, 0, 4);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bSeconds);
                        int tSeconds = BitConverter.ToInt32(bSeconds, 0);
                        var Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(tSeconds).ToLocalTime();
                        Array.Copy(buffer, 6, bDeviceToken, 0, 32);
                        string deviceToken = BitConverter.ToString(bDeviceToken).Replace("-", "").ToLower().Trim();
                        if (deviceToken.Length == 64 && Timestamp > minTimestamp)
                        {
                            fToken.Add(deviceToken);
                            result = deviceToken;
                        }
                        Array.Clear(buffer, 0, buffer.Length);
                        recd = sslStream.Read(buffer, 0, buffer.Length);
                    }
                }

                if (sslStream != null)
                    sslStream.Close();

                if (client != null)
                    client.Close();

            }
            catch
            {
                //errorLog.AddErrorLog("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return null;
            }
            finally
            {
                // The client stream will be closed with the sslStream
                // because we specified this behavior when creating
                // the sslStream.
                sslStream.Close();
                client.Close();
            }
            return fToken;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlpush"></param>
        /// <param name="CertificatePath"></param>
        /// <param name="Password"></param>
        public void sendMsgWithourDll(MobileIOSPush mlpush, string CertificatePath, string Password)
        {
            string devicetocken = mlpush.DeviceToken;//"51dc39080a8673eeac4c8a8a39fdca6f5a4a70ed505b2707c8571315ecf1b8b7";//  iphone device token
            int port = 2195;
            String hostname = "gateway.sandbox.push.apple.com";
            //String hostname = "gateway.push.apple.com";

            string certificatePath = CertificatePath;//Server.MapPath("~/Certificate/IOS/P5ApnsCert.p12");

            string certificatePassword = Password;//"Plumb5@12345";

            X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            //X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
            X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

            TcpClient client = new TcpClient(hostname, port);
            SslStream sslStream = new SslStream(
                            client.GetStream(),
                            false,
                            new RemoteCertificateValidationCallback(ValidateServerCertificate),
                            null
            );

            try
            {
                sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
            }
            catch
            {
                //Console.WriteLine("Authentication failed");
                client.Close();
                //Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                return;
            }


            //// Encode a test message into a byte array.
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memoryStream);

            writer.Write((byte)0);  //The command
            writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
            writer.Write((byte)32); //The deviceId length (big-endian second byte)

            byte[] b0 = HexString2Bytes(devicetocken);

            WriteMultiLineByteArray(b0);

            writer.Write(b0);
            string payload;
            //string strmsgbody = "";
            int totunreadmsg = 20;
            //strmsgbody = "Hey Aashish!";

            //Debug.WriteLine("during testing via device!");
            //Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

            //payload="{\"aps\": {\"alert\": {\"body\": \"Push notification body\",\"title\": \"Push notification title\"},\"mutable-content\": 1 category: \"rich-apns\"},\"media-url\": \"https://i.imgur.com/t4WGJQx.jpg\"}";
            payload = "{\"aps\":{\"alert\": {\"body\": \"" + mlpush.Body + "\",\"title\": \"" + mlpush.Title + "\"},\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42,\"media-url\": \"" + mlpush.Image + "\"}";

            //payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
            writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
            writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

            byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
            writer.Write(b1);
            writer.Flush();

            byte[] array = memoryStream.ToArray();
            //Debug.WriteLine("This is being sent...\n\n");
            //Debug.WriteLine(array);
            try
            {
                sslStream.Write(array);
                sslStream.Flush();
            }
            catch
            {
                //Debug.WriteLine("Write failed buddy!!");
                //Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
            }

            client.Close();
            //Debug.WriteLine("Client closed.");
            //Request.SaveAs(Server.MapPath("APNSSuccess.txt"), true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] HexString2Bytes(string hexString)
        {
            //check for null
            if (hexString == null) return null;
            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            //create a byte array
            byte[] bs = new byte[len_half];
            try
            {
                //convert the hexstring to bytes
                for (int i = 0; i != len_half; i++)
                {
                    bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch
            {
                //MessageBox.Show("Exception : " + ex.Message);
            }
            //return the byte array
            return bs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;


            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public static void WriteMultiLineByteArray(byte[] bytes)
        {
            const int rowSize = 20;
            int iter;

            //Console.WriteLine("initial byte array");
            //Console.WriteLine("------------------");

            for (iter = 0; iter < bytes.Length - rowSize; iter += rowSize)
            {
                //Console.Write(BitConverter.ToString(bytes, iter, rowSize));
                //Console.WriteLine("-");
            }
            //Console.WriteLine(BitConverter.ToString(bytes, iter));
            //Console.WriteLine();
        }
    }
}
