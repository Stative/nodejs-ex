using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            HttpCookie sign = Request.Cookies["sign"];
            if(login !=null && sign !=null)
            {
                if (sign.Value == SignGenerator.GetSign(login.Value + "bytepp"))
                {
                    Label1.Text = login.Value;
                    return;
                }
            }
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Logout.aspx");
        }
    }
}