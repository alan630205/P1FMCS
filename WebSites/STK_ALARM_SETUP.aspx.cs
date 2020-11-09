using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_ALARM_SETUP : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_ALARM_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Load_GVData();
        }
    }

    private void Load_GVData()
    {
        
        string m_querystr = "Select * from STK_ALARM_SETUP order by alarm_no  ";

        //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_querystr + "');</script>");

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_ALARM_cmd.Connection = M_SQLConn;

            M_STK_ALARM_cmd.CommandText = m_querystr;

            M_da.SelectCommand = M_STK_ALARM_cmd;
            M_da.Fill(M_ds, "DT_STK_ALARM");
            GridView1.DataSource = M_ds.Tables["DT_STK_ALARM"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Load_GVData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Load_GVData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string m_mailsubject, m_mailbody, m_sms,m_alarmno;
        string m_errorobject = "";
        bool m_confirm;
        try
        {
            m_alarmno = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblalarmno_Edit")).Text;
            m_mailsubject = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_mailsubject")).Text;
            m_mailbody = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_mailbody")).Text;
            m_sms = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_sms")).Text;
            m_confirm = ((CheckBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("cbconfirm_EDIT")).Checked;
            if (string.IsNullOrEmpty(m_mailsubject))
            {
                m_errorobject = "MAIL主旨欄位 ";
                throw new Exception("不可以為空白 !!");
            }
            if (string.IsNullOrEmpty(m_mailbody))
            {
                m_errorobject = "MAIL內文欄位 ";
                throw new Exception("不可以為空白 !!");
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }
        
        try
        {

            M_sqlcmd.CommandText = @"UPDATE STK_Alarm_SETUP SET MAIL_SUBJECT=@m_mailsubject,MAIL_BODY=@m_mailbody,SMS=@m_sms,ALARM_CONFIRM=@m_confirm where ALARM_NO=@m_alarmno ;";
            M_sqlcmd.Parameters.Clear();

            M_sqlcmd.Parameters.AddWithValue("m_mailsubject", m_mailsubject);
            M_sqlcmd.Parameters.AddWithValue("m_mailbody", m_mailbody);
            M_sqlcmd.Parameters.AddWithValue("m_sms", m_sms);
            M_sqlcmd.Parameters.AddWithValue("m_alarmno", m_alarmno);
            M_sqlcmd.Parameters.AddWithValue("m_confirm", m_confirm);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView1.EditIndex = -1;
            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        
    }
    protected bool Chk_Authority(string m_loginid, string m_appid)
    {
        bool m_result = false;
        SqlCommand M_Chk_Authority_cmd = new SqlCommand();

        using (SqlConnection M_SQLConn = new SqlConnection(M_appconnstr[0]))
        {
            M_Chk_Authority_cmd.Connection = M_SQLConn;
            M_SQLConn.Open();

            M_Chk_Authority_cmd.CommandText = "Select top 1 Write_Flag from User_Task where user_id=@m_loginid and Task_ID=@m_appid";
            M_Chk_Authority_cmd.Parameters.Clear();
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_loginid", m_loginid);
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_appid", m_appid);

            SqlDataReader M_dr = M_Chk_Authority_cmd.ExecuteReader();

            if (M_dr.HasRows)
            {
                M_dr.Read();
                m_result = Convert.ToBoolean(M_dr["Write_Flag"].ToString());
            }
            else
            {
                m_result = false;
            }
            M_dr.Close();
            M_dr.Dispose();
            M_SQLConn.Close();
            M_SQLConn.Dispose();
        }
        M_Chk_Authority_cmd.Dispose();
        return m_result;
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_ALARM_SETUP"))
            {
                e.Row.Cells[0].Visible = false;
            }

        }

    }
}