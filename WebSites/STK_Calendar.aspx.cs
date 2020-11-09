using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STK_Calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string m_script, m_TXBID;
        m_TXBID = Request.QueryString["TextBoxId"].Trim();
        m_script = "opener.window.document.getElementById('" + m_TXBID + "').value='" + Calendar1.SelectedDate.Date.ToString("yyyy/MM/dd") + "';";
        m_script = m_script + "window.close();";
        ClientScript.RegisterStartupScript(GetType(), "_Calender", m_script, true);
    }
}