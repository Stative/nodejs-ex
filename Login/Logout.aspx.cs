using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie login = new HttpCookie("login",string.Empty);
            HttpCookie sign = new HttpCookie("sign", string.Empty);
            login.Expires = DateTime.Now.AddDays(-1);
            sign.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(login);
            Response.Cookies.Add(sign);
            Response.Redirect("Login.aspx");
        }
    }
}