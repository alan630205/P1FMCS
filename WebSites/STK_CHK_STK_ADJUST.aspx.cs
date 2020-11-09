using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_CHK_STK_ADJUST : System.Web.UI.Page
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
            GET_DDL_DATA_WS();
        }
    }
    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "SELECT wsno from STK_WS Order By wsno ";

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }
    private void Load_GVData()
    {
        
        string m_querystr ;
        m_querystr =  "Select a.*,(Select sum(acc_qty) from stk_chk_di where chk_date=a.chk_date ) as acc_qty,";
        m_querystr += "(Select sum(chk_qty+done_using-done_arrival) from stk_chk_di where chk_date=a.chk_date ) as chk_qty,";
        m_querystr += "(Select sum(dis_qty) from stk_chk_di where chk_date=a.chk_date ) as dis_qty ";
        m_querystr += "from stk_chk_si as a where a.chk_ws=@m_ws order by a.chk_date desc";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {

            M_STK_StockADJ_cmd.Connection = M_SQLConn;

            M_STK_StockADJ_cmd.CommandText = m_querystr;

            M_STK_StockADJ_cmd.Parameters.Clear();
            M_STK_StockADJ_cmd.Parameters.AddWithValue("m_ws", DDL_WS.SelectedValue.ToString());
            M_da.SelectCommand = M_STK_StockADJ_cmd;
            M_da.Fill(M_ds, "DT_STK_CHK");
            GridView1.DataSource = M_ds.Tables["DT_STK_CHK"];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        DDL_WS.Enabled = false;
        Button1.Enabled = false;
        Load_GVData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
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
        string m_chk_date, m_chk_ws;
        string m_sqlstr,m_userid;
        bool m_chk;
        DateTime m_adj_datetime,m_updatetime;

        m_chk = ((CheckBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("cb_chk_ok")).Checked;
        m_chk_date = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_chk_date")).Text;
        m_chk_ws = DDL_WS.SelectedValue.ToString();
        m_userid = Session["LoginName"].ToString();
        m_sqlstr =  "INSERT INTO STK_ADJ_LOG(DEPA,WS,ADJ_DATETIME,BARCODE,PARTNO,VENDORBATCH,EXPIRATIONDATE,ADJ_QTY,ADJ_DESC,ADJ_USERID,DATA_CANCEL) ";
        m_sqlstr += "SELECT b.depa AS DEPA,a.chk_ws AS WS,@M_ADJ_DATETIME AS ADJ_DATETIME,a.barcode AS BARCODE,a.partno AS PARTNO,a.vendorbatch AS VENDORBATCH,a.ExpirationDate AS EXPIRATIONDATE,a.dis_qty AS ADJ_QTY,'盤點調整' AS ADJ_DESC,@M_USERID AS ADJ_USERID,0 AS DATA_CANCEL from STK_Chk_DI as a left join stk_stock as b on a.chk_ws=b.ws and a.barcode=b.barcode where a.chk_date=@m_chk_date and a.chk_ws=@M_WS and a.dis_qty<>0 ;";
        m_sqlstr += "UPDATE STK_STOCK SET STK_ADJ_QTY=STK_ADJ_QTY+ISNULL(B.DIS_QTY,0),STK_QTY=STK_QTY+ISNULL(B.DIS_QTY,0) FROM STK_STOCK AS A LEFT JOIN STK_Chk_DI AS B ON A.WS=B.CHK_WS AND A.barcode=B.barcode WHERE B.CHK_DATE=@m_chk_date AND B.CHK_WS=@M_WS ;";
        m_sqlstr += "UPDATE STK_CHK_SI SET ADJ_DATE=@m_updatetime,ADJ_USER=@m_userid,CHK_OK=1 WHERE CHK_DATE=@m_chk_date AND CHK_WS=@M_WS;";
        m_updatetime = DateTime.Now;
        m_adj_datetime = DateTime.Parse(m_chk_date + " 23:59:59").AddDays(-1);
        
        try
        {
            if (m_chk) //已經調整
                throw new Exception(m_chk_date+" "+m_chk_ws+" 盤點資料已經調整！！");

            M_sqlcmd.CommandText = m_sqlstr;
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("M_ADJ_DATETIME", m_adj_datetime);
            M_sqlcmd.Parameters.AddWithValue("M_USERID", m_userid);
            M_sqlcmd.Parameters.AddWithValue("m_chk_date", m_chk_date);
            M_sqlcmd.Parameters.AddWithValue("M_WS", m_chk_ws);
            M_sqlcmd.Parameters.AddWithValue("m_updatetime", m_updatetime);

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
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            DDL_WS.Enabled = true;
            Button1.Enabled = true;
            GridView1.EditIndex = -1;
            Load_GVData();
        }
        
    }
}