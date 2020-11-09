using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class STK_TimeTable_Edit : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_T_SUP_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    //public string m_key_node, m_key_system, m_key_valve, m_key_pou, m_key_tagname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //AddDefaultFirstRecord();
            Load_GVData();
        }
    
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_TimeTable_TMP";
        dt2.Columns.Add(new DataColumn("RowId", typeof(int)));

        dt2.Columns.Add(new DataColumn("TimeId", typeof(string)));
        dt2.Columns.Add(new DataColumn("TimeName", typeof(string)));
        dt2.Columns.Add(new DataColumn("S_Hour", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("E_Hour", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("Rec_Day", typeof(decimal)));

        dr2 = dt2.NewRow();

        dt2.Rows.Add(dr2);

        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    protected void Gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        Load_GVData();
    }
    protected void Gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Load_GVData();
    }
    protected void Gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string m_RowId;
        m_RowId = GridView2.Rows[e.RowIndex].Cells[1].Text;

        M_sqlcmd.CommandText = "DELETE FROM STK_TimeTable WHERE RowId=@m_RowId ";

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("m_RowId", m_RowId);

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_SQLConn.Open();
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
            M_SQLConn.Close();
        }
        Load_GVData();
    }
    protected void Gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        Load_GVData();
    }

    protected void Gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_TimeId,m_TimeName;
        decimal m_E_Hour, m_S_Hour;
        decimal m_Rec_Day;
        int m_RowId;
        string m_errorobject = "";
        //----------------
        try
        {
            m_RowId = Convert.ToInt16(GridView2.Rows[e.RowIndex].Cells[1].Text);
            //m_RowId = Convert.ToInt16(((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbRowId_Edit")).Text);

            m_TimeId = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbTimeId_edit")).Text;
            m_TimeName = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbTimeName_Edit")).Text;

            //if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluelo_Edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluelo_Edit")).Text = "0";
            //if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluehi_Edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluehi_Edit")).Text = "0";

            m_S_Hour = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbS_Hour_edit")).Text);
            m_E_Hour = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbE_Hour_Edit")).Text);

            m_Rec_Day = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbRec_Day_Edit")).Text);

            //驗證資料
            if (string.IsNullOrEmpty(m_TimeId)) { m_errorobject = "班別代號 欄位 "; throw new Exception("不可為空值!!"); }
            if (string.IsNullOrEmpty(m_TimeName)) { m_errorobject = "班別名稱 欄位 "; throw new Exception("不可為空值!!"); }

            if (! (m_S_Hour >= 0 && m_E_Hour >= 0 && m_S_Hour <= 24 && m_E_Hour <= 24 && m_E_Hour > m_S_Hour) )
            {
                m_errorobject = "起算及截止(時)輸入錯誤!!"; throw new Exception("兩者必須大於 0 ，且截止(時)要大於起算(時)");
            }

            if (!(m_Rec_Day>=-1 && m_Rec_Day<=1))
            {
                m_errorobject = "認列日輸入錯誤"; throw new Exception("只可輸入 -1:前一天,0:當天,1:隔天");
            }



        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        try
        { 
            M_sqlcmd.CommandText =
                @"UPDATE STK_TimeTable SET TimeId=@m_TimeId,TimeName=@m_TimeName,S_Hour=@m_S_Hour,E_Hour=@m_E_Hour,Rec_Day=@m_Rec_Day " +
                @"WHERE RowId=@m_RowId ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_RowId", m_RowId);
            M_sqlcmd.Parameters.AddWithValue("m_TimeId", m_TimeId);
            M_sqlcmd.Parameters.AddWithValue("m_TimeName", m_TimeName);
            M_sqlcmd.Parameters.AddWithValue("m_S_Hour", m_S_Hour);
            M_sqlcmd.Parameters.AddWithValue("m_E_Hour", m_E_Hour);
            M_sqlcmd.Parameters.AddWithValue("m_Rec_Day", m_Rec_Day);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView2.EditIndex = -1;

            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
        }
    }
    protected void LbInsert_Click(object sender, EventArgs e)
    {
        GridView2.Columns[1].Visible = false;
        GridView2.FooterRow.Visible = true;
    }
    protected void LbSave_Click(object sender, EventArgs e)
    {
        string m_TimeId, m_TimeName;
        decimal m_E_Hour, m_S_Hour;
        decimal m_Rec_Day;
        string m_errorobject ="";

        //輸入資料驗證
        try
        {
            m_TimeId = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbTimeId_Footer")).Text;
            m_TimeName =  ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbTimeName_Footer")).Text;
            m_S_Hour = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbS_Hour_Footer")).Text);
            m_E_Hour = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbE_Hour_Footer")).Text);
            m_Rec_Day = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbRec_Day_Footer")).Text);

            if (string.IsNullOrEmpty(m_TimeId)) { m_errorobject = "班別代號 欄位 "; throw new Exception("不可為空值!!"); }
            if (string.IsNullOrEmpty(m_TimeName)) { m_errorobject = "班別名稱 欄位 "; throw new Exception("不可為空值!!"); }

            if (! (m_S_Hour >= 0 && m_E_Hour >= 0 && m_S_Hour <= 24 && m_E_Hour <= 24 && m_E_Hour > m_S_Hour) )
            {
                m_errorobject = "起算及截止(時)輸入錯誤!!"; throw new Exception("兩者必須大於 0 ，且截止(時)要大於起算(時)");
            }
            if (!(m_Rec_Day >= -1 && m_Rec_Day <= 1))
            {
                m_errorobject = "認列日輸入錯誤"; throw new Exception("只可輸入 -1:前一天,0:當天,1:隔天");
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        try
        {
            M_sqlcmd.CommandText =
                @"INSERT INTO STK_TimeTable (TimeId,TimeName,S_Hour,E_Hour,Rec_day) " +
                @"Values(@m_TimeId,@m_TimeName,@m_S_Hour,@m_E_Hour,@m_Rec_Day) ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_TimeId", m_TimeId);
            M_sqlcmd.Parameters.AddWithValue("m_TimeName", m_TimeName);
            M_sqlcmd.Parameters.AddWithValue("m_S_Hour", m_S_Hour);
            M_sqlcmd.Parameters.AddWithValue("m_E_Hour", m_E_Hour);
            M_sqlcmd.Parameters.AddWithValue("m_Rec_Day", m_Rec_Day);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }
            GridView2.Columns[1].Visible = true;

            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }

    }

    protected void LbCancelSave_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = false;
        GridView2.Columns[1].Visible = true;

    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Width = 100;
            e.Row.Cells[4].BackColor = System.Drawing.Color.LightBlue;

            e.Row.Cells[5].BackColor = System.Drawing.Color.HotPink;

            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_TimeTable_Edit"))
            {
                e.Row.Cells[0].Visible = false;
            }

        }
    }
    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        Load_GVData();
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

    private void Load_GVData()
    {

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_T_SUP_cmd.Connection = M_SQLConn;

            //M_STK_ChemName_cmd.CommandText = "Select * from STK_ChemData where depa='" + DDL_depa.SelectedValue.ToString() + "'  order by Chemname,drumcode,partno ";
            M_T_SUP_cmd.CommandText = "SELECT * FROM STK_TimeTable order by TimeId,S_Hour ";
            //M_T_SUP_cmd.Parameters.Clear();
            //M_T_SUP_cmd.Parameters.AddWithValue("m_plant", m_plant);
            //M_T_SUP_cmd.Parameters.AddWithValue("m_category", m_category);
            //M_T_SUP_cmd.Parameters.AddWithValue("m_equipname", m_equipname);
            //M_T_SUP_cmd.Parameters.AddWithValue("m_gastype", m_gastype);

            M_da.SelectCommand = M_T_SUP_cmd;
            M_da.Fill(M_ds, "T_SUP_TMP");

            if (M_ds.Tables["T_SUP_TMP"].Rows.Count>0)
            {
                GridView2.DataSource = M_ds.Tables["T_SUP_TMP"];
                GridView2.DataBind();
            }
            else
            {
                AddDefaultFirstRecord();

            }


        }
    }
}