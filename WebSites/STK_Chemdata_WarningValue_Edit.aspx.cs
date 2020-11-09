using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_Chemdata_WarningValue_Edit : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_T_STK_cmd = new SqlCommand();
    private SqlCommand M_valuetypecmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    DataTable M_ddl_ws = new DataTable();
    DataTable M_ddl_chemname = new DataTable();

    //public string m_key_node, m_key_system, m_key_valve, m_key_pou, m_key_tagname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!M_ds.Tables.Contains("T_DDL_chemname"))
            {
                GET_DDL_DATA_CHEMNAME();
            }
            if (!M_ds.Tables.Contains("T_DDL_ws"))
            {
                GET_DDL_DATA_WS();
            }

            AddDefaultFirstRecord();
        }
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_STK_TMP";

        dt2.Columns.Add(new DataColumn("ws", typeof(string)));
        dt2.Columns.Add(new DataColumn("Chemname", typeof(string)));

        dt2.Columns.Add(new DataColumn("Warning_Value_Day", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("Warning_Value_Month", typeof(decimal)));

        dt2.Columns.Add(new DataColumn("rec_enable", typeof(bool)));

        dr2 = dt2.NewRow();
        dr2["rec_enable"] = 0;
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
        DDL_depa.Enabled = true;
        Btn_ok.Enabled = true;
        Load_GVData();
    }
    protected void Gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string m_depa,m_ws,m_chemname;

        m_depa = DDL_depa.SelectedValue.ToString() ;
        m_ws = ((DropDownList)GridView2.Rows[e.RowIndex].Cells[0].FindControl("ddlws")).SelectedValue;
        m_chemname = ((DropDownList)GridView2.Rows[e.RowIndex].Cells[0].FindControl("ddlchemname")).SelectedValue;

        M_sqlcmd.CommandText = "DELETE FROM STK_ChemData_WarningValue WHERE depa=@m_depa and ws=@m_ws and chemname=@m_chemname ";

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
        M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
        M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);

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
        DDL_depa.Enabled = false;
        Btn_ok.Enabled = false;
        Load_GVData();
    }

    protected void Gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_depa,m_ws,m_chemname;
        decimal m_Warning_Value_Day, m_Warning_Value_Month;

        bool m_chk;
        string m_errorobject = "";
        //----------------
        try
        {
            m_depa = DDL_depa.SelectedValue.ToString();
            m_ws = ((DropDownList)GridView2.Rows[e.RowIndex].Cells[0].FindControl("ddlws")).SelectedValue;
            m_chemname = ((DropDownList)GridView2.Rows[e.RowIndex].Cells[0].FindControl("ddlchemname")).SelectedValue;

            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Day_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Day_edit")).Text = "0";
            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Month_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Month_edit")).Text = "0";

            m_Warning_Value_Day = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Day_edit")).Text);
            m_Warning_Value_Month = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbWarning_Value_Month_edit")).Text);

            m_chk = ((CheckBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("cbenable_Edit")).Checked;


            //驗證資料
            //if (m_warningvalue_hi > 0 && m_warningvalue_lo > 0 && m_warningvalue_lo >= m_warningvalue_hi)
            //{
            //    m_errorobject = "警戒值輸入錯誤!!"; throw new Exception("警戒值LO 不可大於 警戒值HI");
            //}
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        try
        { 
            M_sqlcmd.CommandText =
                @"UPDATE STK_ChemData_WarningValue set Warning_Value_Day=@m_Warning_Value_Day,Warning_Value_Month=@m_Warning_Value_Month,rec_enable=@m_chk " +
                @"WHERE depa=@m_depa and ws=@m_ws and chemname=@m_chemname ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);

            M_sqlcmd.Parameters.AddWithValue("m_Warning_Value_Day", m_Warning_Value_Day);
            M_sqlcmd.Parameters.AddWithValue("m_Warning_Value_Month", m_Warning_Value_Month);

            M_sqlcmd.Parameters.AddWithValue("m_chk", m_chk);

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
        DDL_depa.Enabled = true;

        Btn_ok.Enabled = true;
        
    }
    protected void LbInsert_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = true;
        DDL_depa.Enabled = false;

        Btn_ok.Enabled = false;

        //dropdownlist ws,chemname綁定數據
        if (!M_ds.Tables.Contains("T_DDL_ws"))
        {
            GET_DDL_DATA_WS();
        }
        if (!M_ds.Tables.Contains("T_DDL_chemname"))
        {
            GET_DDL_DATA_CHEMNAME();
        }
        DropDownList ws_ddl = (DropDownList)GridView2.FooterRow.FindControl("ddlws");
        ws_ddl.DataSource = M_ds.Tables["T_DDL_ws"].DefaultView;
        ws_ddl.DataValueField = M_ds.Tables["T_DDL_ws"].Columns[0].ToString();
        ws_ddl.DataTextField = M_ds.Tables["T_DDL_ws"].Columns[0].ToString();
        ws_ddl.DataBind();

        DropDownList chemname_ddl = (DropDownList)GridView2.FooterRow.FindControl("ddlchemname");
        chemname_ddl.DataSource = M_ds.Tables["T_DDL_chemname"].DefaultView;
        chemname_ddl.DataValueField = M_ds.Tables["T_DDL_chemname"].Columns[0].ToString();
        chemname_ddl.DataTextField = M_ds.Tables["T_DDL_chemname"].Columns[0].ToString();
        chemname_ddl.DataBind();
    }
    protected void LbSave_Click(object sender, EventArgs e)
    {
        string m_depa, m_ws, m_chemname;
        decimal m_Warning_Value_Day, m_Warning_Value_Month;

        bool m_chk;
        string m_errorobject = "";

        //輸入資料驗證
        try
        {

            m_depa = DDL_depa.SelectedValue.ToString();
            m_ws = ((DropDownList)GridView2.FooterRow.Cells[0].FindControl("ddlws")).SelectedValue;
            m_chemname = ((DropDownList)GridView2.FooterRow.Cells[0].FindControl("ddlchemname")).SelectedValue;

            if (string.IsNullOrEmpty(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_Day_Footer")).Text)) ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_Day_Footer")).Text = "0";
            if (string.IsNullOrEmpty(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_Month_Footer")).Text)) ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_Month_Footer")).Text = "0";

            m_Warning_Value_Day = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_Day_Footer")).Text);
            m_Warning_Value_Month = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbWarning_Value_month_Footer")).Text);

            m_chk = ((CheckBox)GridView2.FooterRow.Cells[0].FindControl("cbenable_footer")).Checked;

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }


        try
        {
            M_sqlcmd.CommandText =
                @"INSERT INTO STK_ChemData_WarningValue (depa,ws,chemname,warning_value_day,warning_value_month,rec_enable) " +
                @"Values(@m_depa,@m_ws,@m_chemname,@m_Warning_Value_Day, @m_Warning_Value_Month,@m_chk) ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);
            M_sqlcmd.Parameters.AddWithValue("m_Warning_Value_Day", m_Warning_Value_Day);
            M_sqlcmd.Parameters.AddWithValue("m_Warning_Value_Month", m_Warning_Value_Month);
            M_sqlcmd.Parameters.AddWithValue("m_chk", m_chk);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }


        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" +  ex.Message + "');</script>");

            Response.Write("<script>alert(' 資料存檔錯誤，請檢查 "+ex.Message+" ');</script>");
            return;
        }
        DDL_depa.Enabled = true;
        Btn_ok.Enabled = true;
        Load_GVData();

    }

    protected void lbCancelSave_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = false;
        DDL_depa.Enabled = true;
        Btn_ok.Enabled = true;
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Width = 100;
            e.Row.Cells[4].BackColor = System.Drawing.Color.LightBlue;
            e.Row.Cells[5].BackColor = System.Drawing.Color.LightBlue;

            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_Chemdata_WarningValue_Edit"))
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

    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }

    private void Load_GVData()
    {
        string m_depa;
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            m_depa = DDL_depa.SelectedValue.ToString().Trim();

            M_T_STK_cmd.Connection = M_SQLConn;

            M_T_STK_cmd.CommandText = "SELECT * FROM STK_ChemData_WarningValue where depa=@m_depa order by depa,ws,chemname ";
            M_T_STK_cmd.Parameters.Clear();
            M_T_STK_cmd.Parameters.AddWithValue("m_depa", m_depa);

            M_da.SelectCommand = M_T_STK_cmd;
            M_da.Fill(M_ds, "T_STK_TMP");
            GridView2.DataSource = M_ds.Tables["T_STK_TMP"];
            GridView2.DataBind();
        }
    }

    private void GET_DDL_DATA_WS()
    {
        string m_depa = DDL_depa.SelectedValue.ToString().Trim();

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                DDL_strCon.Open();
                SqlDataAdapter da = new SqlDataAdapter("select distinct ws from stk_chemdata where depa ='"+m_depa+"' order by ws ", DDL_strCon);
                da.Fill(M_ds, "T_DDL_ws");
                da.Dispose();
                
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
    private void GET_DDL_DATA_CHEMNAME()
    {
        string m_depa = DDL_depa.SelectedValue.ToString().Trim();

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                DDL_strCon.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select distinct chemname from stk_chemdata where depa='"+m_depa+"' order by chemname", DDL_strCon);
                da.Fill(M_ds, "T_DDL_chemname");
                da.Dispose();

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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Gridview2_DDL_BindData(e);
        }
    }

    private void Gridview2_DDL_BindData(GridViewRowEventArgs e)
    {
        if (!M_ds.Tables.Contains("T_DDL_chemname"))
        {
            GET_DDL_DATA_CHEMNAME();
        }
        if (!M_ds.Tables.Contains("T_DDL_ws"))
        {
            GET_DDL_DATA_WS();
        }
        DropDownList ws_ddl = (DropDownList)e.Row.Cells[2].FindControl("ddlws");
        ws_ddl.DataSource = M_ds.Tables["T_DDL_ws"].DefaultView;

        ws_ddl.DataValueField = M_ds.Tables["T_DDL_ws"].Columns[0].ToString();
        ws_ddl.DataTextField = M_ds.Tables["T_DDL_ws"].Columns[0].ToString();
        ws_ddl.DataBind();
        if (!string.IsNullOrEmpty(((Label)e.Row.Cells[2].FindControl("lbws")).Text))
        {
            ws_ddl.Items.FindByValue(((Label)e.Row.Cells[2].FindControl("lbws")).Text).Selected = true;
        }

        DropDownList chemname_ddl = (DropDownList)e.Row.Cells[3].FindControl("ddlchemname");
        chemname_ddl.DataSource = M_ds.Tables["T_DDL_chemname"].DefaultView;
        chemname_ddl.DataValueField = M_ds.Tables["T_DDL_chemname"].Columns[0].ToString();
        chemname_ddl.DataTextField = M_ds.Tables["T_DDL_chemname"].Columns[0].ToString();
        chemname_ddl.DataBind();
        if (!string.IsNullOrEmpty(((Label)e.Row.Cells[3].FindControl("lbchemname")).Text))
        {
            chemname_ddl.Items.FindByValue(((Label)e.Row.Cells[3].FindControl("lbchemname")).Text).Selected = true;
        }

    }

}