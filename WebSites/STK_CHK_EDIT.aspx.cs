using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_CHK_EDIT : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_StockADJ_cmd = new SqlCommand();
    private SqlCommand M_QueryData_cmd = new SqlCommand();
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
    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "Select '' as wsno union SELECT wsno from STK_WS Order By wsno ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_WS.DataValueField = "wsno";
                DDL_WS.DataTextField = "wsno";

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
    private void GET_DDL_DATA_CHEMNAME()
    {
        string m_sqlstr = "SELECT '' as depa,'' as Chemname union SELECT depa,Chemname FROM  STK_CHEMDATA where depa='SDS' or depa='CDS'  Order by depa,ChemName Asc ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_CHEMNAME.DataValueField = "Chemname";
                DDL_CHEMNAME.DataTextField = "Chemname";

                DDL_CHEMNAME.DataSource = ddl;
                DDL_CHEMNAME.DataBind();
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
    private void GET_DDL_DATA_CHKDATE()
    {
        string m_sqlstr = "SELECT DISTINCT CHK_DATE FROM STK_CHK_SI where chk_ws='" + DDL_WS.SelectedValue.ToString() + "' Order By CHK_DATE Asc ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_CHK_DATE.DataValueField = "chk_date";
                DDL_CHK_DATE.DataTextField = "chk_date";

                DDL_CHK_DATE.DataSource = ddl;
                DDL_CHK_DATE.DataBind();
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
        dt2.TableName = "T_CHK_Data";
        dt2.Columns.Add(new DataColumn("Chemname", typeof(string)));
        dt2.Columns.Add(new DataColumn("Barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("Partno", typeof(string)));
        dt2.Columns.Add(new DataColumn("Vendorbatch", typeof(string)));
        dt2.Columns.Add(new DataColumn("fir_qty", typeof(string)));
        dt2.Columns.Add(new DataColumn("in_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("out_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("using_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("adj_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("acc_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("chk_qty", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("done_using", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("done_arrival", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("dis_qty", typeof(decimal)));


        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        ViewState["T_CHK_Data"] = dt2;
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }
    private void Load_GVData()
    {
        
        string m_querystr = "Select a.*,b.chemname from STK_CHK_DI as a left join stk_chemdata as b on a.chk_ws=b.ws and a.partno=b.partno Where ";
        if ( ! string.IsNullOrEmpty(DDL_WS.SelectedValue.ToString())) m_querystr = m_querystr + " a.chk_ws='" + DDL_WS.SelectedValue.ToString().Trim() + "' and ";
        if ( ! string.IsNullOrEmpty(DDL_CHK_DATE.SelectedValue.ToString())) m_querystr = m_querystr + " a.chk_date='" + DDL_CHK_DATE.SelectedValue.ToString().Trim() + "' and ";
        if ( ! string.IsNullOrEmpty(DDL_CHEMNAME.SelectedValue.ToString())) m_querystr = m_querystr + " b.chemname='" + DDL_CHEMNAME.SelectedValue.ToString().Trim() + "' and ";

        if (!string.IsNullOrEmpty(txb_vendorbatch.Text)) m_querystr = m_querystr + " a.vendorbatch like '%" + txb_vendorbatch.Text.Trim() + "%' and ";

        m_querystr = m_querystr.Substring(0, m_querystr.Length - 5);
        m_querystr = m_querystr + " order by b.chemname,a.Partno,a.Vendorbatch ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_StockADJ_cmd.Connection = M_SQLConn;

            M_STK_StockADJ_cmd.CommandText = m_querystr;

            M_da.SelectCommand = M_STK_StockADJ_cmd;
            M_da.Fill(M_ds, "DT_STK_CHK");
            GridView1.DataSource = M_ds.Tables["DT_STK_CHK"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string m_chk_ws, m_chk_date;
        bool m_chk_ok = false;
        m_chk_ws = DDL_WS.SelectedValue.ToString();
        m_chk_date = DDL_CHK_DATE.SelectedValue.ToString();

        M_QueryData_cmd.CommandText = "SELECT * FROM STK_CHK_SI WHERE chk_date=@m_chk_date and chk_ws=@m_chk_ws";
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_QueryData_cmd.Connection = M_SQLConn;
            M_SQLConn.Open();
            M_QueryData_cmd.Parameters.Clear();
            M_QueryData_cmd.Parameters.AddWithValue("@m_chk_ws", m_chk_ws);
            M_QueryData_cmd.Parameters.AddWithValue("@m_chk_date", m_chk_date);

            SqlDataReader M_dr = M_QueryData_cmd.ExecuteReader();

            if (M_dr.HasRows)
            {
                M_dr.Read();
                m_chk_ok = Convert.ToBoolean(M_dr["chk_ok"].ToString());
            }
            M_dr.Close();
            M_dr.Dispose();
            M_QueryData_cmd.Dispose();
            M_SQLConn.Close();
            M_SQLConn.Dispose();
        }

        if (m_chk_ok)
        {
            GridView1.EditIndex = -1;
            Response.Write("<script>alert('盤點日期: " + m_chk_date+" - "+m_chk_ws + " 資料已調整，不可以修改');</script>");
        }
        else
        {
            GridView1.EditIndex = e.NewEditIndex;
            DDL_WS.Enabled = false;
            DDL_CHK_DATE.Enabled = false;
            DDL_CHEMNAME.Enabled = false;
            txb_vendorbatch.Enabled = false;
            Button1.Enabled = false;
        }
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        DDL_WS.Enabled = true;
        DDL_CHK_DATE.Enabled = true;
        DDL_CHEMNAME.Enabled = true;
        txb_vendorbatch.Enabled = true;
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
        string m_barcode,m_chk_ws,m_chk_date;
        string m_errorobject = "";
        decimal m_chk_qty,m_fir_qty,m_in_qty,m_out_qty,m_using_qty,m_adj_qty,m_acc_qty,m_done_using,m_dis_qty,m_done_arrival;
        try
        {
            m_errorobject = "盤點數 ";
            if (string.IsNullOrEmpty(((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_chk_qty_edit")).Text))
                m_chk_qty = 0;
            else
                m_chk_qty = Convert.ToDecimal(((TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txb_chk_qty_edit")).Text);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        m_chk_ws = DDL_WS.SelectedValue.ToString();
        m_chk_date = DDL_CHK_DATE.SelectedValue.ToString();
        m_barcode = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_barcode")).Text;
        m_fir_qty = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_fir_qty")).Text);
        m_in_qty =  Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_in_qty")).Text);
        m_out_qty =  Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_out_qty")).Text);
        m_using_qty = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_using_qty")).Text);
        m_adj_qty = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_adj_qty")).Text);
        m_acc_qty = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_acc_qty")).Text);
        m_done_using = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_done_using")).Text);
        m_done_arrival = Convert.ToDecimal(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_done_arrival")).Text);
        m_dis_qty = (m_chk_qty - m_acc_qty + m_done_using - m_done_arrival);

        try
        {

            M_sqlcmd.CommandText = "UPDATE STK_CHK_DI SET CHK_QTY=@m_chk_qty,DIS_QTY=@m_dis_qty WHERE CHK_DATE=@m_chk_date and CHK_WS=@m_chk_ws and BARCODE=@m_barcode ";
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_chk_qty", m_chk_qty);
            M_sqlcmd.Parameters.AddWithValue("m_dis_qty", m_dis_qty);
            M_sqlcmd.Parameters.AddWithValue("m_chk_date", m_chk_date);
            M_sqlcmd.Parameters.AddWithValue("m_chk_ws", m_chk_ws);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", m_barcode);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView1.EditIndex = -1;

            DDL_WS.Enabled = true;
            DDL_CHK_DATE.Enabled = true;
            DDL_CHEMNAME.Enabled = true;
            txb_vendorbatch.Enabled = true;
            Button1.Enabled = true;

            Load_GVData();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        
    }

    protected void DDL_ws_SelectedIndexChanged(object sender, EventArgs e)
    {
        GET_DDL_DATA_CHKDATE();
    }
}