using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class T_SUP_WarningValue_Edit : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_T_SUP_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    public string m_key_node, m_key_system, m_key_valve, m_key_pou, m_key_tagname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddDefaultFirstRecord();
            GET_DDL_DATA_NODE();
            GET_DDL_DATA_SYSTEM();
        }
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_SUP_TMP";
        dt2.Columns.Add(new DataColumn("F_NODE_NAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("F_SYSTEM_NAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("F_VALVE_NAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("F_POU_NAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("F_Tagname", typeof(string)));
        dt2.Columns.Add(new DataColumn("Times_Value", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("Second_Value", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("AREANAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("PU_NAME", typeof(string)));
        dt2.Columns.Add(new DataColumn("chk_enable", typeof(bool)));

        dr2 = dt2.NewRow();
        dr2["chk_enable"] = 0;
        dt2.Rows.Add(dr2);
        ViewState["T_SUP_TMP"] = dt2;
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        Load_GVData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Load_GVData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string m_nodename, m_systemname, m_valvename, m_pouname, m_tagname;

        m_nodename = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblnode")).Text;
        m_systemname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblsystem")).Text;
        m_valvename = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblvalve")).Text;
        m_pouname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblpu")).Text;
        m_tagname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbltagname")).Text;

        M_sqlcmd.CommandText = "DELETE FROM T_SUP_WarningValue WHERE F_NODE_NAME=@m_nodename and F_SYSTEM_NAME=@m_systemname and F_valve_NAME=@m_valvename and F_POU_NAME=@m_pouname and F_Tagname=@m_tagname ";

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("m_nodename", m_nodename);
        M_sqlcmd.Parameters.AddWithValue("m_systemname", m_systemname);
        M_sqlcmd.Parameters.AddWithValue("m_valvename", m_valvename);
        M_sqlcmd.Parameters.AddWithValue("m_pouname", m_pouname);
        M_sqlcmd.Parameters.AddWithValue("m_tagname", m_tagname);

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_SQLConn.Open();
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
            M_SQLConn.Close();
        }
        Load_GVData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        Load_GVData();
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_nodename, m_systemname, m_valvename, m_pouname, m_tagname;
        string m_times, m_seconds;
        string m_areaname, m_puname;
        bool m_chk;

        //----------------
        try
        {
            m_nodename = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblnode")).Text;
            m_systemname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblsystem")).Text;
            m_valvename = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblvalve")).Text;
            m_pouname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblpou")).Text;
            m_tagname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbltagname")).Text;
            m_times = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbtimes_Edit")).Text;
            m_seconds = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbsecond_Edit")).Text;
            m_areaname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbareaname_Edit")).Text;
            m_puname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbpuname_Edit")).Text;
            m_chk = ((CheckBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("cbenable_Edit")).Checked;

            M_sqlcmd.CommandText =
                @"UPDATE T_SUP_WarningValue SET Times_Value=@m_times,Second_Value=@m_seconds,AREANAME=@m_areaname,PU_NAME=@m_puname,chk_enable=@m_chk " +
                @"WHERE F_NODE_NAME=@m_nodename and F_SYSTEM_NAME=@m_systemname and F_VALVE_NAME=@m_valvename and F_POU_NAME=@m_pouname and F_Tagname=@m_tagname ";
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_nodename", m_nodename);
            M_sqlcmd.Parameters.AddWithValue("m_systemname", m_systemname);
            M_sqlcmd.Parameters.AddWithValue("m_valvename", m_valvename);
            M_sqlcmd.Parameters.AddWithValue("m_pouname", m_pouname);
            M_sqlcmd.Parameters.AddWithValue("m_tagname", m_tagname);
            M_sqlcmd.Parameters.AddWithValue("m_times", m_times);
            M_sqlcmd.Parameters.AddWithValue("m_seconds", m_seconds);
            M_sqlcmd.Parameters.AddWithValue("m_areaname", m_areaname);
            M_sqlcmd.Parameters.AddWithValue("m_puname", m_puname);
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
        
        
    }
    protected void lbInsert_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = true;
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        string m_nodename, m_systemname, m_valvename, m_pouname, m_tagname;
        string m_times, m_seconds;
        string m_areaname, m_puname;
        bool m_chk;
        string m_errorobject="";

        //輸入資料驗證
        try
        {

            m_nodename = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbnode_Footer")).Text;
            m_systemname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbsystem_Footer")).Text;
            m_valvename = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbvalve_Footer")).Text;
            m_pouname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbpou_Footer")).Text;
            m_tagname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbtagname_Footer")).Text;
            m_times = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbtimes_Footer")).Text;
            m_seconds = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbsecond_Footer")).Text;
            m_areaname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbAreaName_Footer")).Text;
            m_puname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbPUName_Footer")).Text;
            m_chk = ((CheckBox)GridView2.FooterRow.Cells[0].FindControl("cbenable_footer")).Checked;


            if (string.IsNullOrEmpty(m_nodename)) { m_errorobject = "NODE 欄位 "; throw new Exception("不可為空值!!"); }

            if (string.IsNullOrEmpty(m_systemname)) { m_errorobject = "SYSTEM 欄位 "; throw new Exception("不可為空值!!"); }

            if (string.IsNullOrEmpty(m_pouname)) { m_errorobject = "POU 欄位 "; throw new Exception("不可為空值!!"); ; }

            if (string.IsNullOrEmpty(m_tagname)) { m_errorobject = "TAGNAME 欄位 "; throw new Exception("不可為空值!!"); }

            if (string.IsNullOrEmpty(m_areaname)) { m_errorobject = "AREANAME 欄位 "; throw new Exception("不可為空值!!"); }

            if (string.IsNullOrEmpty(m_puname)) { m_errorobject = "PU 欄位 "; throw new Exception("不可為空值!!"); }

            if (string.IsNullOrEmpty(m_valvename)) { m_valvename = ""; }
            if (string.IsNullOrEmpty(m_times)) { m_times = "0"; }
            if (string.IsNullOrEmpty(m_seconds)) { m_seconds = "0"; }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }


        try
        {
            M_sqlcmd.CommandText =
                @"Insert into T_SUP_WarningValue (F_NODE_NAME,F_SYSTEM_NAME,F_VALVE_NAME,F_POU_NAME,F_Tagname,Times_Value,Second_Value,AREANAME,PU_NAME,chk_enable)" +
                @"Values( @m_nodename,@m_systemname,@m_valvename,@m_pouname,@m_tagname,@m_times,@m_seconds,@m_areaname,@m_puname,@m_chk) ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_nodename", m_nodename);
            M_sqlcmd.Parameters.AddWithValue("m_systemname", m_systemname);
            M_sqlcmd.Parameters.AddWithValue("m_valvename", m_valvename);
            M_sqlcmd.Parameters.AddWithValue("m_pouname", m_pouname);
            M_sqlcmd.Parameters.AddWithValue("m_tagname", m_tagname);
            M_sqlcmd.Parameters.AddWithValue("m_times", m_times);
            M_sqlcmd.Parameters.AddWithValue("m_seconds", m_seconds);
            M_sqlcmd.Parameters.AddWithValue("m_areaname", m_areaname);
            M_sqlcmd.Parameters.AddWithValue("m_puname", m_puname);
            M_sqlcmd.Parameters.AddWithValue("m_chk", m_chk);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }

    }

    protected void lbCancelSave_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = false;
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Width = 100;
            //e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
            //e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "T_SUP_WarningValue_Edit"))
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
        string m_nodename, m_systemname, m_valvename, m_pouname;
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            m_nodename = DDL_NODE.SelectedValue.ToString();
            m_systemname = "%" + DDL_SYSTEM.SelectedValue.ToString().Trim() + "%";
            m_valvename = "%" + DDL_VALVE.SelectedValue.ToString().Trim() + "%";
            m_pouname = "%" + DDL_POU.SelectedValue.ToString().Trim() + "%";

            M_T_SUP_cmd.Connection = M_SQLConn;

            //M_STK_ChemName_cmd.CommandText = "Select * from STK_ChemData where depa='" + DDL_depa.SelectedValue.ToString() + "'  order by Chemname,drumcode,partno ";
            M_T_SUP_cmd.CommandText = "Select * from T_SUP_WARNINGVALUE where F_NODE_NAME=@m_nodename and F_SYSTEM_NAME like @m_systemname and F_VALVE_NAME like @m_valvename and F_POU_NAME like @m_pouname order by F_NODE_NAME,F_SYSTEM_NAME,F_VALVE_NAME,F_POU_NAME,F_Tagname ";
            M_T_SUP_cmd.Parameters.Clear();
            M_T_SUP_cmd.Parameters.AddWithValue("m_nodename", m_nodename);
            M_T_SUP_cmd.Parameters.AddWithValue("m_systemname", m_systemname);
            M_T_SUP_cmd.Parameters.AddWithValue("m_valvename", m_valvename);
            M_T_SUP_cmd.Parameters.AddWithValue("m_pouname", m_pouname);

            M_da.SelectCommand = M_T_SUP_cmd;
            M_da.Fill(M_ds, "T_SUP_TMP");
            GridView2.DataSource = M_ds.Tables["T_SUP_TMP"];
            GridView2.DataBind();
        }
    }
    private void GET_DDL_DATA_NODE()
    {
        string m_sqlstr = "Select distinct f_node_name from T_SUP_WarningValue order by f_node_name";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_NODE.DataValueField = "f_node_name";
                DDL_NODE.DataTextField = "f_node_name";

                DDL_NODE.DataSource = ddl;
                DDL_NODE.DataBind();
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
    private void GET_DDL_DATA_SYSTEM()
    {
        string m_sqlstr = "Select distinct f_system_name from T_SUP_WarningValue where F_NODE_NAME=@m_nodename order by f_system_name";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;
                dropdownlist_cmd.Parameters.Clear();
                dropdownlist_cmd.Parameters.AddWithValue("m_nodename", DDL_NODE.SelectedValue.ToString());

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_SYSTEM.DataValueField = "f_system_name";
                DDL_SYSTEM.DataTextField = "f_system_name";

                DDL_SYSTEM.DataSource = ddl;
                DDL_SYSTEM.DataBind();
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

    protected void DDL_NODE_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_SYSTEM();
    }

    private void GET_DDL_DATA_VALVE()
    {
        string m_sqlstr = "Select distinct f_valve_name from T_SUP_WarningValue where F_NODE_NAME=@m_nodename and F_SYSTEM_NAME=@m_systemname order by f_valve_name";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;
                dropdownlist_cmd.Parameters.Clear();
                dropdownlist_cmd.Parameters.AddWithValue("m_nodename", DDL_NODE.SelectedValue.ToString());
                dropdownlist_cmd.Parameters.AddWithValue("m_systemname", DDL_SYSTEM.SelectedValue.ToString());

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_VALVE.DataValueField = "f_valve_name";
                DDL_VALVE.DataTextField = "f_valve_name";

                DDL_VALVE.DataSource = ddl;
                DDL_VALVE.DataBind();
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

    protected void DDL_SYSTEM_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_VALVE();
    }
    protected void DDL_VALVE_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_POU();
    }

    private void GET_DDL_DATA_POU()
    {
        string m_sqlstr = "Select distinct f_pou_name from T_SUP_WarningValue where F_NODE_NAME=@m_nodename and F_SYSTEM_NAME=@m_systemname and F_VALVE_NAME=@m_valvename order by f_pou_name";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;
                dropdownlist_cmd.Parameters.Clear();
                dropdownlist_cmd.Parameters.AddWithValue("m_nodename", DDL_NODE.SelectedValue.ToString());
                dropdownlist_cmd.Parameters.AddWithValue("m_systemname", DDL_SYSTEM.SelectedValue.ToString());
                dropdownlist_cmd.Parameters.AddWithValue("m_valvename", DDL_VALVE.SelectedValue.ToString());

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_POU.DataValueField = "f_pou_name";
                DDL_POU.DataTextField = "f_pou_name";

                DDL_POU.DataSource = ddl;
                DDL_POU.DataBind();
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

}