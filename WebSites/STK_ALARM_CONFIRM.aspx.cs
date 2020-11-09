using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_ALARM_CONFIRM : System.Web.UI.Page
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
            AddDefaultFirstRecord();
            GET_DDL_DATA_DEPA();
            GET_DDL_DATA_ALARMNO();
        }
    }

    private void Load_GVData()
    {
        string m_querystr = "Select a.*,b.alarm_desc as alm_desc from STK_ALARM as a left join stk_alarm_setup as  b on a.alm_msgtype=b.alarm_no where alm_confirm=1 ";

        //m_querystr = m_querystr + " and a.depa='" + DDL_depa.SelectedValue.ToString().Trim() + "' ";
        if (!string.IsNullOrEmpty(DDL_depa.SelectedValue.ToString())) m_querystr = m_querystr + "and a.depa='" + DDL_depa.SelectedValue.ToString().Trim() + "' ";
        if (!string.IsNullOrEmpty(DDL_msgtype.SelectedValue.ToString())) m_querystr = m_querystr + "and a.alm_msgtype='" + DDL_msgtype.SelectedValue.ToString().Trim() + "' ";
        if (DLL_confirm.SelectedValue.ToString()=="1") m_querystr = m_querystr +" and a.confirm_user is not null ";    //過濾已確認資料
        if (DLL_confirm.SelectedValue.ToString()=="0") m_querystr = m_querystr + "and a.confirm_user is null ";    //過濾未確認資料

        m_querystr = m_querystr + " order by a.alm_datetime desc ";

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
        string m_description,m_user;
        m_user = ((Label)GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lbl_confirm_user")).Text;
        m_description = ((Label)GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lbl_confirm_description")).Text;

        if (! string.IsNullOrEmpty(m_description))
        {
            if (m_user != Session["LoginName"].ToString())
            {
                Response.Write("<script>alert('該筆資料已經確認，如要取消確認或修正，需使用同一帳號進行修正');</script>");
                return;
            }
        }

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
        DateTime m_confirm_datetime;
        string m_user,m_description,m_serialno;

        m_serialno = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_alm_serialno_edit")).Text;

        m_user = Session["LoginName"].ToString();
        m_description = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_confirm_description_edit")).Text;
        m_confirm_datetime = DateTime.Now;
        
        try
        {
            //把確認說明清空代表取消確認，就把user,description,confirm_datetime 清為NULL
            if (string.IsNullOrEmpty(m_description))
            {
                M_sqlcmd.CommandText = @"UPDATE STK_ALARM SET CONFIRM_DATETIME=null,CONFIRM_USER=null,CONFIRM_DESCRIPTION=null WHERE ALM_Serialno=@m_serialno ;";
                M_sqlcmd.Parameters.Clear();

                M_sqlcmd.Parameters.AddWithValue("@m_serialno", m_serialno);
            }
            else
            {
                M_sqlcmd.CommandText = @"UPDATE STK_ALARM SET CONFIRM_DATETIME=@m_confirm_datetime,CONFIRM_USER=@m_user,CONFIRM_DESCRIPTION=@m_description WHERE ALM_Serialno=@m_serialno ;";
                M_sqlcmd.Parameters.Clear();

                M_sqlcmd.Parameters.AddWithValue("@m_serialno", m_serialno);

                M_sqlcmd.Parameters.AddWithValue("@m_confirm_datetime", m_confirm_datetime);
                M_sqlcmd.Parameters.AddWithValue("@m_user", m_user);
                M_sqlcmd.Parameters.AddWithValue("@m_description", m_description);
            }

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_ALARM_Data";
        dt2.Columns.Add(new DataColumn("alm_serialno", typeof(string)));
        dt2.Columns.Add(new DataColumn("alm_datetime", typeof(DateTime)));
        dt2.Columns.Add(new DataColumn("alm_msgtype", typeof(string)));
        dt2.Columns.Add(new DataColumn("alm_desc", typeof(string)));
        dt2.Columns.Add(new DataColumn("alm_mailsubject", typeof(string)));
        dt2.Columns.Add(new DataColumn("alm_mailbody", typeof(string)));
        dt2.Columns.Add(new DataColumn("confirm_description", typeof(string)));
        dt2.Columns.Add(new DataColumn("confirm_datetime", typeof(DateTime)));
        dt2.Columns.Add(new DataColumn("confirm_user", typeof(string)));
        dt2.Columns.Add(new DataColumn("send_times", typeof(decimal)));
        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        ViewState["T_ALARM_Data"] = dt2;
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }

    private void GET_DDL_DATA_DEPA()
    {
        string m_sqlstr = "SELECT '' as depa union SELECT DISTINCT depa as depa FROM STK_ChemData Order By depa ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_depa.DataValueField = "depa";
                DDL_depa.DataTextField = "depa";

                DDL_depa.DataSource = ddl;
                DDL_depa.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

    private void GET_DDL_DATA_ALARMNO()
    {
        string m_sqlstr = "SELECT '' as alarm_no,'' as alarm_desc union SELECT alarm_no,alarm_no+'  '+alarm_desc as alarm_desc FROM STK_ALARM_SETUP Order By alarm_no ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_msgtype.DataValueField = "alarm_no";
                DDL_msgtype.DataTextField = "alarm_desc";

                DDL_msgtype.DataSource = ddl;
                DDL_msgtype.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[2].Visible = false;
            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_ALARM_CONFIRM"))
            {
                e.Row.Cells[0].Visible = false;
            }
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

}