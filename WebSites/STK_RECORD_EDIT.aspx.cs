using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_RECORD_EDIT : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;

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
        }
    }
    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "SELECT DISTINCT ws FROM STK_STOCK Order By ws Asc ";

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
        dt2.TableName = "T_RECORD_Data";
        dt2.Columns.Add(new DataColumn("data_type", typeof(string)));
        dt2.Columns.Add(new DataColumn("serialno", typeof(string)));
        dt2.Columns.Add(new DataColumn("seq", typeof(int)));
        dt2.Columns.Add(new DataColumn("date", typeof(string)));
        dt2.Columns.Add(new DataColumn("datetime", typeof(DateTime)));
        dt2.Columns.Add(new DataColumn("ws", typeof(string)));
        dt2.Columns.Add(new DataColumn("userid", typeof(string)));
        dt2.Columns.Add(new DataColumn("barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("partno", typeof(string)));
        dt2.Columns.Add(new DataColumn("vendorbatch", typeof(string)));
        dt2.Columns.Add(new DataColumn("in_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("out_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("using_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("takeoff_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("tranin_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("tranout_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("adj_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("stk_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("descr", typeof(string)));
        dt2.Columns.Add(new DataColumn("data_cancel", typeof(bool)));

        dr2 = dt2.NewRow();
        dr2["data_cancel"] = 0;
        dt2.Rows.Add(dr2);
        ViewState["T_RECORD_Data"] = dt2;
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }
    private void Load_GVData()
    {

        string m_querystr = "";
        m_querystr += "Select y.*,sum(case when data_cancel=0 then in_qty-out_qty-using_qty+takeoff_qty+tranin_qty-tranout_qty+adj_qty else 0 end) OVER(PARTITION BY ws,partno,vendorbatch ORDER BY datetime,serialno,seq) as STK_QTY from ( ";
        m_querystr += "Select'using' as data_type,Chemname as serialno,EQName as seq,convert(varchar(10),using_date,111) as date, Using_Date as datetime,ws,Userid,barcode,partno,VendorBatch,0 as in_qty,0 as out_qty,case when a.Qty>0 then a.Qty else 0 end as using_qty,case when a.Qty<0 then abs(a.Qty) else 0 end as takeoff_qty,0 as tranin_qty,0 as tranout_qty,0 as adj_qty,'' as descr ,data_cancel from STK_UsingData as a where a.vendorbatch=@m_vendorbatch and a.ws=@m_ws ";
        m_querystr += "union all ";
        m_querystr += "Select 'arrival' as data_type,serialno as serialno,LTRIM(str(seq)) as seq,arrival_date as date,timestamp as datetime,ws,userid,barcode,partno,vendorbatch,case when arrival_type='I' then qty else 0 end as in_qty,case when arrival_type='O' then qty else 0 end as out_qty,0 as using_qty,0 as takeoff_qty,0 as tranin_qty,0 as tranout_qty,0 as adj_qty,'' as descr,data_cancel from stk_arrival as a where a.vendorbatch=@m_vendorbatch and a.ws=@m_ws ";
        m_querystr += "union all ";
        m_querystr += "Select 'adjust' as data_type,'' as serialno,'' as seq,convert(varchar(10),adj_datetime,111) as date,adj_datetime as datetime,ws,adj_userid as userid,barcode,partno,vendorbatch,0 as in_qty,0 as out_qty,0 as using_qty,0 as takeoff_qty,0 as tranin_qty ,0 as tranout_qty,adj_qty as adj_qty,adj_desc as descr, data_cancel from STK_ADJ_Log as a where a.vendorbatch=@m_vendorbatch and a.ws=@m_ws ";
        m_querystr += "union ";
        m_querystr += "select 'trans' as data_type,'' as serialno,'' as seq,convert(varchar(10),trans_datetime,111) as date,trans_datetime as datetime,ws,userid,barcode,partno,vendorbatch,0 as in_qty,0 as out_qty,0 as using_qty,0 as takeoff_qty,case when data_type='I' then qty else 0 end as tranin_qty,case when data_type='O' then qty else 0 end as tranout_qty,0 as adj_qty,'' as descr,data_cancel from STK_Transfer as a where a.vendorbatch=@m_vendorbatch and a.ws=@m_ws ";
        m_querystr += ") as y order by datetime asc ";
        
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_StockADJ_cmd.Connection = M_SQLConn;
            M_STK_StockADJ_cmd.CommandText = m_querystr;

            M_STK_StockADJ_cmd.Parameters.Clear();

            M_STK_StockADJ_cmd.Parameters.AddWithValue("m_ws", DDL_WS.SelectedValue.ToString());
            M_STK_StockADJ_cmd.Parameters.AddWithValue("m_vendorbatch", TXB_Vendorbatch.Text);

            M_da.SelectCommand = M_STK_StockADJ_cmd;
            M_da.Fill(M_ds, "DT_STK_RECORD");
            GridView1.DataSource = M_ds.Tables["DT_STK_RECORD"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        TXB_Vendorbatch.Enabled = false;
        Button1.Enabled = false;
        DDL_WS.Enabled = false;
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        TXB_Vendorbatch.Enabled = true;
        Button1.Enabled = true;
        DDL_WS.Enabled = true;
        Load_GVData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Load_GVData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string m_barcode,m_partno,m_vendorbatch, m_datetime, m_serialno, m_seq, m_chemname, m_eqname, m_qty, m_ws, m_updatesql;
        m_barcode = ((Label)GridView1.Rows[e.RowIndex].Cells[1].FindControl("lblbarcode")).Text;
        m_partno = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblpartno")).Text;
        m_vendorbatch = TXB_Vendorbatch.Text;
        m_datetime = ((Label)GridView1.Rows[e.RowIndex].Cells[4].FindControl("lbldatetime")).Text;
        m_serialno = ((Label)GridView1.Rows[e.RowIndex].Cells[2].FindControl("lblserialno")).Text;
        m_seq = ((Label)GridView1.Rows[e.RowIndex].Cells[3].FindControl("lblseq")).Text;
        m_chemname = ((Label)GridView1.Rows[e.RowIndex].Cells[2].FindControl("lblserialno")).Text;
        m_eqname = ((Label)GridView1.Rows[e.RowIndex].Cells[3].FindControl("lblseq")).Text;
        m_qty = "0";
        m_ws = DDL_WS.SelectedValue.ToString();
        m_updatesql = "";

        if (((TextBox)GridView1.Rows[e.RowIndex].Cells[5].FindControl("txb_in_qty_edit")).Visible)
        { 
            m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].FindControl("txb_in_qty_edit")).Text;
            m_updatesql += "declare @m_old_qty decimal(18,0) ";
            m_updatesql += "Select @m_old_qty=qty from STK_Arrival where ws=@m_ws and arrival_type='I' and arrival_date=@m_date and serialno=@m_serialno and seq=@m_seq ";
            m_updatesql += "update STK_Arrival set qty=@m_qty where ws=@m_ws and arrival_type='I' and arrival_date=@m_date and serialno=@m_serialno and seq=@m_seq ";
            m_updatesql += "update STK_Stock set STK_IN_QTY=STK_IN_QTY-@m_old_qty+@m_qty,STK_QTY=STK_QTY-@m_old_qty+@m_qty where ws=@m_ws and barcode=@m_barcode and partno=@m_partno and VendorBatch=@m_vendorbatch ";
        }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[6].FindControl("txb_out_qty_edit")).Visible)
        { 
            m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].FindControl("txb_out_qty_edit")).Text;
            m_updatesql += "declare @m_old_qty decimal(18,0) ";
            m_updatesql += "Select @m_old_qty=qty from STK_Arrival where ws=@m_ws and arrival_type='O' and arrival_date=@m_date and serialno=@m_serialno and seq=@m_seq ";
            m_updatesql += "update STK_Arrival set qty=@m_qty where ws=@m_ws and arrival_type='O' and arrival_date=@m_date and serialno=@m_serialno and seq=@m_seq ";
            m_updatesql += "update STK_Stock set STK_OUT_QTY=STK_OUT_QTY-@m_old_qty+@m_qty,STK_QTY=STK_QTY+@m_old_qty-@m_qty where ws=@m_ws and barcode=@m_barcode and partno=@m_partno and VendorBatch=@m_vendorbatch ";
        }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[7].FindControl("txb_using_qty_edit")).Visible)
        { m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[7].FindControl("txb_using_qty_edit")).Text; }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[8].FindControl("txb_takeoff_qty")).Visible)
        { m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[8].FindControl("txb_takeoff_qty")).Text; }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[9].FindControl("txb_tranin_qty_edit")).Visible)
        { m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[9].FindControl("txb_tranin_qty_edit")).Text; }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[10].FindControl("txb_tranout_qty_edit")).Visible)
        { m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[10].FindControl("txb_tranout_qty_edit")).Text; }

        else if (((TextBox)GridView1.Rows[e.RowIndex].Cells[11].FindControl("txb_adj_qty_edit")).Visible)
        { m_qty = ((TextBox)GridView1.Rows[e.RowIndex].Cells[11].FindControl("txb_adj_qty_edit")).Text; }

        try
        {
            M_sqlcmd.CommandText = m_updatesql;
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_ws", @m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", m_barcode);
            M_sqlcmd.Parameters.AddWithValue("m_partno", m_partno);
            M_sqlcmd.Parameters.AddWithValue("m_vendorbatch", m_vendorbatch);
            M_sqlcmd.Parameters.AddWithValue("m_qty", m_qty);
            M_sqlcmd.Parameters.AddWithValue("m_serialno", m_serialno);
            M_sqlcmd.Parameters.AddWithValue("m_seq", m_seq);
            M_sqlcmd.Parameters.AddWithValue("m_date", m_datetime.Substring(0,10));

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView1.EditIndex = -1;
            DDL_WS.Enabled = true;
            TXB_Vendorbatch.Enabled = true;
            Button1.Enabled = true;
            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowType == DataControlRowType.DataRow) || (e.Row.RowType == DataControlRowType.Footer))
        {
            if (e.Row.DataItem != null)
            {
                if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
                {
                    if (((CheckBox)e.Row.Cells[12].FindControl("chb_data_cancel")).Checked)  //條件式
                    {
                        e.Row.Attributes.Add("style", "background-color:#FFCCFF"); //當該筆資料已經作廢，變更background顏色
                        ((LinkButton)e.Row.Cells[12].FindControl("lbtn_cancel")).Text = "取消作廢";
                    }

                    if (((Label)e.Row.Cells[5].FindControl("lblin_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[5].FindControl("lblin_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[6].FindControl("lblout_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[6].FindControl("lblout_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[7].FindControl("lblusing_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[7].FindControl("lblusing_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[8].FindControl("lbltakeoff_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[8].FindControl("lbltakeoff_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[9].FindControl("lbltranin_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[9].FindControl("lbltranin_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[10].FindControl("lbltranout_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[10].FindControl("lbltranout_qty")).Text = "";
                    }
                    if (((Label)e.Row.Cells[11].FindControl("lbladj_qty")).Text == "0")
                    {
                        ((Label)e.Row.Cells[11].FindControl("lbladj_qty")).Text = "";
                    }
                }
                else if (e.Row.RowState == DataControlRowState.Edit || ((int)e.Row.RowState) == 5)
                {
                    if (((TextBox)e.Row.Cells[5].FindControl("txb_in_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[5].FindControl("txb_in_qty_edit")).Visible=false;
                    }
                    if (((TextBox)e.Row.Cells[6].FindControl("txb_out_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[6].FindControl("txb_out_qty_edit")).Visible = false;
                    }
                    if (((TextBox)e.Row.Cells[7].FindControl("txb_using_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[7].FindControl("txb_using_qty_edit")).Visible = false;
                    }
                    if (((TextBox)e.Row.Cells[8].FindControl("txb_takeoff_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[8].FindControl("txb_takeoff_qty_edit")).Visible = false;
                    }
                    if (((TextBox)e.Row.Cells[9].FindControl("txb_tranin_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[9].FindControl("txb_tranin_qty_edit")).Visible = false;
                    }
                    if (((TextBox)e.Row.Cells[10].FindControl("txb_tranout_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[10].FindControl("txb_tranout_qty_edit")).Visible = false;
                    }
                    if (((TextBox)e.Row.Cells[11].FindControl("txb_adj_qty_edit")).Text == "0")
                    {
                        ((TextBox)e.Row.Cells[11].FindControl("txb_adj_qty_edit")).Visible = false;
                    }
                }
            }
        }
    }
}