﻿using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp.Extensions;

namespace Plumb5GenralFunction
{
    public class SaveDownloadFilesToAws
    {
        int AdsId = 0;
        int MailTemplateId = 0;
        AmazonS3Client client;
        public string bucketname = "";
        public string AwsBucketURL = "";
        public string _bucketName = "";
        public string Foldername = "";
        public SaveDownloadFilesToAws(int adsid, int mailtemplateId)
        {
            AdsId = adsid;
            MailTemplateId = mailtemplateId;
            client = new AmazonS3Client(AllConfigURLDetails.KeyValueForConfig["ACCESSKEY"].ToString(), AllConfigURLDetails.KeyValueForConfig["SECRETKEY"].ToString(), Amazon.RegionEndpoint.GetBySystemName(AllConfigURLDetails.KeyValueForConfig["REGIONENDPOINT"].ToString()));
            //bucketname = AllConfigURLDetails.KeyValueForConfig["BUCKETNAME"] + @"/" + "allimages/Campaign-" + AdsId + "-" + MailTemplateId + "";
            bucketname = "allimages/Campaign-" + AdsId + "-" + MailTemplateId + "/";
            _bucketName = AllConfigURLDetails.KeyValueForConfig["BUCKETNAME"].ToString();
        }

        public SaveDownloadFilesToAws(int adsid, string folderName)
        {
            AdsId = adsid;
            client = new AmazonS3Client(AllConfigURLDetails.KeyValueForConfig["ACCESSKEY"].ToString(), AllConfigURLDetails.KeyValueForConfig["SECRETKEY"].ToString(), Amazon.RegionEndpoint.GetBySystemName(AllConfigURLDetails.KeyValueForConfig["REGIONENDPOINT"].ToString()));
            bucketname = AllConfigURLDetails.KeyValueForConfig["BUCKETNAME"].ToString()  ;
            AwsBucketURL = AllConfigURLDetails.KeyValueForConfig["AWS_BUCKET_URL"] + @"/" + folderName;
            Foldername = folderName;
        }

        public async Task<string> UploadFiles(string keyName, string filePath)
        {
            string ResponseId = "";
            if (keyName != null && !string.IsNullOrEmpty(keyName) && filePath != null && !string.IsNullOrEmpty(filePath) && bucketname != null && !string.IsNullOrEmpty(bucketname))
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12;


                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketname,
                        Key = keyName,
                        FilePath = filePath,
                        //ContentType = "text/plain"
                    };

                    PutObjectResponse response = await client.PutObjectAsync(putRequest);

                    if (response.HttpStatusCode.ToString() == "OK" && response.ETag != null && response.ETag != "")
                        ResponseId = response.ETag.Replace("\"", "").ToString();
                }
                catch (Exception ex)
                {
                    using (ErrorUpdation objError = new ErrorUpdation("AmazonS3UploadErrorLog"))
                    {
                        objError.AddError(ex.Message.ToString(), "FilePath= " + filePath + " | keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "", "");
                    }
                }
            }
            return ResponseId;
        }

        public async Task<string> UploadFileContent(string keyName, string ContentBody)
        {
            string ResponseId = "";
            if (keyName != null && !string.IsNullOrEmpty(keyName) && ContentBody != null && !string.IsNullOrEmpty(ContentBody) && bucketname != null && !string.IsNullOrEmpty(bucketname))
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12;

                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketname,
                        Key = keyName,
                        ContentBody = ContentBody
                    };

                    PutObjectResponse response = await client.PutObjectAsync(putRequest);

                    if (response.HttpStatusCode.ToString() == "OK" && response.ETag != null && response.ETag != "")
                        ResponseId = response.ETag.Replace("\"", "").ToString();
                }
                catch (Exception ex)
                {
                    using (ErrorUpdation objError = new ErrorUpdation("AmazonS3UploadErrorLog"))
                    {
                        objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "", "");
                    }
                }
            }
            return ResponseId;
        }

        public async Task<string> UploadFileStream(string keyName, Stream InputStream)
        {
            string ResponseId = "";
            if (keyName != null && !string.IsNullOrEmpty(keyName) && InputStream != null && bucketname != null && !string.IsNullOrEmpty(bucketname))
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12;

                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketname,
                        Key = keyName,
                        InputStream = InputStream
                    };

                    PutObjectResponse response = await client.PutObjectAsync(putRequest);

                    if (response.HttpStatusCode.ToString() == "OK" && response.ETag != null && response.ETag != "")
                        ResponseId = response.ETag.Replace("\"", "").ToString();
                }
                catch (Exception ex)
                {
                    using (ErrorUpdation objError = new ErrorUpdation("AmazonS3UploadErrorLog"))
                    {
                        objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "", "");
                    }
                }
            }
            return ResponseId;
        }

        public async Task DownloadFiles(string keyName, string filePath, string BucketPath)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = BucketPath,
                    Key = keyName
                };

                GetObjectResponse getresponse = await client.GetObjectAsync(getRequest);
                if (getresponse != null)
                    await getresponse.WriteResponseStreamToFileAsync(filePath, true, default);
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "FilePath= " + filePath + " | keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "", "");
                }
            }
        }

        public async Task<Stream> GetFileContentStream(string keyName, string BucketPath)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = BucketPath,
                    Key = keyName
                };

                GetObjectResponse getresponse = await client.GetObjectAsync(getRequest);
                if (getresponse != null)
                    return getresponse.ResponseStream;
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->GetFileContent", "");
                }
            }

            return null;
        }

        public async Task<string> GetFileContentString(string keyName, string BucketPath)
        {
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = BucketPath,
                    Key = bucketname + keyName,
                };

                GetObjectResponse getresponse = await client.GetObjectAsync(getRequest);
                if (getresponse != null)
                {
                    byte[] responseByte = getresponse.ResponseStream.ReadAsBytes();
                    return Encoding.UTF8.GetString(responseByte);
                }
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->GetFileContent", ex.StackTrace);
                }
            }
            return null;
        }

        public string GetFileContentStringAsync(string keyName, string BucketPath)
        {
            try
            {
                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = BucketPath,
                    Key = keyName,
                };

                var task = client.GetObjectAsync(getRequest);
                task.Wait();
                var getresponse = task.Result;

                if (getresponse != null)
                {
                    byte[] responseByte = getresponse.ResponseStream.ReadAsBytes();
                    return Encoding.UTF8.GetString(responseByte);
                }
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->GetFileContentStringAsync", ex.StackTrace);
                }
            }

            return null;
        }

        public async Task<bool> DeleteFile(string keyName, string BucketPath)
        {
            bool status = false;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = BucketPath,
                    Key = keyName
                };

                DeleteObjectResponse response = await client.DeleteObjectAsync(request);

                if (response.HttpStatusCode.ToString() == "OK")
                    status = true;
            }
            catch (AmazonS3Exception ex)
            {
                status = false;
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->DeleteFile", "");
                }
            }
            catch (Exception ex)
            {
                status = false;
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->DeleteFile", "");
                }
            }

            return status;
        }

        public async Task<Tuple<string, string>> UploadClientFiles(string FileName, Stream InputStream)
        {
            FileName = Regex.Replace(FileName, @"\s+", "_");
            string ResponseId = string.Empty;
            string _BucketName = string.Empty;
            string UniqueId = Helper.GenerateUniqueNumber();
            string GivenFileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
            string GivenFileExtension = Path.GetExtension(FileName);
            string _FileName = Foldername+@"/" + GivenFileNameWithoutExtension + "_" + UniqueId + GivenFileExtension;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketname,
                    Key = _FileName,
                    InputStream = InputStream,
                    //ContentType = "text/plain"
                };

                PutObjectResponse response = await client.PutObjectAsync(putRequest);

                if (response.HttpStatusCode.ToString() == "OK" && response.ETag != null && response.ETag != "")
                    ResponseId = response.ETag.Replace("\"", "").ToString();

                if (!String.IsNullOrEmpty(ResponseId))
                {
                    _BucketName = AwsBucketURL + "/" + _FileName;
                    _FileName = FileName;

                }

            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3UploadErrorLog"))
                {
                    objError.AddError(ex.Message.ToString(), "InputStream= " + InputStream.ToString() + " | keyName= " + FileName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "", "");
                }
            }

            return new Tuple<string, string>(_FileName, _BucketName);
        }

        public async Task<bool> ListingContentAndDownloadFiles(string keyName, string downloadLocation, string BucketPath)
        {
            bool result = false;
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                ListObjectsRequest request = new ListObjectsRequest
                {
                    BucketName = _bucketName,
                    Prefix = $"allimages/{keyName}/"
                };

                ListObjectsResponse response = await client.ListObjectsAsync(request);

                foreach (S3Object obj in response.S3Objects)
                {
                    try
                    {
                        if (!Directory.Exists(downloadLocation))
                            Directory.CreateDirectory(downloadLocation);

                        var filename = obj.Key.Split('/')[2];

                        if (!String.IsNullOrEmpty(filename))
                        {
                            GetObjectRequest getRequest = new GetObjectRequest
                            {
                                BucketName = bucketname,
                                Key = filename
                            };

                            GetObjectResponse Response = await client.GetObjectAsync(getRequest);
                            if (Response != null && obj.Size > 0)
                            {
                                using (Stream responseStream = Response.ResponseStream)
                                {
                                    await Response.WriteResponseStreamToFileAsync(downloadLocation + "\\" + filename, true, default);
                                }
                            }
                        }
                    }
                    catch (IOException ioex)
                    {
                        throw ioex;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "FilePath= " + downloadLocation + " | keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws-->ListingContentAndDownloadFiles", ex.StackTrace);
                }
            }
            return result;
        }

        public async Task<bool> ListingFilesAndDeleteFiles(string keyName, string BucketPath)
        {
            bool result = false;
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                ListObjectsRequest request = new ListObjectsRequest
                {
                    BucketName = _bucketName,
                    Prefix = $"allimages/{keyName}/"
                };

                ListObjectsResponse response = await client.ListObjectsAsync(request);
                foreach (S3Object obj in response.S3Objects)
                {
                    try
                    {
                        var filename = obj.Key.Split('/')[2];

                        if (!String.IsNullOrEmpty(filename))
                        {
                            bool status = false;
                            try
                            {
                                DeleteObjectRequest deleterequest = new DeleteObjectRequest
                                {
                                    BucketName = BucketPath,
                                    Key = filename
                                };

                                DeleteObjectResponse deleteresponse = await client.DeleteObjectAsync(deleterequest);

                                //if (deleteresponse.HttpStatusCode.ToString() == "OK")
                                //    status = true;

                                //if (!status)
                                //{
                                //    using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                                //    {
                                //        objError.AddError("", "keyName= " + filename + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->ListingFilesAndDeleteFiles", "");
                                //    }
                                //}
                            }
                            catch (AmazonS3Exception ex)
                            {
                                status = false;
                                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                                {
                                    objError.AddError(ex.Message.ToString(), "keyName= " + filename + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->ListingFilesAndDeleteFiles", "");
                                }
                            }
                            catch (Exception ex)
                            {
                                status = false;
                                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                                {
                                    objError.AddError(ex.Message.ToString(), "keyName= " + filename + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->ListingFilesAndDeleteFiles", "");
                                }
                            }
                        }
                    }
                    catch (IOException ioex)
                    {
                        throw ioex;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws-->ListingFilesAndDeleteFiles", ex.StackTrace);
                }
            }

            return result;
        }

        public async Task<byte[]> GetFileContentStringBytes(string keyName, string BucketPath)
        {
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = BucketPath,
                    Key = keyName,
                };

                GetObjectResponse getresponse = await client.GetObjectAsync(getRequest);
                if (getresponse != null)
                {
                    return getresponse.ResponseStream.ReadAsBytes();
                }
            }
            catch (Exception ex)
            {
                using (ErrorUpdation objError = new ErrorUpdation("AmazonS3DownloadFiles"))
                {
                    objError.AddError(ex.Message.ToString(), "keyName= " + keyName + " | SubDirectoryInBucket= " + bucketname + "", DateTime.Now.ToString(), "SaveDownloadFilesToAws->GetFileContent", ex.StackTrace);
                }
            }

            return null;
        }
    }
}