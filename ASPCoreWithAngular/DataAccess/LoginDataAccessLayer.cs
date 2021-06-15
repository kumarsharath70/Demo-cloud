using ASPCoreWithAngular.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.DataAccess
{
    public class LoginDataAccessLayer : ILogin
    {
        private string connectionString;
        private readonly ILogger<LoginDataAccessLayer> _logger;
        public LoginDataAccessLayer(IConfiguration configuration, ILogger<LoginDataAccessLayer> logger)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }

        public int CheckUser(string userName, string password)
        {
            int userId = 0;
            // string pwd = DecryptString(password);
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetUserMembershipByUserName", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", userName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader["UserId"]) >  0)
                                {
                                    string dbPwd = DecryptString(reader["TemporaryPassword"].ToString());
                                    if (password != dbPwd) return userId;
                                    
                                    userId = Convert.ToInt32(reader["UserId"]); 

                                }
                            }
                        }
                    }
                }
                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return userId;
            }
        }

        public string DecryptString(string stringToDecrypt)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("Passord"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            stringToDecrypt = RemoveInvalidChars(stringToDecrypt);
            byte[] DataToDecrypt = Convert.FromBase64String(stringToDecrypt);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        public string RemoveInvalidChars(string strSource)
        {
            return Regex.Replace(strSource, @"[^0-9a-zA-Z=+\/]", "");
        }
        public int UpdateReadAction(string userName, string action, string fileName)
        {
            int status = 0; ;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateOrderReadStatus", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@info_Read", action == "Read" ? 1: 0);
                    cmd.Parameters.AddWithValue("@info_ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@info_FileName", fileName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                return status;
                            }
                        }
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return status;
            }
        }
    }
}

