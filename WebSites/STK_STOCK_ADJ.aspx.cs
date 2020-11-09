using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_STOCK_ADJ : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_StockADJ_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddDefaultFirstRecord();
            GET_DDL_DATA_WS();
            GET_DDL_DATA_CHEMNAME();
        }
    }
    private void GET_DDL_DATA_CHEMNAME()
    {
        string m_sqlstr = "SELECT '' as Chemname union SELECT DISTINCT Chemname FROM STK_ChemDAta where depa in('SDS','CDS') and  ws='" + DDL_WS.SelectedValue.ToString() + "' Order By ChemName Asc ";

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
        string m_sqlstr = "SELECT wsno as ws from stk_ws ";

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


    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_STOCK_Data";
        dt2.Columns.Add(new DataColumn("DrumCode", typeof(string)));
        dt2.Columns.Add(new DataColumn("Vendorbatch", typeof(string)));
        dt2.Columns.Add(new DataColumn("ws", typeof(string)));
        dt2.Columns.Add(new DataColumn("barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("expirationdate", typeof(string)));
        dt2.Columns.Add(new DataColumn("stk_in_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("stk_out_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("stk_using_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("stk_adj_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("STK_Transfer_IN_QTY", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("STK_Transfer_OUT_QTY", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("stk_qty", typeof(decimal)));


        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        ViewState["T_STOCK_Data"] = dt2;
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }
    private void Load_GVData()
    {
        
        //string m_querystr = "Select a.* from STK_Stock as a left join (Select depa,ws,chemname,drumcode,partno from stk_chemdata) as b on a.ws=b.ws and a.partno=b.partno Where ";
        //if ( ! string.IsNullOrEmpty(DDL_depa.SelectedValue.ToString())) m_querystr = m_querystr + " b.depa='" + DDL_depa.SelectedValue.ToString().Trim() + "' and ";
        //if ( ! string.IsNullOrEmpty(DDL_ChemName.SelectedValue.ToString())) m_querystr = m_querystr + " b.partno='" + DDL_ChemName.SelectedValue.ToString().Trim() + "' and ";
        string m_querystr = "Select a.* from STK_Stock as a Where ";
        if (!string.IsNullOrEmpty(DDL_ChemName.SelectedValue.ToString()))
        {
            m_querystr += " a. Drumcode IN (Select distinct Drumcode from stk_chemdata where chemname like '%" + DDL_ChemName.SelectedValue.ToString().Trim() + "%') and ";
        }
        if ( ! string.IsNullOrEmpty(DDL_WS.SelectedValue.ToString())) m_querystr +=  " a.ws='" + DDL_WS.SelectedValue.ToString().Trim() + "' and ";
        if ( ! string.IsNullOrEmpty(TXB_Vendorbatch.Text)) m_querystr += " a.vendorbatch like '%" + TXB_Vendorbatch.Text.Trim() + "%' and ";

        m_querystr = m_querystr.Substring(0, m_querystr.Length - 5);
        m_querystr = m_querystr + " order by DrumCode,Vendorbatch ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_StockADJ_cmd.Connection = M_SQLConn;
            M_STK_StockADJ_cmd.CommandText = m_querystr;

            M_STK_StockADJ_cmd.Parameters.Clear();
            //M_STK_StockADJ_cmd.Parameters.AddWithValue("m_depa", DDL_depa.SelectedValue.ToString());
            M_da.SelectCommand = M_STK_StockADJ_cmd;
            M_da.Fill(M_ds, "DT_STK_STockADJ");
            GridView1.DataSource = M_ds.Tables["DT_STK_STockADJ"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
//        DDL_depa.Enabled = false;
        DDL_ChemName.Enabled = false;
//        DDL_DrumCode.Enabled = false;
        TXB_Vendorbatch.Enabled = false;
        Button1.Enabled = false;
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
//        DDL_depa.Enabled = true;
        DDL_ChemName.Enabled = true;
//        DDL_DrumCode.Enabled = true;
        TXB_Vendorbatch.Enabled = true;
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
        string m_barcode, m_userid, m_adjdesc,m_drumcode,m_vendorbatch,m_depa,m_expirationdate;
        string m_ws;
        string m_errorobject = "";
        decimal m_adjqty;
        try
        {
            //m_errorobject = "調整數量 ";
            m_adjqty = Convert.ToDecimal(((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_adjqty")).Text);

            m_adjdesc = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_desc")).Text;
            if (string.IsNullOrEmpty(m_adjdesc))
            {
                m_errorobject = "調整描述輸入錯誤 ";
                throw new Exception("不可以為空白 !!");
            }

            m_ws = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblws")).Text;
            m_barcode = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblbarcode")).Text;
            m_drumcode = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbldrumcode")).Text;
            m_vendorbatch = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblvendorbatch")).Text;
            //m_depa = DDL_depa.SelectedValue.ToString().Trim();
            m_expirationdate = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblexpirationdate")).Text;
            m_userid = "";// Session["LoginName"].ToString();

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }
        
        try
        {

            M_sqlcmd.CommandText = "Insert into stk_adj_log (ws,adj_datetime,barcode,drumcode,vendorbatch,expirationdate,adj_qty,adj_desc,adj_userid,data_cancel) Values(@m_ws,@m_datetime,@m_barcode,@m_drumcode,@m_vendorbatch,@m_expirationdate,@m_adj_qty,@m_adj_desc,@m_adj_userid,0 ) " +
               @"UPDATE STK_Stock SET STK_QTY=STK_QTY+@m_adj_qty,STK_ADJ_QTY=STK_ADJ_QTY+@m_adj_qty where ws=@m_ws and barcode=@m_barcode ;";
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_datetime", DateTime.Now);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", m_barcode);
            M_sqlcmd.Parameters.AddWithValue("m_drumcode", m_drumcode);
            M_sqlcmd.Parameters.AddWithValue("m_vendorbatch", m_vendorbatch);
            M_sqlcmd.Parameters.AddWithValue("m_adj_qty", m_adjqty);
            M_sqlcmd.Parameters.AddWithValue("m_adj_desc", m_adjdesc);
            M_sqlcmd.Parameters.AddWithValue("m_adj_userid", m_userid);
            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
//            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("m_expirationdate", m_expirationdate);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView1.EditIndex = -1;
//            DDL_depa.Enabled = true;
            DDL_ChemName.Enabled = true;
//            DDL_DrumCode.Enabled = true;
            TXB_Vendorbatch.Enabled = true;
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

            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_STOCK_ADJ"))
            {
                e.Row.Cells[12].Visible = false;
            }

        }

    }
}