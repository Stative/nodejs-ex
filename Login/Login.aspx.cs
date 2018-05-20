using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection sqlConncetion= null ;
        protected async void Page_Load(object sender, EventArgs e)
        {
            
            string conncectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            sqlConncetion  = new SqlConnection(conncectionString);
            await sqlConncetion.OpenAsync();
        }

        protected async void Button1_Click(object sender, EventArgs e) 
        {
            Dictionary<string, string> db = new Dictionary<string, string>();
            SqlCommand getUsersCredCmd = new SqlCommand("SELECT [Login],[Password] FROM [Users]", sqlConncetion);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getUsersCredCmd.ExecuteReaderAsync();
                while(await sqlReader.ReadAsync())
                {
                    db.Add(Convert.ToString(sqlReader["Login"]),Convert.ToString(sqlReader["Password"]));  
                }
            }
            catch
            {

            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();  
            }
            if (TextBox2.Text == db[TextBox1.Text])
            {
                HttpCookie login = new HttpCookie("login", TextBox1.Text);
                HttpCookie sign = new HttpCookie("sign", SignGenerator.GetSign(TextBox1.Text +"bytepp"));
                Response.Cookies.Add(login);
                Response.Cookies.Add(sign);
                Response.Redirect("UserPage.aspx",false);
            }
        }
        protected void Page_Unload(object sender,EventArgs e)
        {
            if (sqlConncetion != null && sqlConncetion.State != ConnectionState.Closed)
                sqlConncetion.Close();
        }

    }
}