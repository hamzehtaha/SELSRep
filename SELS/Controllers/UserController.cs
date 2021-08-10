using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SELS.Controllers
{
    public class UserController : ApiController
    {
        private const string cCLASS_NAME = "UserController";
        #region
        /// <summary>
        /// To get user name and password from data base 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            try
            {
                string tQuery = @"select Username,Password from 
                                    dbo.[User]";
                DataTable tTable = new DataTable();
                using (var con = new SqlConnection (ConfigurationManager.ConnectionStrings["UserAppDB"].ConnectionString))
                    using (var tCommand = new SqlCommand(tQuery,con))
                using (var tDatatable = new SqlDataAdapter(tCommand))
                {
                    tCommand.CommandType = CommandType.Text;
                    tDatatable.Fill(tTable);
                }
                return Request.CreateResponse(HttpStatusCode.OK, tTable); 

            }catch (Exception ex)
            {
                Console.WriteLine(cCLASS_NAME +" "+ ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Not found");
            }
        }
        /// <summary>
        /// To add new User in data base 
        /// </summary>
        /// <param name="pUser">User object</param>
        /// <returns></returns>
        public string POST (Models.User pUser)
        {
            try
            {
                if (CheckValidateUserNameAndPassword(pUser))
                {
                    string tQuery = @"
                                    INSERT INTO dbo.[User] VALUES (
                                    '" + pUser.Username + @"'
                                    ,'" + pUser.Password + @"'
                                    ,'" + pUser.Phonenumber + @"'
                                  )";
                    DataTable tTable = new DataTable();
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserAppDB"].ConnectionString))
                    using (var tCommand = new SqlCommand(tQuery, con))
                    using (var tDatatable = new SqlDataAdapter(tCommand))
                    {
                        tCommand.CommandType = CommandType.Text;
                        tDatatable.Fill(tTable);
                    }
                    return ("Added!!");
                }
                else
                {
                    return ("Something went wrong , Check your validations");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(cCLASS_NAME + " " + ex.Message);
                return ("Something went wrong");
            }
        }
        #endregion

        #region
        /// <summary>
        /// To check the validate the username and password
        /// </summary>
        /// <param name="pUser"></param>
        /// <returns></returns>
        private bool CheckValidateUserNameAndPassword (Models.User pUser)
        {
            try
            {
                return true; 
            }catch (Exception ex)
            {
                Console.WriteLine(cCLASS_NAME + " " + ex.Message);
                return false; 
            }
        }

        [HttpPost]
        [Route("api/user/login")]
        public bool Login(string UserName,string Password)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserAppDB"].ConnectionString);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT COUNT(*) FROM [user] WHERE [username]='" + UserName + "' AND [password]='" + Password + "'", con);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
