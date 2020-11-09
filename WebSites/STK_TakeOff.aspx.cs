using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_TakeOff : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P3FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_TakeOff_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddDefaultFirstRecord();
            GET_DDL_DATA_DEPA();
            GET_DDL_DATA_CHEMNAME();
            GET_DDL_DATA_WS();
        }
    }
    private void GET_DDL_DATA_CHEMNAME()
    {
        string m_sqlstr = "SELECT '' as Chemname union SELECT DISTINCT Chemname FROM STK_ChemData where depa='" + DDL_depa.SelectedValue.ToString() + "' and ws='" + DDL_WS.SelectedValue.ToString() + "' Order By ChemName Asc ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_ChemName.DataValueField = "Chemname";
                DDL_ChemName.DataTextField = "Chemname";

                DDL_ChemName.DataSource = ddl;
                DDL_ChemName.DataBind();
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
    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "SELECT '' as WS union SELECT DISTINCT ws FROM STK_ChemDAta where depa='" + DDL_depa.SelectedValue.ToString() + "' Order By ws Asc ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_WS.DataValueField = "ws";
                DDL_WS.DataTextField = "ws";

                DDL_WS.DataSource = ddl;
                DDL_WS.DataBind();
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


    private void GET_DDL_DATA_DEPA()
    {
        string m_sqlstr = "SELECT DISTINCT depa as depa FROM STK_ChemData Order By depa ";

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

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_TakeOff_Data";
        dt2.Columns.Add(new DataColumn("ws", typeof(string)));
        dt2.Columns.Add(new DataColumn("chemname", typeof(string)));
        dt2.Columns.Add(new DataColumn("EQName", typeof(string)));
        dt2.Columns.Add(new DataColumn("Using_Date", typeof(DateTime)));
        dt2.Columns.Add(new DataColumn("barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("Drumcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("vendorbatch", typeof(string)));

        dt2.Columns.Add(new DataColumn("ExpirationDate", typeof(string)));
        dt2.Columns.Add(new DataColumn("ExpirationDays", typeof(string)));
        dt2.Columns.Add(new DataColumn("Using_StartTime", typeof(string)));
        dt2.Columns.Add(new DataColumn("Using_EndTime", typeof(string)));


        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        ViewState["T_TakeOff_Data"] = dt2;
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }
    private void Load_GVData()
    {

        string m_querystr = "Select * From STK_USINGDATA WHERE (using_starttime is null or using_endtime is null)   and ";
        if (!string.IsNullOrEmpty(DDL_depa.SelectedValue.ToString())) m_querystr = m_querystr + " chemname IN (Select distinct chemname from stk_chemdata where depa='" + DDL_depa.SelectedValue.ToString().Trim() + "')  and ";
        if (!string.IsNullOrEmpty(DDL_ChemName.SelectedValue.ToString())) m_querystr = m_querystr + " chemname ='" + DDL_ChemName.SelectedValue.ToString().Trim() + "'  and ";
        if (!string.IsNullOrEmpty(DDL_WS.SelectedValue.ToString())) m_querystr = m_querystr + " ws='" + DDL_WS.SelectedValue.ToString().Trim() + "'  and ";

        m_querystr = m_querystr.Substring(0, m_querystr.Length - 5);
        m_querystr = m_querystr + " order by ws,chemname,using_date desc ";
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_TakeOff_cmd.Connection = M_SQLConn;
            M_STK_TakeOff_cmd.CommandText = m_querystr;

            M_da.SelectCommand = M_STK_TakeOff_cmd;
            M_da.Fill(M_ds, "T_TakeOff_Data");
            GridView1.DataSource = M_ds.Tables["T_TakeOff_Data"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        DDL_depa.Enabled = false;
        DDL_ChemName.Enabled = false;
        DDL_WS.Enabled = false;
        Button1.Enabled = false;
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        DDL_depa.Enabled = true;
        DDL_ChemName.Enabled = true;
        DDL_WS.Enabled = true;
        Button1.Enabled = true;

        Load_GVData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Load_GVData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string m_barcode, m_userid, m_drumcode, m_vendorbatch, m_depa, m_expirationdate;
        string m_chemname, m_eqname, m_ws, m_sqlstr;
        string m_usingdate, m_starttime, m_endtime;
        DateTime m_datetime = DateTime.Now;
        int m_expdays = 0;

        m_ws = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblws")).Text;
        m_chemname = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblchemname")).Text;
        m_eqname = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblEQName")).Text;
        m_usingdate = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblusing_date")).Text;
        m_barcode = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblbarcode")).Text;
        m_drumcode = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbldrumcode")).Text;
        m_vendorbatch = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblvendorbatch")).Text;
        m_depa = DDL_depa.SelectedValue.ToString().Trim();
        m_expirationdate = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblexpirationdate")).Text;
        m_starttime = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblusing_starttime")).Text;
        m_endtime = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblusing_endtime")).Text;
        m_userid = Session["LoginName"].ToString();

        //----計算剩餘天數
        if (!String.IsNullOrEmpty(m_expirationdate))
        {
            DateTime sdate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            DateTime edate = DateTime.Parse(m_expirationdate);
            TimeSpan TS_expdays = edate.Subtract(sdate);
            m_expdays = (int)TS_expdays.TotalDays;
        }


        try
        {
            //寫入結束時間
            m_sqlstr = "UPDATE STK_UsingData  SET Using_ENDTime = @m_datetime,Using_StartTime=ISNULL(Using_StartTime,@m_datetime) where ws=@m_ws and chemname=@m_chemname and eqname=@m_eqname and using_date=@m_usingdate ; ";
            //寫入下架紀錄
            m_sqlstr += "INSERT INTO STK_UsingData(using_Date,barcode,ws,chemname,eqname,userid,Drumcode,VendorBatch,Qty,ExpirationDate,ExpirationDays,using_StartTime,using_ENDTime,data_cancel) ";
            m_sqlstr += "Values(@m_datetime,@M_Barcode,@M_ws,@M_ChemName,@M_EQName,@M_Userid,@M_Drumcode,@M_VendorBatch,-1,@M_Exp_Date,@m_expdays,@m_datetime,@m_datetime,0) ; ";

            m_sqlstr += "UPdate STK_Stock SET stk_qty=isnull(stk_qty,0)+1,stk_using_qty=isnull(stk_using_qty,0)-1 where ws=@m_ws and barcode=@m_barcode; ";
            m_sqlstr += "IF @@ROWCOUNT=0 ";
            m_sqlstr += "Insert into STK_Stock (ws,barcode,drumcode,vendorbatch,ExpirationDate,stk_in_qty,stk_out_qty,stk_using_qty,stk_adj_qty,stk_qty) values(@m_ws,@m_barcode,@m_drumcode,@m_vendorbatch,@M_Exp_Date,0,0,-1,0,1); ";
            //異動庫存，下架的庫存直接移到'Z9999'
            //1.下架至原本庫位
            //2.新增一筆庫位轉移至 'Z9999' (一筆由原庫位轉出，一筆轉入'Z9999')
            m_sqlstr += "Insert into STK_Transfer(data_type,ws,trans_datetime,barcode,drumcode,vendorbatch,expirationdate,userid,qty,data_cancel) ";
            m_sqlstr += "Values('O',@M_WS,@m_datetime,@M_Barcode,@M_Drumcode,@M_VendorBatch,@M_Exp_Date,@M_Userid,1,0); ";
            m_sqlstr += "UPDATE STK_STOCK SET STK_QTY=ISNULL(STK_QTY,0)-1,STK_Transfer_OUT_QTY=ISNULL(STK_Transfer_OUT_QTY,0)+1 where ws=@m_ws and barcode=@m_barcode; ";
            m_sqlstr += "Insert into STK_Transfer(data_type,ws,trans_datetime,barcode,drumcode,vendorbatch,expirationdate,userid,qty,data_cancel) ";
            m_sqlstr += "Values('I','Z9999',@m_datetime,@M_Barcode,@M_Drumcode,@M_VendorBatch,@M_Exp_Date,@M_Userid,1,0); ";
            m_sqlstr += "update STK_STOCK SET STK_QTY=ISNULL(STK_QTY,0)+1 ,STK_Transfer_IN_QTY=ISNULL(STK_Transfer_IN_QTY,0)+1 where ws='Z9999' and barcode=@m_barcode; ";
            m_sqlstr += "IF @@ROWCOUNT=0 ";
            m_sqlstr += "Insert into STK_Stock (ws,barcode,Drumcode,vendorbatch,ExpirationDate,stk_in_qty,stk_out_qty,stk_using_qty,stk_adj_qty,STK_Transfer_IN_QTY,STK_Transfer_OUT_QTY,stk_qty) values('Z9999',@m_barcode,@m_drumcode,@m_vendorbatch,@M_Exp_Date,0,0,0,0,1,0,1); ";

            //M_sqlcmd.CommandType = CommandType.Text;
            M_sqlcmd.CommandText = m_sqlstr;
            M_sqlcmd.Parameters.Clear();

            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("M_Exp_Date", m_expirationdate);
            M_sqlcmd.Parameters.AddWithValue("m_datetime", m_datetime);
            M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);
            M_sqlcmd.Parameters.AddWithValue("m_eqname", @m_eqname);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", @m_barcode);
            M_sqlcmd.Parameters.AddWithValue("m_drumcode", @m_drumcode);
            M_sqlcmd.Parameters.AddWithValue("m_vendorbatch", m_vendorbatch);
            M_sqlcmd.Parameters.AddWithValue("m_usingdate", m_usingdate);
            M_sqlcmd.Parameters.AddWithValue("M_Userid", m_userid);
            M_sqlcmd.Parameters.AddWithValue("m_expdays", m_expdays);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView1.EditIndex = -1;
            DDL_depa.Enabled = true;
            DDL_ChemName.Enabled = true;
            DDL_WS.Enabled = true;
            Button1.Enabled = true;
            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }

    }

    protected void DDL_depa_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_CHEMNAME();
        GET_DDL_DATA_WS();
    }
    protected void DDL_WS_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_CHEMNAME();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_TAKEOFF"))
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